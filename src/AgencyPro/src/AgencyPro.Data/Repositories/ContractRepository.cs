// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using AgencyPro.Core.Candidates.Models;
using AgencyPro.Core.Contracts.Models;

namespace AgencyPro.Data.Repositories
{
    public static class CandidateRepository
    {
        public static IQueryable<Candidate> GetById(
            this IQueryable<Candidate> repo, Guid id)
        {
            return repo.Where(x => x.Id == id);
        }
    }
    public static class ContractRepository
    {
        public static IQueryable<Contract> GetById(
            this IQueryable<Contract> repo, Guid id)
        {
            return repo.Where(x => x.Id == id);
        }
    }
}