// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AutoMapper;
using AgencyPro.Core.Notifications.Models;
using AgencyPro.Core.Notifications.ViewModels;

namespace AgencyPro.Core.Notifications
{
    public class NotificationProjections : Profile
    {
        public NotificationProjections()
        {
            CreateMap<Notification, NotificationOutput>()
                .IncludeAllDerived();
        }
    }
}