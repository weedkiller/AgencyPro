// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Lookup.ViewModels;

namespace AgencyPro.Core.Lookup.Services
{
    public interface ILookupService
    {
        LookupOutput GetAll();
    }
}