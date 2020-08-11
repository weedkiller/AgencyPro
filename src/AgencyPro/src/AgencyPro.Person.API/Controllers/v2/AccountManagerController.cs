// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Threading.Tasks;
using AgencyPro.Core.Roles.Services;
using AgencyPro.Core.Roles.ViewModels.AccountManagers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.Person.API.Controllers.v2
{
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
}