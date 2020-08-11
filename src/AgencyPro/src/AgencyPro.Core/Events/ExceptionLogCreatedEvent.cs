// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace AgencyPro.Core.Events
{
    public class ExceptionLogCreatedEvent : ExceptionLogEvent
    {
        public ExceptionLogCreatedEvent(ExceptionLog.ExceptionLog err)
        {
            Exception = err;
        }
    }
}