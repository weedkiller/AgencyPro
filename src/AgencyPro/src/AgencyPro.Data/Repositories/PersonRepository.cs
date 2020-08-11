// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using AgencyPro.Core.People.Models;

namespace AgencyPro.Data.Repositories
{
    public static class PersonRepository
    {
        public static IQueryable<Person> GetById(
            this IQueryable<Person> repo, Guid id)
        {
            return repo.Where(x => x.Id == id);
        }

        public static IQueryable<Person> GetByEmail(
            this IQueryable<Person> repo, string email)
        {
            return repo.Where(x => x.ApplicationUser.Email == email);
        }
    }
}