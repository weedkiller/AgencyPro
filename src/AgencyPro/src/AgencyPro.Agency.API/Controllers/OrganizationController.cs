using System;
using System.Threading.Tasks;
using IdeaFortune.Core.Organizations.Services;
using IdeaFortune.Core.Organizations.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdeaFortune.Agency.API.Controllers
{
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;

        public OrganizationController(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        /// <summary>
        /// Gets an organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpGet("{organizationId}")]
        [ProducesResponseType(typeof(OrganizationOutput), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetOrganization([FromRoute]Guid organizationId)
        {
            var org = await _organizationService.Get(organizationId);

            return Ok(org);
        }
    }
}
