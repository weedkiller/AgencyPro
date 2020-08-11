// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Threading.Tasks;
using AgencyPro.Core.Organizations.Services;
using AgencyPro.Core.People.Services;
using AgencyPro.Core.People.ViewModels;
using AgencyPro.Core.UserAccount.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.Person.API.Controllers.v2
{
    [ApiExplorerSettings(IgnoreApi = false)]
    [Route("organization")]
    [Produces("application/json")]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationService _service;
        private readonly IPersonService _personService;
        private readonly IUserInfo _user;

        public OrganizationController(IOrganizationService service, IPersonService personService, IUserInfo user)
        {
            _service = service;
            _personService = personService;
            _user = user;
        }

        [HttpPost]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> SwitchOrganizations([FromBody] SwitchOrganizationInput model)
        {
            model.PersonId = _user.UserId;

            var result = await _personService.SwitchOrgs(model);

            return Ok(result);
        }
        
    }
}
