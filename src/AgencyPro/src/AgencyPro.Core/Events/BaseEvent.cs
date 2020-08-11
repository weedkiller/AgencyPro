// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Config;
using AgencyPro.Core.Enums;
using AgencyPro.Core.EventHandling;

namespace AgencyPro.Core.Events
{
    public abstract class BaseEvent : IEvent
    {
       
        public ModelType ModelType { get; set; }
        public ModelAction Action { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public AppInformation AppInfo { get; set; }
        public AppBaseUrls Urls { get; set; }
    }
}