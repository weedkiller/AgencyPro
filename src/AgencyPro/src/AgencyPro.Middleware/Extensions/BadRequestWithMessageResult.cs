// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Net;

namespace AgencyPro.Middleware.Extensions
{
    public class BadRequestWithMessageResult : OutputWithMessageResult
    {
        /// <summary>
        /// </summary>
        /// <param name="message"></param>
        public BadRequestWithMessageResult(string message) : base(message, HttpStatusCode.BadRequest)
        {
        }
    }
}