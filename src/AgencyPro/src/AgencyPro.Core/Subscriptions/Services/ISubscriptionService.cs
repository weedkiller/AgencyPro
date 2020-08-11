// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.Organizations.Models;
using Stripe;

namespace AgencyPro.Core.Subscriptions.Services
{
    public interface ISubscriptionService
    {
        Task<int> PullSubscription(Subscription subscription);
        string GetSubscriptionLevel(Organization organization);


        Task<int> ImportSubscriptions(int limit);
        Task<int> ExportSubscriptions();

        Task<int> PushSubscription(Guid organization, bool saveChanges);


        Task<int> ImportSubscriptionItem(SubscriptionItem item);
    }
}