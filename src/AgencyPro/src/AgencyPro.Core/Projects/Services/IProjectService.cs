// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.Common;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Projects.Filters;
using AgencyPro.Core.Projects.Models;
using AgencyPro.Core.Projects.ViewModels;
using AgencyPro.Core.Services;

namespace AgencyPro.Core.Projects.Services
{
    public interface IProjectService : IService<Project>
    {
        Task<ProjectResult> CreateProject(IProviderAgencyOwner ao, ProjectInput input);
        Task<ProjectResult> CreateProject(IOrganizationAccountManager am, ProjectInput input);
        Task<ProjectResult> CreateProject(IOrganizationProjectManager pm, ProjectInput input);

        Task<PackedList<T>> GetProjects<T>(IOrganizationAccountManager am, ProjectFilters filters) where T : AccountManagerProjectOutput, new();
        Task<PackedList<T>> GetProjects<T>(IOrganizationProjectManager pm, ProjectFilters filters) where T : ProjectManagerProjectOutput, new();
        Task<PackedList<T>> GetProjects<T>(IOrganizationCustomer cu, ProjectFilters filters) where T : CustomerProjectOutput, new();
        Task<PackedList<T>> GetProjects<T>(IProviderAgencyOwner ao, ProjectFilters filters) where T : AgencyOwnerProjectOutput, new();
        Task<PackedList<T>> GetProjects<T>(IOrganizationContractor co, ProjectFilters filters) where T : ContractorProjectOutput, new();

        Task<T> GetProject<T>(Guid projectId) where T : ProjectOutput, new();
        Task<T> GetProject<T>(IProviderAgencyOwner ao, Guid projectId) where T : AgencyOwnerProjectOutput, new();
        Task<T> GetProject<T>(IOrganizationAccountManager am, Guid projectId) where T : AccountManagerProjectOutput, new();
        Task<T> GetProject<T>(IOrganizationProjectManager pm, Guid projectId) where T : ProjectManagerProjectOutput, new();
        Task<T> GetProject<T>(IOrganizationContractor co, Guid projectId) where T : ContractorProjectOutput, new();
        Task<T> GetProject<T>(IOrganizationCustomer cu, Guid projectId) where T : CustomerProjectOutput, new();

        Task<ProjectResult> UpdateProject(IProviderAgencyOwner ao, Guid projectId, UpdateProjectInput model);
        Task<ProjectResult> UpdateProject(IOrganizationAccountManager am, Guid projectId, UpdateProjectInput model);
        Task<ProjectResult> UpdateProject(IOrganizationProjectManager co, Guid projectId, UpdateProjectInput model);

        Task<ProjectResult> DeleteProject(IProviderAgencyOwner agencyOwner, Guid projectId);

        Task<List<T>> GetProjects<T>(IProviderAgencyOwner owner, Guid[] uniqueProjectIds) 
            where T : AgencyOwnerProjectOutput, new();

        Task<List<T>> GetProjects<T>(IOrganizationCustomer cu, Guid[] uniqueProjectIds) 
            where T : CustomerProjectOutput, new();

        Task<List<T>> GetProjects<T>(IOrganizationAccountManager am, Guid[] uniqueProjectIds)
            where T : AccountManagerProjectOutput, new();

        Task<List<T>> GetProjects<T>(IOrganizationProjectManager pm, Guid[] uniqueProjectIds) 
            where T : ProjectManagerProjectOutput, new();

        Task<List<T>> GetProjects<T>(IOrganizationContractor co, Guid[] uniqueProjectIds) 
            where T : ContractorProjectOutput, new();

        Task<ProjectResult> PauseProject(IOrganizationCustomer cu, Guid projectId);
        Task<ProjectResult> PauseProject(IOrganizationAccountManager am, Guid projectId);
        Task<ProjectResult> PauseProject(IProviderAgencyOwner ao, Guid projectId);

        Task<ProjectResult> RestartProject(IProviderAgencyOwner ao, Guid projectId);
        Task<ProjectResult> RestartProject(IOrganizationCustomer cu, Guid projectId);
        Task<ProjectResult> RestartProject(IOrganizationAccountManager am, Guid projectId);

        Task<ProjectResult> EndProject(IProviderAgencyOwner ao, Guid projectId);
        Task<ProjectResult> EndProject(IOrganizationCustomer cu, Guid projectId);
        Task<ProjectResult> EndProject(IOrganizationAccountManager am, Guid projectId);

        

    }
}