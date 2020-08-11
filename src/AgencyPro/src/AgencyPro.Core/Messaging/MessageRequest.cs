// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace AgencyPro.Core.Messaging
{
    /// <summary>
    ///     Message Request class
    /// </summary>
    public class MessageRequest
    {
        /// <summary>
        ///     Get or set message value
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     Get or set EventName
        /// </summary>
        public MessagingEvent EventName { get; set; }
    }
}