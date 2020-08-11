// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Core.Config;
using AgencyPro.Core.Proposals.Services;
using AgencyPro.Core.Proposals.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AgencyPro.Identity.API.Controllers.Proposal
{
    [SecurityHeaders]
    [AllowAnonymous]
    public class ProposalController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IOptions<AppSettings> _settings;
        private readonly IProposalService _proposalService;
        private readonly IProposalPDFService _proposalPdfService;

        public ProposalController(IHostingEnvironment hostingEnvironment, IOptions<AppSettings> settings,
            IProposalService proposalService, IProposalPDFService proposalPdfService)
        {
            _settings = settings;
            _hostingEnvironment = hostingEnvironment;
            _proposalService = proposalService;
            _proposalPdfService = proposalPdfService;
        }

        /// <summary>
        /// Show Detail of the proposal to the user
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Detail(string id)
        {
            FixedPriceProposalDetailOutput fixedPriceProposalDetailOutput = null;
            if (ModelState.IsValid)
            {
                Guid.TryParse(id, out Guid ProposalGuid);
                fixedPriceProposalDetailOutput = await _proposalService.GetProposal<FixedPriceProposalDetailOutput>(ProposalGuid);
            }

            if (fixedPriceProposalDetailOutput == null)
            {
                throw new ApplicationException("Proposal Not Found");
            }

            return View(fixedPriceProposalDetailOutput);
        }

        public async Task<IActionResult> AcceptProposal(string id)
        {
            ProposalResult proposalResult = new ProposalResult();
            if (ModelState.IsValid)
            {
                Guid.TryParse(id, out Guid ProposalGuid);
                proposalResult = await _proposalService.AcceptFixedPriceProposal(ProposalGuid);
            }

            string message = string.Empty;
            if(proposalResult.Succeeded)
                message = "Success! Proposal accepted successfully";
            else
                message = !string.IsNullOrEmpty(proposalResult.ErrorMessage?.Trim()) ? "Error! " + proposalResult.ErrorMessage : "Error! Proposal not accepted";

            TempData["ProposalAcceptStatus"] = message;
            return RedirectToAction("Detail", new { id });
        }

        public async Task<IActionResult> RejectProposal(string id)
        {
            ProposalResult proposalResult = new ProposalResult();
            if (ModelState.IsValid)
            {
                Guid.TryParse(id, out Guid ProposalGuid);
                proposalResult = await _proposalService.Reject(ProposalGuid, new ProposalRejectionInput());
            }

            string message = string.Empty;
            if (proposalResult.Succeeded)
                message = "Success! Proposal rejected successfully";
            else
                message = !string.IsNullOrEmpty(proposalResult.ErrorMessage?.Trim()) ? "Error! " + proposalResult.ErrorMessage : "Error! Proposal not rejected";

            TempData["ProposalRejectStatus"] = message;
            return RedirectToAction("Detail", new { id });
        }

        public async Task<IActionResult> DownloadProposal(string id)
        {
            Guid.TryParse(id, out Guid ProposalGuid);
            return await _proposalPdfService.GenerateProposal(ProposalGuid);
        }
    }
}