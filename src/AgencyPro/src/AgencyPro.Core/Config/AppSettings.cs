// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace AgencyPro.Core.Config
{
    public class AppSettings
    {
        public string Scope { get; set; }
        public AppInformation Information { get; set; }
        public StripeSettings Stripe { get; set; }
        public PandaDocsSettings PandaDocs { get; set; }
        public AppBaseUrls Urls { get; set; }
        public AuthSettings Auth { get; set; }
        public TwilioSettings Twilio { get; set; }
        public EmailSettings Email { get; set; }
        

        public AppSettings()
        {
            Urls = new AppBaseUrls();
        }
    }
}