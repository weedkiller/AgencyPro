// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.PaymentTerms.Services;
using AgencyPro.Core.PaymentTerms.ViewModels;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.AccountManager.API.Controllers.v2
{
    public class PaymentTermController : OrganizationUserController
    {
        private readonly IOrganizationAccountManager _accountManager;
        private readonly IPaymentTermService _paymentTermService;

        public PaymentTermController(IOrganizationAccountManager accountManager, IServiceProvider serviceProvider, IPaymentTermService paymentTermService ) : base(serviceProvider)
        {
            _accountManager = accountManager;
            _paymentTermService = paymentTermService;
        }

        /// <summary>
        /// Gets payment terms for organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<PaymentTermOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPaymentTerms( [FromRoute]Guid organizationId )
        {
            var result = await _paymentTermService.GetPaymentTermsByOrganization(organizationId);
            return Ok(result);
        }

        /// <summary>
        /// Add payment term to organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="paymentTermId"></param>
        /// <returns></returns>
        [HttpPut("{paymentTermId}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddPaymentTermToOrganization([FromRoute] Guid organizationId, [FromRoute]int paymentTermId)
        {
            var result = await _paymentTermService.AddPaymentTermToOrganization(_accountManager, paymentTermId);
            return Ok(result);
        }

        /// <summary>
        /// Removes a payment term from an organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="paymentTermId"></param>
        /// <returns></returns>
        [HttpDelete("{paymentTermId}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> RemovePaymentTermFromOrganization([FromRoute] Guid organizationId, [FromRoute]int paymentTermId)
        {
            var result = await _paymentTermService.RemovePaymentFromOrganization(_accountManager, paymentTermId);
            return Ok(result);
        }

    }
}