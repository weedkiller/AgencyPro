// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AgencyPro.Core.Contracts.Enums;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Proposals.Enums;
using AgencyPro.Core.Stories.Enums;
using AgencyPro.Core.Stories.Events;
using AgencyPro.Core.Stories.Models;
using AgencyPro.Core.TimeEntries.Enums;
using AgencyPro.Core.TimeEntries.Events;
using AgencyPro.Core.TimeEntries.Models;
using AgencyPro.Core.TimeEntries.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Omu.ValueInjecter;

namespace AgencyPro.Services.TimeEntries
{
    public partial class TimeEntryService
    {
        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[{nameof(TimeEntryService)}.{callerName}] - {message}";
        }

        private async Task<TimeEntryResult> TrackTime(TimeEntry entry, Guid? storyId, bool? isStoryComplete)
        {
            _logger.LogInformation(GetLogMessage("Tracking time entry..."));

            var retVal = new TimeEntryResult();

            var contract = await _contractService.Repository.Queryable()
                .Include(x=>x.Project)
                .ThenInclude(x=>x.CustomerAccount)
                .Include(x => x.Project)
                .ThenInclude(x => x.Proposal)
                .Include(x=>x.ProviderOrganization)
                .ThenInclude(x=>x.Organization)
                .Include(x=>x.MarketerOrganization)
                .ThenInclude(x=>x.Organization)
                .Include(x=>x.RecruiterOrganization)
                .ThenInclude(x=>x.Organization)
                .Include(x=>x.OrganizationContractor)
                .Where(x => x.Id == entry.ContractId)
                .FirstOrDefaultAsync();

            if (contract.Status == ContractStatus.Paused || contract.Status == ContractStatus.Inactive)
            {
                retVal.ErrorMessage = "You cannot log time if contract is paused or ended";
                _logger.LogInformation(GetLogMessage(retVal.ErrorMessage));
                return retVal;
            }

            if (contract.Project.Proposal == null)
            {
                retVal.ErrorMessage = "Proposal not found for the project";
                _logger.LogInformation(GetLogMessage(retVal.ErrorMessage));
                return retVal;
            }

            if (contract.Project.Proposal.Status != ProposalStatus.Accepted)
            {
                retVal.ErrorMessage = "Proposal not accepted for the project";
                _logger.LogInformation(GetLogMessage(retVal.ErrorMessage));
                return retVal;
            }

            var autoApproveProject = contract.Project.AutoApproveTimeEntries;
            var autoApproveContractor = contract.OrganizationContractor.AutoApproveTimeEntries;
            var autoApprove = autoApproveProject || autoApproveContractor;

            entry.ProviderAgencyOwnerId = contract.ProviderOrganization.Organization.CustomerId;
            entry.MarketingAgencyOwnerId = contract.MarketerOrganization.Organization.CustomerId;
            entry.RecruitingAgencyOwnerId = contract.RecruiterOrganization.Organization.CustomerId;

            entry.ProviderOrganizationId = contract.ContractorOrganizationId;
            entry.MarketingOrganizationId = contract.MarketerOrganizationId;
            entry.RecruitingOrganizationId = contract.RecruiterOrganizationId;

            entry.ContractorId = contract.ContractorId;
            entry.MarketerId = contract.MarketerId;
            entry.RecruiterId = contract.RecruiterId;
            entry.ProjectManagerId = contract.ProjectManagerId;
            entry.AccountManagerId = contract.AccountManagerId;
            entry.ProjectId = contract.ProjectId;
            entry.CustomerId = contract.Project.CustomerId;
            entry.CustomerOrganizationId = contract.Project.CustomerOrganizationId;
            entry.Status = autoApprove ? TimeStatus.Approved : TimeStatus.Logged;
            entry.StoryId = storyId;
            entry.InstantAccountManagerStream = contract.AccountManagerStream;
            entry.InstantProjectManagerStream = contract.ProjectManagerStream;
            entry.InstantRecruiterStream = contract.RecruiterStream;
            entry.InstantMarketerStream = contract.MarketerStream;
            entry.InstantContractorStream = contract.ContractorStream;
            entry.InstantSystemStream = contract.SystemStream;
            entry.InstantAgencyStream = contract.AgencyStream;
            entry.InstantRecruitingAgencyStream = contract.RecruitingAgencyStream;
            entry.InstantMarketingAgencyStream = contract.MarketingAgencyStream;

            entry.UpdatedById = _userInfo.UserId;
            entry.CreatedById = _userInfo.UserId;

            if (autoApprove)
            {
                entry.StatusTransitions.Add(new TimeEntryStatusTransition()
                {
                    Status = TimeStatus.Approved,
                    ObjectState = ObjectState.Added
                });
            }
            else
            {
                entry.StatusTransitions.Add(new TimeEntryStatusTransition()
                {
                    Status = TimeStatus.Logged,
                    ObjectState = ObjectState.Added
                });
            }
          

            var records = await Repository.InsertAsync(entry, true);

            
            _logger.LogDebug(GetLogMessage("{0} records updated"), records);


            if (records > 0)
            {
                bool isStoryCompleted = false;

                if (storyId.HasValue)
                {
                    _logger.LogDebug(GetLogMessage("Time entry has story: {0}"), storyId.Value);

                    var story = await _storyService.Repository
                        .Queryable()
                        .FirstOrDefaultAsync(x => x.Id == storyId.Value);

                    if (isStoryComplete.HasValue)
                    {
                        if (isStoryComplete.Value && story.Status != StoryStatus.Completed)
                        {
                            _logger.LogDebug(GetLogMessage("Story was completed"));

                            story.Status = StoryStatus.Completed;
                            story.StatusTransitions.Add(new StoryStatusTransition()
                            {
                                Status = StoryStatus.Completed,
                                ObjectState = ObjectState.Added
                            });

                            isStoryCompleted = true;

                        }
                    }
                    else
                    {
                        _logger.LogDebug(GetLogMessage("Story was not completed"));
                        if (story.Status != StoryStatus.InProgress)
                        {
                            story.Status = StoryStatus.InProgress;
                            story.StatusTransitions.Add(new StoryStatusTransition()
                            {
                                Status = StoryStatus.InProgress,
                                ObjectState = ObjectState.Added
                            });
                        }

                    }

                    story.TotalHoursLogged += entry.TotalHours;
                    story.ObjectState = ObjectState.Modified;
                    story.Updated = DateTimeOffset.UtcNow;

                    var result = _storyService.Repository.InsertOrUpdateGraph(story, true);

                    _logger.LogDebug(GetLogMessage("{0} records updated in db"), result);

                    if (result > 0)
                    {
                        retVal.Succeeded = true;
                        retVal.TimeEntryId = entry.Id;

                        await Task.Run(() =>
                        {
                            RaiseEvent(new TimeEntryLoggedEvent()
                            {
                                TimeEntryId = entry.Id
                            });
                        });

                        if (isStoryCompleted)
                        {
                            await Task.Run(() => RaiseEvent(new StoryCompletedEvent()
                            {
                                StoryId = storyId.Value
                            }));
                        }
                    }
                }
                else
                {
                    retVal.Succeeded = true;
                    retVal.TimeEntryId = entry.Id;
                }
            }
          
            return retVal;
        }

