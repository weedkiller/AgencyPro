// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.ComponentModel;

namespace AgencyPro.Core.Messaging
{
    public enum MessagingEvent
    {
        /// <summary>
        ///     Unknown enum
        /// </summary>
        Unknown = 0,

        /// <summary>
        ///     On Message Listened(onMessageListened) enum value. For general purpose
        /// </summary>
        [Description("onMessageListened")] OnMessageListened = 1,

        /// <summary>
        ///     On Inserted(onInserted) enum value.
        /// </summary>
        [Description("onInserted")] OnInserted = 2,

        /// <summary>
        ///     On Deleted(onDeleted) enum value.
        /// </summary>
        [Description("onDeleted")] OnDeleted = 3,

        /// <summary>
        ///     On Updated(onUpdated) enum value.
        /// </summary>
        [Description("onUpdated")] OnUpdated = 4,

        /// <summary>
        ///     On Exception(onException) enum value.
        /// </summary>
        [Description("onException")] OnException = 5
    }
}