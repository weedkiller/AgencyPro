// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.Invoices.Services;
using AgencyPro.Core.Invoices.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.AgencyOwner.API.Controllers.v2
{
    public class InvoiceSummaryController  : OrganizationUserController
    {
        private readonly IInvoiceProjectSummaryService _service;
        private readonly IProviderAgencyOwner _agencyOwner;

        public InvoiceSummaryController(IProviderAgencyOwner agencyOwner, IServiceProvider serviceProvider, IInvoiceProjectSummaryService service) : base(serviceProvider)
        {
            _agencyOwner = agencyOwner;
            _service = service;
        }

       

        /// <summary>
        /// Gets the details of project for invoicing
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet("{projectId}")]
        [ProducesResponseType(typeof(ProjectInvoiceDetails), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetInvoiceSummaryDetails([FromRoute]Guid organizationId, [FromRoute]Guid projectId)
        {
            var result = await _service.GetProjectInvoiceDetails(_agencyOwner, projectId);
            return Ok(result);
        }
    }
}
