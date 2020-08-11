// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.Invoices.Filters;
using AgencyPro.Core.Invoices.Services;
using AgencyPro.Core.Invoices.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.AccountManager.API.Controllers.v2
{
    public class InvoiceController : OrganizationUserController
    {
        private readonly IOrganizationAccountManager _accountManager;
        private readonly IProjectInvoiceService _invoiceService;

        public InvoiceController(IProjectInvoiceService invoiceService, IOrganizationAccountManager accountManager,
            IServiceProvider serviceProvider) : base(
            serviceProvider)
        {
            _accountManager = accountManager;
            _invoiceService = invoiceService;
        }

        /// <summary>
        ///     Generate an invoice
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="projectId"></param>
        /// <param name="input"></param>
        /// <returns>number of affected results</returns>
        [HttpPost("generate")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<IActionResult> GenerateInvoice([FromRoute] Guid organizationId, [FromQuery] Guid projectId,
            [FromBody] InvoiceInput input)
        {
            var invoice =
                await _invoiceService.CreateInvoice(_accountManager, input);
            
            return Ok(invoice);
        }

        /// <summary>
        ///     gets invoices from ao perspective
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<AccountManagerProjectInvoiceOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromRoute] Guid organizationId, [FromQuery] InvoiceFilters filters)
        {
            var invoices =
                await _invoiceService.GetInvoices<AccountManagerProjectInvoiceOutput>(_accountManager, filters);
            AddPagination(filters, invoices.Total);
            return Ok(invoices.Data);
        }

        /// <summary>
        /// send invoice to customer
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        [HttpPatch("{invoiceId}/send")]
        [ProducesResponseType(typeof(InvoiceResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> SendInvoice([FromRoute] Guid organizationId, [FromRoute] string invoiceId)
        {
            var result = await _invoiceService.SendInvoice(_accountManager, invoiceId);

            return Ok(result);

        }

        /// <summary>
        ///     Get the invoice by id
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        [HttpGet("{invoiceId}")]
        [ProducesResponseType(typeof(AccountManagerProjectInvoiceDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetInvoice([FromRoute] Guid organizationId, [FromRoute] string invoiceId)
        {
            var x = await _invoiceService
                .GetInvoice<AccountManagerProjectInvoiceDetailsOutput>(_accountManager, invoiceId);

            return Ok(x);
        }
    }
}