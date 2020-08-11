// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Threading.Tasks;
using AgencyPro.Core.Roles.Services;
using AgencyPro.Core.Roles.ViewModels.ProjectManagers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.Person.API.Controllers.v2
{
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
}