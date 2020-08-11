// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Config;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.Data.UnitOfWork;
using AgencyPro.Core.EventHandling;
using AgencyPro.Core.Services;
using AgencyPro.Services.Account.Messaging;
using AgencyPro.Services.Account.Validation;
using IdentityModel;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AgencyPro.Core.Constants;
using AgencyPro.Core.UserAccount.Models;
using AgencyPro.Core.UserAccount.Validation;

namespace AgencyPro.Services.Account
{
   
    public partial class UserAccountManager
        : UserManager<ApplicationUser>,
            IService<ApplicationUser>,
            IQueryableUserStore<ApplicationUser>
    {
        protected EventsHandler EventsHandler { get; set; }

        private readonly ILogger<UserAccountManager> _logger;
        private readonly Lazy<AggregateValidator<ApplicationUser>> _emailValidator;
        private readonly Lazy<AggregateValidator<ApplicationUser>> _passwordValidator;
        private readonly Lazy<AggregateValidator<ApplicationUser>> _phoneNumberValidator;
        private readonly Lazy<AggregateValidator<ApplicationUser>> _usernameValidator;
        private readonly IdentityOptions _identityOptions;
        private readonly AppSettings _settings;

        public UserAccountManager(
            IUserStore<ApplicationUser> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<ApplicationUser> passwordHasher,
            IEnumerable<IUserValidator<ApplicationUser>> userValidators,
            IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators,
            ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors,
            IServiceProvider services,
            IUnitOfWorkAsync unitOfWork,
            IOptions<IdentityOptions> identityOptions,
            IOptions<AppSettings> settings,
            ILogger<UserAccountManager> logger)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _identityOptions = identityOptions.Value;
            _logger = logger;
            _settings = settings.Value;

            Repository = unitOfWork.RepositoryAsync<ApplicationUser>();
            Settings = settings.Value.Auth;

            EventsHandler = new EventsHandler();
            AddEventHandler(new MultiUserAccountEventsHandler(services));

            var accountValidators = new UserAccountValidators();

            _usernameValidator = new Lazy<AggregateValidator<ApplicationUser>>(() =>
            {
                var val = new AggregateValidator<ApplicationUser>();
                if (!_settings.Auth.EmailIsUsername)
                {
                    val.Add(UserAccountValidation.UsernameDoesNotContainAtSign);
                    val.Add(UserAccountValidation.UsernameCanOnlyStartOrEndWithLetterOrDigit);
                    val.Add(UserAccountValidation.UsernameOnlyContainsValidCharacters);
                    val.Add(UserAccountValidation.UsernameOnlySingleInstanceOfSpecialCharacters);
                }

                val.Add(UserAccountValidation.UsernameMustNotAlreadyExist);
                val.Add(accountValidators.UsernameValidator);
                return val;
            });

            _emailValidator = new Lazy<AggregateValidator<ApplicationUser>>(() =>
            {
                var val = new AggregateValidator<ApplicationUser>
                {
                    UserAccountValidation.EmailIsRequiredIfRequireAccountVerificationEnabled,
                    UserAccountValidation.EmailIsValidFormat,
                    UserAccountValidation.EmailMustNotAlreadyExist,
                    accountValidators.EmailValidator
                };
                return val;
            });

            _phoneNumberValidator = new Lazy<AggregateValidator<ApplicationUser>>(() =>
            {
                var val = new AggregateValidator<ApplicationUser>
                {
                    UserAccountValidation.PhoneNumberIsRequiredIfRequireAccountVerificationEnabled,
                    UserAccountValidation.PhoneNumberMustNotAlreadyExist,
                    accountValidators.PhoneNumberValidator
                };
                return val;
            });

            _passwordValidator = new Lazy<AggregateValidator<ApplicationUser>>(() =>
            {
                var val = new AggregateValidator<ApplicationUser>
                {
                    UserAccountValidation.PasswordMustBeDifferentThanCurrent,
                    accountValidators.PasswordValidator
                };
                return val;
            });

        }

        public AuthSettings Settings { get; set; }

        public override Task<string> GetSecurityStampAsync(ApplicationUser user)
        {
            return Task.FromResult(user.SecurityStamp);
        }

