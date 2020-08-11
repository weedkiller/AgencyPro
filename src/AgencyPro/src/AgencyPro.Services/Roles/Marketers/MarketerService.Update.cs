// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.Roles.Services;
using AgencyPro.Core.Roles.ViewModels.Marketers;

namespace AgencyPro.Services.Roles.Marketers
{
    public partial class MarketerService
    {
        public Task<T> Update<T>(IMarketer principal, MarketerUpdateInput model)
            where T : MarketerOutput
        {
            throw new NotImplementedException();
        }
    }
}