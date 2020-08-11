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

namespace AgencyPro.Customer.API.Controllers.v2
{
    public class InvoiceController : OrganizationUserController
    {
        private readonly IOrganizationCustomer _customer;
        private readonly IProjectInvoiceService _invoiceService;
        public InvoiceController(IProjectInvoiceService invoiceService,
            IOrganizationCustomer customer,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _invoiceService = invoiceService;
            _customer = customer;
        }

       

        /// <summary>
        ///     Get the invoice by id
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        [HttpGet("{invoiceId}")]
        [ProducesResponseType(typeof(CustomerProjectInvoiceDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetInvoice([FromRoute] Guid organizationId, [FromRoute] string invoiceId
        )
        {
            var x = await _invoiceService.GetInvoice<CustomerProjectInvoiceDetailsOutput>(_customer, invoiceId);
            return Ok(x);
        }

        /// <summary>
        ///     Gets invoices
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<CustomerProjectInvoiceOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetInvoices([FromRoute] Guid organizationId, [FromQuery] InvoiceFilters filters
        )
        {
            var invoices = await _invoiceService.GetInvoices<CustomerProjectInvoiceOutput>(_customer, filters);
            AddPagination(filters, invoices.Total);
            return Ok(invoices.Data);
        }
    }
}