        public override async Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string code)
        {
            user.EmailConfirmed = true;
            if (!await VerifyUserTokenAsync(user, Options.Tokens.EmailConfirmationTokenProvider, ConfirmEmailTokenPurpose, code))
            {
                List<IdentityError> errors = new List<IdentityError>() { new IdentityError { Description = "Invalid or expired token" } };
                return IdentityResult.Failed(errors.ToArray());
            }
            var records = await Repository.UpdateAsync(user, true);
            if (records > 0)
            {
                FireEvents();

                return IdentityResult.Success;
            }

            return IdentityResult.Failed();
        }
        public override Task<IdentityResult> SetUserNameAsync(ApplicationUser user, string userName)
        {
            user.UserName = userName;
            Repository.UpdateAsync(user, true);

            return Task.FromResult(IdentityResult.Success);
        }

        public override Task<IdentityResult> SetPhoneNumberAsync(ApplicationUser user, string phoneNumber)
        {
            user.PhoneNumber = phoneNumber;
            Repository.UpdateAsync(user, true);

            return Task.FromResult(IdentityResult.Success);
        }

        public override Task<IdentityResult> SetEmailAsync(ApplicationUser user, string email)
        {
            user.Email = email;
            Repository.UpdateAsync(user, true);

            return Task.FromResult(IdentityResult.Success);
        }

        public override Task<IList<Claim>> GetClaimsAsync(ApplicationUser account)
        {
            if (account == null) throw new ArgumentNullException(nameof(account));

            IList<Claim> claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Subject, account.Id.ToString()),
               // new Claim(JwtClaimTypes.Id, account.Id.ToString()),
                new Claim(JwtClaimTypes.Name, account.Id.ToString()),
                new Claim(JwtClaimTypes.Email, account.Email),
               // new Claim(JwtClaimTypes.EmailVerified, account.EmailConfirmed.ToString()),
               // new Claim(JwtClaimTypes.PhoneNumber, account.PhoneNumber),
               // new Claim(JwtClaimTypes.PhoneNumberVerified, account.PhoneNumberConfirmed.ToString()),
                new Claim(JwtClaimTypes.GivenName, account.Person.FirstName),
                new Claim(JwtClaimTypes.FamilyName, account.Person.LastName)
            };

            return Task.FromResult(claims);
        }

        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[UserAccountManager.{callerName}] - {message}";
        }

        public override Task<bool> IsLockedOutAsync(ApplicationUser user)
        {
            return Task.FromResult(_identityOptions.Lockout.MaxFailedAccessAttempts <= user.AccessFailedCount);
        }

        public override string GetUserId(ClaimsPrincipal principal)
        {
            return principal.GetSubjectId();
        }

        public override Task<bool> IsInRoleAsync(ApplicationUser user, string role)
        {
            if (role == RoleNames.AdminRole)
            {
                return Task.FromResult(user.IsAdmin);
            }
            if (role == RoleNames.Person)
            {
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }


        public override string GetUserName(ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.Identity.Name;
        }

        public override Task<bool> IsEmailConfirmedAsync(ApplicationUser user)
        {
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public async Task SetUserNameAsync(ApplicationUser user, string userName, CancellationToken cancellationToken)
        {
            var userCandidate = await FindByNameAsync(userName, cancellationToken);
            if (!string.IsNullOrWhiteSpace(userName) && userCandidate != null && userCandidate.Id != user.Id)
                throw new ValidationException("Username already in use");

            user.NormalizedUserName = userName.ToLower();
            user.UserName = userName;
        }
        
        public Task<string> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedUserName.ToLower());
        }

        public Task SetNormalizedUserNameAsync(ApplicationUser user, string normalizedName, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedUserName = normalizedName.ToLower());
        }


        public override async Task<IdentityResult> UpdateAsync(ApplicationUser user)
        {
            try
            {
                user.Email = user.Email;
                user.NormalizedEmail = user.Email.ToUpper();
                user.ObjectState = ObjectState.Modified;
                user.LastUpdated = DateTimeOffset.UtcNow;

                var output = Repository.InsertOrUpdateGraph(user, true);
                if (output > 0)
                {
                    return await Task.FromResult(IdentityResult.Success);
                }
                else
                {
                    return await Task.FromResult(IdentityResult.Failed());
                }

            }
            catch (Exception)
            {
                return await Task.FromResult(IdentityResult.Failed());
            }
        }

        public override Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string currentPassword, string newPassword)
        {
            List<IdentityError> errors = new List<IdentityError>();

            PasswordVerificationResult passwordHash = PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, currentPassword);
            if (passwordHash == PasswordVerificationResult.Failed)
            {
                errors.Add(new IdentityError()
                {
                    Code = "101",
                    Description = "Invalid current password"
                });
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }

            var result = _passwordValidator.Value.Validate(this, user, newPassword);
            if (result == null || result == ValidationResult.Success)
            {
                var newPasswordHash = PasswordHasher.HashPassword(user, newPassword);
                user.PasswordHash = newPasswordHash;
                user.ObjectState = ObjectState.Modified;
                user.LastUpdated = DateTimeOffset.UtcNow;

                var output = Repository.InsertOrUpdateGraph(user, true);
                if (output > 0)
                {
                    return Task.FromResult(IdentityResult.Success);
                }
                errors.Add(new IdentityError()
                {
                    Code = "100",
                    Description = "Unable to update data store"
                });
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            };
            errors.Add(new IdentityError()
            {
                Code = "102",
                Description = result?.ErrorMessage ?? "Invalid new password"
            });

            return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
        }
    }
}