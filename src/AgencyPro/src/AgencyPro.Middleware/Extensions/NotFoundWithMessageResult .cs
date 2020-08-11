// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Net;

namespace AgencyPro.Middleware.Extensions
{
    /// <summary>
    /// </summary>
    public class NotFoundWithMessageResult : OutputWithMessageResult
    {
        /// <summary>
        /// </summary>
        /// <param name="message"></param>
        public NotFoundWithMessageResult(string message) : base(message, HttpStatusCode.NotFound)
        {
        }
    }
}