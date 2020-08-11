// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationPeople.Services;
using AgencyPro.Core.People.Services;
using AgencyPro.Core.People.ViewModels;
using AgencyPro.Core.Registration.Events;
using AgencyPro.Core.Registration.Services;
using AgencyPro.Core.Registration.ViewModels;
using AgencyPro.Core.UserAccount.Models;
using AgencyPro.Services.Registration.EventHandlers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Registration
{
    public class RegistrationService : Service<ApplicationUser>, IRegistrationService
    {
        private readonly IOrganizationPersonService _organizationPersonService;
        private readonly IPersonService _personService;
        private readonly ILogger<RegistrationService> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public RegistrationService(
            IOrganizationPersonService organizationPersonService,
            UserManager<ApplicationUser> userManager,
            IPersonService personService,

            RegistrationEventHandlers events,
            ILogger<RegistrationService> logger,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _organizationPersonService = organizationPersonService;
            _personService = personService;
            _logger = logger;
            _userManager = userManager;

            AddEventHandler(events);
        }

        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[{nameof(RegistrationService)}.{callerName}] - {message}";
        }

        public async Task<RegistrationResult> Register(RegisterInputModel model)
        {
            _logger.LogInformation(GetLogMessage("Email: {0}"),model.Email);

            var retVal = new RegistrationResult();

            var affiliate = await _organizationPersonService.GetPersonByCode(model.InvitationCode);
            if (affiliate == null)
            {
                retVal.ErrorMessage = "Invalid affiliate code";
                return retVal;
            }

            var newPerson = await _personService.CreatePerson(new PersonInput()
            {
                Iso2 = model.Iso2,
                ProvinceState = model.ProvinceState,
                EmailAddress = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,

                PhoneNumber = model.Phone,
                SendEmail = false
            }, affiliate.PersonId, affiliate.PersonId, affiliate.OrganizationId, model.Password);

            if (newPerson.Succeeded)
            {
                retVal.Succeeded = newPerson.Succeeded;
                retVal.PersonId = newPerson.PersonId;

                await Task.Run(() =>
                {
                    RaiseEvent(new RegistrationEvent()
                    {
                        PersonId = newPerson.PersonId.Value
                    });
                });
            }


            return retVal;
        }
    }
}
