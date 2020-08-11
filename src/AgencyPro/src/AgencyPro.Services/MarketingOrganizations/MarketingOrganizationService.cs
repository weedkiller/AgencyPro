// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Agreements.Models;
using AgencyPro.Core.Data.Repositories;
using AgencyPro.Core.Organizations.MarketingOrganizations.Models;
using AgencyPro.Core.Organizations.MarketingOrganizations.Services;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.MarketingOrganizations
{
    public partial class MarketingOrganizationService : Service<MarketingOrganization>, IMarketingOrganizationService
    {
        private readonly ILogger<MarketingOrganizationService> _logger;
        private readonly IRepositoryAsync<MarketingAgreement> _marketingAgreement;

        public MarketingOrganizationService(
            ILogger<MarketingOrganizationService> logger,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _logger = logger;
            _marketingAgreement = UnitOfWork.RepositoryAsync<MarketingAgreement>();
        }
    }
}
