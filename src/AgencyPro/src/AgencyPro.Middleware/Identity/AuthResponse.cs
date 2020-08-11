// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using Newtonsoft.Json;

namespace AgencyPro.Middleware.Identity
{
    public class AuthResponse
    {
        [JsonProperty("access_token")] public string AccessToken { get; set; }

        [JsonProperty("token_type")] public string TokenType { get; set; }

        [JsonProperty("expires_in")] public int ExpiresIn { get; set; }

        [JsonProperty("username")] public string Username { get; set; }

        [JsonProperty("role")] public string Role { get; set; }

        [JsonProperty(".issued")] public DateTime Issued { get; set; }

        [JsonProperty(".expires")] public DateTime Expires { get; set; }

        [JsonProperty("refresh_token")] public string RefreshToken { get; set; }

        [JsonProperty("as:client_id")] public string AsClientId { get; set; }
    }
}