// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AgencyPro.Core.UserAccount.Models;
using IdentityModel;

namespace AgencyPro.Core.UserAccount
{
    public static class UserAccountExtensions
    {
        public static bool HasClaim(this ApplicationUser account, string type)
        {
            if (account == null) throw new ArgumentException("account");
            if (String.IsNullOrWhiteSpace(type)) throw new ArgumentException("type");

            return account.UserClaims.Any(x => x.ClaimType == type);
        }

        public static bool HasClaim(this ApplicationUser account, string type, string value)
        {
            if (account == null) throw new ArgumentException("account");
            if (String.IsNullOrWhiteSpace(type)) throw new ArgumentException("type");
            if (String.IsNullOrWhiteSpace(value)) throw new ArgumentException("value");

            return account.UserClaims.Any(x => x.ClaimType == type && x.ClaimValue == value);
        }

        public static IEnumerable<string> GetClaimValues(this ApplicationUser account, string type)
        {
            if (account == null) throw new ArgumentException("account");
            if (String.IsNullOrWhiteSpace(type)) throw new ArgumentException("type");

            var query =
                from claim in account.UserClaims
                where claim.ClaimType == type
                select claim.ClaimValue;
            return query.ToArray();
        }

        public static string GetClaimValue(this ApplicationUser account, string type)
        {
            if (account == null) throw new ArgumentException("account");
            if (String.IsNullOrWhiteSpace(type)) throw new ArgumentException("type");

            var query =
                from claim in account.UserClaims
                where claim.ClaimType == type
                select claim.ClaimValue;
            return query.SingleOrDefault();
        }


        public static IEnumerable<Claim> GetIdentificationClaims(this ApplicationUser account)
        {
            if (account == null) throw new ArgumentNullException(nameof(account));

            var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Subject, account.Id.ToString()),
                new Claim(JwtClaimTypes.Id, account.Id.ToString()),
                new Claim(JwtClaimTypes.Name, account.UserName)
            };

            return claims;
        }
        public static IEnumerable<Claim> GetAllClaims(this ApplicationUser account)
        {
            if (account == null) throw new ArgumentNullException(nameof(account));

            var claims = new List<Claim>();
            claims.AddRange(account.GetIdentificationClaims());

            if (!String.IsNullOrWhiteSpace(account.Email))
            {
                claims.Add(new Claim(JwtClaimTypes.Email, account.Email));
            }
            if (!String.IsNullOrWhiteSpace(account.PhoneNumber))
            {
                claims.Add(new Claim(JwtClaimTypes.PhoneNumber, account.PhoneNumber));
            }
            
            var otherClaims =
                (from uc in account.UserClaims 
                    select new Claim(uc.ClaimType, uc.ClaimValue)).ToList();
            claims.AddRange(otherClaims);

            return claims;
        }


        public static bool HasPassword(this ApplicationUser account)
        {
            if (account == null) throw new ArgumentException("account");
            return !string.IsNullOrWhiteSpace(account.PasswordHash);
        }

        public static bool IsNew(this ApplicationUser account)
        {
            if (account == null) throw new ArgumentException("account");
            return account.LastLogin == null;
        }
        
    }
}
