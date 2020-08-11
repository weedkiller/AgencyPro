// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Threading.Tasks;
using AgencyPro.Core.Roles.Services;
using AgencyPro.Core.Roles.ViewModels.AccountManagers;
using AgencyPro.Core.Roles.ViewModels.Contractors;
using AgencyPro.Core.Roles.ViewModels.Customers;
using AgencyPro.Core.Roles.ViewModels.Marketers;
using AgencyPro.Core.Roles.ViewModels.ProjectManagers;
using AgencyPro.Core.Roles.ViewModels.Recruiters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.Person.API.Controllers.v2
{
    [ApiExplorerSettings(IgnoreApi = false)]
    [Route("customer")]
    [Produces("application/json")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomer _principal;
        private readonly ICustomerService _service;

        public CustomerController(ICustomer principal, ICustomerService service)
        {
            _service = service;
            _principal = principal;
        }

        [HttpPatch]
        [ProducesResponseType(typeof(CustomerDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromBody]CustomerUpdateInput input)
        {
            var result = await _service.Update<CustomerDetailsOutput>(_principal, input);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(CustomerDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var result = await _service.GetById<CustomerDetailsOutput>(_principal.Id);
            return Ok(result);
        }
    }

    [ApiExplorerSettings(IgnoreApi = false)]
    [Route("account-manager")]
    [Produces("application/json")]
    public class AccountManagerController : ControllerBase
    {
        private static IAccountManager _principal;
        private readonly IAccountManagerService _service;

        public AccountManagerController(IAccountManager principal, IAccountManagerService service)
        {
            _service = service;
            _principal = principal;
        }

        [HttpPatch]
        [ProducesResponseType(typeof(AccountManagerDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromBody]AccountManagerUpdateInput input)
        {
            var result = await _service.Update<AccountManagerDetailsOutput>(_principal, input);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(AccountManagerDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var result = await _service.GetById<AccountManagerDetailsOutput>(_principal.Id);
            return Ok(result);
        }
    }

    [ApiExplorerSettings(IgnoreApi = false)]
    [Route("project-manager")]
    [Produces("application/json")]
    public class ProjectManagerController : ControllerBase
    {
        private readonly IProjectManager _projectManager;
        private readonly IProjectManagerService _service;

        public ProjectManagerController(IProjectManager projectManager, IProjectManagerService service)
        {
            _service = service;
            _projectManager = projectManager;
        }

        [HttpPatch]
        [ProducesResponseType(typeof(ProjectManagerDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromBody]ProjectManagerUpdateInput input)
        {
            var result = await _service.Update<ProjectManagerDetailsOutput>(_projectManager, input);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ProjectManagerDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var result = await _service.GetById<ProjectManagerDetailsOutput>(_projectManager.Id);
            return Ok(result);
        }
    }

    [ApiExplorerSettings(IgnoreApi = false)]
    [Route("marketer")]
    [Produces("application/json")]
    public class MarketerController : ControllerBase
    {
        private readonly IMarketer _marketer;
        private readonly IMarketerService _marketerService;

        public MarketerController(IMarketer marketer, IMarketerService marketerService)
        {
            _marketerService = marketerService;
            _marketer = marketer;
        }


        [HttpPatch]
        [ProducesResponseType(typeof(MarketerDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromBody]MarketerUpdateInput input)
        {
            var result = await _marketerService.Update<MarketerDetailsOutput>(_marketer, input);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(MarketerDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var result = await _marketerService.GetById<MarketerDetailsOutput>(_marketer.Id);
            return Ok(result);
        }
    }

    [ApiExplorerSettings(IgnoreApi = false)]
    [Route("recruiter")]
    [Produces("application/json")]
    public class RecruiterController : ControllerBase
    {
        private readonly IRecruiter _recruiter;
        private readonly IRecruiterService _service;

        public RecruiterController(IRecruiter recruiter, IRecruiterService service)
        {
            _service = service;
            _recruiter = recruiter;
        }

        [HttpPatch]
        [ProducesResponseType(typeof(RecruiterDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromBody]RecruiterUpdateInput input)
        {
            var result = await _service.Update<RecruiterDetailsOutput>(_recruiter, input);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(RecruiterDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var result = await _service.GetById<RecruiterDetailsOutput>(_recruiter.Id);
            return Ok(result);
        }
    }

    [ApiExplorerSettings(IgnoreApi = false)]
    [Route("contractor")]
    [Produces("application/json")]
    public class ContractorController : ControllerBase
    {
        private readonly IContractor _principal;
        private readonly IContractorService _service;

        public ContractorController(IContractor principal, IContractorService service)
        {
            _service = service;
            _principal = principal;
        }

        /// <summary>
        /// updates a contractor profile
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPatch]
        [ProducesResponseType(typeof(ContractorDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromBody] ContractorUpdateInput input)
        {
            var result = await _service.Update<ContractorDetailsOutput>(_principal, input);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ContractorDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var result = await _service.GetById<ContractorDetailsOutput>(_principal.Id);
            return Ok(result);
        }
    }
}