        public async Task<TimeEntryResult> TrackTime(IOrganizationContractor contractor, TimeTrackingModel model ) 
        {
            var entry = new TimeEntry
            {
                StartDate = model.StartDateTime,
                EndDate = model.StartDateTime.AddMinutes(model.Duration),
                ContractId = model.ContractId,

            }.InjectFrom(model) as TimeEntry;

            return await TrackTime(entry, model.StoryId, model.CompleteStory);

        }

        public async Task<TimeEntryResult> TrackDay(IOrganizationContractor contractor, DayTimeTrackingModel model)
        {

            var entry = new TimeEntry
            {
                StartDate = model.StartDateTime,
                EndDate = model.StartDateTime.AddMinutes(480),
                ContractId = model.ContractId,
            }.InjectFrom(model) as TimeEntry;

            return await TrackTime(entry, model.StoryId, model.CompleteStory);
        }

        public async Task<TimeEntryResult> TrackHalfDay(IOrganizationContractor contractor, DayTimeTrackingModel model)
        {
            var entry = new TimeEntry
            {
                StartDate = model.StartDateTime,
                EndDate = model.StartDateTime.AddMinutes(240),
                ContractId = model.ContractId
            }.InjectFrom(model) as TimeEntry;

            return await TrackTime(entry, model.StoryId, model.CompleteStory);

        }
    }
}