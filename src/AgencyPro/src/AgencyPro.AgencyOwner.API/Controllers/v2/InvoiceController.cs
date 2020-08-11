// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Invoices.Filters;
using AgencyPro.Core.Invoices.Services;
using AgencyPro.Core.Invoices.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgencyPro.AgencyOwner.API.Controllers.v2
{
    public class InvoiceController : OrganizationUserController
    {
        private readonly IProviderAgencyOwner _agencyOwner;
        private readonly IProjectInvoiceService _invoiceService;
        private readonly IInvoiceProjectSummaryService _invoiceSummaryService;

        public InvoiceController(
            IProjectInvoiceService invoiceService,
            IInvoiceProjectSummaryService invoiceSummaryService,
            IProviderAgencyOwner agencyOwner,
            IServiceProvider provider) :
            base(provider)
        {
            _agencyOwner = agencyOwner;
            _invoiceService = invoiceService;
            _invoiceSummaryService = invoiceSummaryService;
        }

        /// <summary>
        /// Gets a summary of projects for invoicing
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpGet("summary")]
        [ProducesResponseType(typeof(ProjectInvoiceSummary), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetInvoiceSummary([FromRoute]Guid organizationId)
        {
            var result = await _invoiceSummaryService.GetProjectInvoiceSummary(_agencyOwner);
            return Ok(result);
        }


        /// <summary>
        /// Gets the details of project for invoicing
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet("summary/{projectId}/details")]
        [ProducesResponseType(typeof(ProjectInvoiceDetails), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetInvoiceSummaryDetails([FromRoute]Guid organizationId, [FromRoute]Guid projectId)
        {
            var result = await _invoiceSummaryService.GetProjectInvoiceDetails(_agencyOwner, projectId);
            return Ok(result);
        }

        /// <summary>
        ///     Get the invoice by id
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        [HttpGet("{invoiceId}")]
        [ProducesResponseType(typeof(AgencyOwnerProjectInvoiceDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetInvoice([FromRoute] Guid organizationId, [FromRoute] string invoiceId)
        {
            var x = await _invoiceService
                .GetInvoice<AgencyOwnerProjectInvoiceDetailsOutput>(_agencyOwner, invoiceId);

            return Ok(x);
        }

        /// <summary>
        ///     gets invoices from ao perspective
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<AgencyOwnerProjectInvoiceOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetInvoices([FromRoute] Guid organizationId, [FromQuery] InvoiceFilters filters
        )
        {
            var invoices = await _invoiceService.GetInvoices<AgencyOwnerProjectInvoiceOutput>(_agencyOwner, filters);
            AddPagination(filters, invoices.Total);
            return Ok(invoices.Data);
        }


        /// <summary>
        ///     Generates an invoice
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("")]
        [ProducesResponseType(typeof(InvoiceResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateInvoice([FromRoute] Guid organizationId, [FromBody] InvoiceInput input)
        {
            var invoice =
                await _invoiceService.CreateInvoice(_agencyOwner, input);
            return Ok(invoice);
        }


        /// <summary>
        ///     Generates an invoice
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        [HttpPost("{invoiceId}/finalize")]
        [ProducesResponseType(typeof(InvoiceResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> FinalizeInvoice([FromRoute] Guid organizationId, [FromRoute] string invoiceId)
        {
            var result = await _invoiceService.FinalizeInvoice(_agencyOwner, invoiceId);

            return Ok(result);

        }

        /// <summary>
        /// Send invoice to customer
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        [HttpPatch("{invoiceId}/send")]
        [ProducesResponseType(typeof(InvoiceResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> SendInvoice([FromRoute] Guid organizationId, [FromRoute] string invoiceId)
        {
            var result = await _invoiceService.SendInvoice(_agencyOwner, invoiceId);

            return Ok(result);

        }

    }
}