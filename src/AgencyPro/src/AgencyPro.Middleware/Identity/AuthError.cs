// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Newtonsoft.Json;

namespace AgencyPro.Middleware.Identity
{
    /// <summary>
    ///     Authentication error response object
    /// </summary>
    public class AuthError
    {
        /// <summary>
        ///     Gets or sets the error description.
        /// </summary>
        /// <value>
        ///     The error description.
        /// </value>
        [JsonProperty("error_description")]
        public string Description { get; set; }

        /// <summary>
        ///     Gets or sets the error.
        /// </summary>
        /// <value>
        ///     The error.
        /// </value>
        [JsonProperty("error")]
        public string Error { get; set; }
    }
}