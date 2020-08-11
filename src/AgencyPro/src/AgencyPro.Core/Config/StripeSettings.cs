// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace AgencyPro.Core.Config
{
    public class StripeSettings
    {
        public string SecretApiKey { get; set; }
        public string PublicApiKey { get; set; }

        public string ClientId { get; set; }
        public string RedirectUri { get; set; }
        public string WebhookSigningKey { get; set; }
    }
}