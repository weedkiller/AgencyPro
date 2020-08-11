// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Roles.Models;
using AgencyPro.Core.Roles.ViewModels.Recruiters;
using AgencyPro.Core.Services;

namespace AgencyPro.Core.Roles.Services
{
    public interface IRecruiterService : IService<Recruiter>, IRoleService<RecruiterInput, RecruiterUpdateInput, RecruiterOutput, IRecruiter>
    {
    }
}