// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Threading.Tasks;
using AgencyPro.Core.StripeSources.ViewModels;
using Stripe;

namespace AgencyPro.Core.StripeSources.Services
{
    public interface ISourceService
    {
        Task<SourceResult> SourceChargeable(Source source);
    }
}