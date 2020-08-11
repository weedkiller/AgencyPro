// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.ActivityFeed.Services;
using AgencyPro.Core.ActivityFeed.ViewModels;
using AgencyPro.Core.Notifications.Models;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgencyPro.Services.ActivityFeed
{
    public class ActivityFeedService : Service<Notification>, IActivityFeedService
    {
        protected readonly DbContextOptions<AppDbContext> DbContextOptions;

        public ActivityFeedService(

            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            DbContextOptions = serviceProvider
                .GetRequiredService<DbContextOptions<AppDbContext>>();
        }

        public List<ActivityFeedOutput> GetActivityFeed(IProviderAgencyOwner ao, ActivityFeedFilters filters)
        {
            var retVal = new List<ActivityFeedOutput>();
            
            Task<List<LeadNotification>> leadNotifications = null;
            Task<List<ContractNotification>> contractNotifications = null;
            Task<List<CandidateNotification>> candidateNotifications = null;
            Task<List<ProposalNotification>> proposalNotifications = null;
            Task<List<ProjectNotification>> projectNotifications = null;

            using (var context = new AppDbContext(DbContextOptions))
            {
                if (filters.Type.Contains(NotificationType.Lead))
                {
                    var query = context.LeadNotifications
                        .Where(x => x.UserId == ao.CustomerId && x.OrganizationId == ao.OrganizationId &&
                                    x.Created > filters.MaxDate);

                    if (filters.LeadId.HasValue)
                    {
                        query = query.Where(x => x.LeadId == filters.LeadId.Value);
                    }

                    leadNotifications = query.ToListAsync();
                }

                if (filters.Type.Contains(NotificationType.Candidate))
                {
                    var query = context.CandidateNotifications
                        .Where(x => x.UserId == ao.CustomerId && x.OrganizationId == ao.OrganizationId && x.Created > filters.MaxDate);

                    if (filters.CandidateId.HasValue)
                    {
                        query = query.Where(x => x.CandidateId == filters.CandidateId.Value);
                    }

                    candidateNotifications = query.ToListAsync();
                }

                if (filters.Type.Contains(NotificationType.Contract))
                {
                    var query = context.ContractNotifications
                        .Where(x => x.UserId == ao.CustomerId && x.OrganizationId == ao.OrganizationId &&
                                    x.Created > filters.MaxDate);

                    if (filters.ContractId.HasValue)
                    {
                        query = query.Where(x => x.ContractId == filters.ContractId.Value);
                    }

                    contractNotifications = query.ToListAsync();
                }

                if (filters.Type.Contains(NotificationType.Proposal))
                {
                    var query = context.ProposalNotifications
                        .Where(x => x.UserId == ao.CustomerId && x.OrganizationId == ao.OrganizationId &&
                                    x.Created > filters.MaxDate);
                    
                    if (filters.ProposalId.HasValue)
                    {
                        query = query.Where(x => x.Id == filters.ProposalId.Value);
                    }
                    else if (filters.ProjectId.HasValue)
                    {
                        {
                            query = query.Where(x => x.Id == filters.ProjectId.Value);
                        }
                    }

                    proposalNotifications = query.ToListAsync();
                }

                if (filters.Type.Contains(NotificationType.Project))
                {
                    var query = context.ProjectNotifications
                        .Where(x => x.UserId == ao.CustomerId && x.OrganizationId == ao.OrganizationId &&
                                    x.Created > filters.MaxDate);
                    
                    if (filters.ProjectId.HasValue) query = query.Where(x => x.Id == filters.ProjectId.Value);

                    projectNotifications = query.ToListAsync();
                }

                Task.WaitAll();

                if (filters.Type.Contains(NotificationType.Lead))
                {
                    foreach (var item in leadNotifications.Result)
                    {
                        retVal.Add(new ActivityFeedOutput()
                        {
                            Url = item.Url,
                            Message = item.Message,
                            Created = item.Created
                        });
                    }
                }

                if (filters.Type.Contains(NotificationType.Contract))
                {
                    foreach (var item in contractNotifications.Result)
                    {
                        retVal.Add(new ActivityFeedOutput()
                        {
                            Url = item.Url,
                            Message = item.Message,
                            Created = item.Created
                        });
                    }
                }

                if (filters.Type.Contains(NotificationType.Candidate))
                {
                    foreach (var item in candidateNotifications.Result)
                    {
                        retVal.Add(new ActivityFeedOutput()
                        {
                            Url = item.Url,
                            Message = item.Message,
                            Created = item.Created
                        });
                    }
                }

                if (filters.Type.Contains(NotificationType.Proposal))
                {
                    foreach (var item in proposalNotifications.Result)
                    {
                        retVal.Add(new ActivityFeedOutput()
                        {
                            Url = item.Url,
                            Message = item.Message,
                            Created = item.Created
                        });
                    }
                }

                if (filters.Type.Contains(NotificationType.Project))
                {
                    foreach (var item in projectNotifications.Result)
                    {
                        retVal.Add(new ActivityFeedOutput()
                        {
                            Url = item.Url,
                            Message = item.Message,
                            Created = item.Created
                        });
                    }
                }
            }

            return retVal.OrderByDescending(x => x.Created).ToList();
        }

    }
}
