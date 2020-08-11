using IdeaFortune.Core.EmailSending.Services;
using IdeaFortune.Core.Leads.ViewModels;
using IdeaFortune.Core.Metadata;
using IdeaFortune.Core.UrlHelpers;
using Microsoft.AspNetCore.Html;

namespace IdeaFortune.Mailer.TagHelpers
{
    public static class LeadHelpers
    {
        public static IHtmlContent LeadCreated<T>(this IBasicEmail email, T model) where T: LeadOutput, new()
        {
            var role = FlowExtensions.GetRole(typeof(T));

            var message = "";
            var streamName = "";
            var stream = 0m;
            var bonus = 0m;
            var bonusName = "";

            switch (role)
            {
                case FlowRoleToken.Marketer:
                    bonus = model.MarketerBonus;
                    stream = model.MarketerStream;
                    streamName = "MA Stream";
                    bonusName = "MA Bonus";
                    message = "A new lead was created";
                    break;

                case FlowRoleToken.MarketingAgencyOwner:
                    bonus = model.MarketingAgencyBonus;
                    stream = model.MarketingAgencyStream;
                    streamName = "MAO Stream";
                    bonusName = "MAO Bonus";
                    message = "A new lead was created";
                    break;

                case FlowRoleToken.AgencyOwner:
                    bonus = model.MarketingBonus;
                    stream = model.MarketingStream;
                    streamName = "Marketing Stream";
                    bonusName = "Marketing Bonus";
                    message = "You have a new lead";
                    break;

            }

            var link = email.GetLeadUrl(model);

            var contractDetails = model.LeadDetails(stream, streamName, bonus, bonusName);

            return TemplateHelpers.ThreeParagraph(message, link, contractDetails);
        }
        public static IHtmlContent LeadQualified<T>(this IBasicEmail email,T model) where T : LeadOutput, new()
        {
            var role = FlowExtensions.GetRole(typeof(T));

            var message = "";
            var streamName = "";
            var stream = 0m;
            var bonus = 0m;
            var bonusName = "";
            
            switch (role)
            {
                case FlowRoleToken.Marketer:
                    bonus = model.MarketerBonus;
                    stream = model.MarketerStream;
                    streamName = "MA Stream";
                    bonusName = "MA Bonus";
                    message = "Your lead was qualified";
                    break;
                    
                case FlowRoleToken.MarketingAgencyOwner:
                    bonus = model.MarketingAgencyBonus;
                    stream = model.MarketingAgencyStream;
                    streamName = "MAO Stream";
                    bonusName = "MAO Bonus";
                    message = "Your marketer's lead was qualified";
                    break;
                case FlowRoleToken.AgencyOwner:
                    bonus = model.MarketingBonus;
                    stream = model.MarketingStream;
                    streamName = "Marketing Stream";
                    bonusName = "Marketing Bonus";
                    message = "Your lead was successfully assigned";
                    break;
                case FlowRoleToken.AccountManager:
                    bonus = model.MarketingBonus;
                    stream = model.MarketingStream;
                    streamName = "Marketing Stream";
                    bonusName = "Marketing Bonus";
                    message = "A new Opportunity was assigned to you.";
                    break;
                case FlowRoleToken.None:
                    break;
               
            }

            var link = email.GetLeadUrl(model);

            var contractDetails = model.LeadDetails(stream, streamName, bonus, bonusName);

            return TemplateHelpers.ThreeParagraph(message, link, contractDetails);
            
        }

        public static IHtmlContent LeadRejected<T>(this IBasicEmail email, T model) where T : LeadOutput, new()
        {
            var role = FlowExtensions.GetRole(typeof(T));

            var message = "";
            switch (role)
            {
                case FlowRoleToken.Marketer:
                    message = "Your lead was rejected";
                    break;

                case FlowRoleToken.MarketingAgencyOwner:
                    message = "Your lead was rejected";
                    break;
                case FlowRoleToken.AgencyOwner:
                    message = "Your lead was rejected";
                    break;
                case FlowRoleToken.AccountManager:
                    message = "Your lead was rejected";
                    break;
            }

            var link = email.GetLeadUrl(model);

            return TemplateHelpers.TwoParagraph(message, link);
        }

        public static IHtmlContent LeadPromoted<T>(this IBasicEmail email, T model) where T : LeadOutput, new()
        {
            var role = FlowExtensions.GetRole(typeof(T));

            var message = "";
            var streamName = "";
            var stream = 0m;
            var bonus = 0m;
            var bonusName = "";

            switch (role)
            {
                case FlowRoleToken.Marketer:
                    bonus = model.MarketerBonus;
                    stream = model.MarketerStream;
                    streamName = "MA Stream";
                    bonusName = "MA Bonus";
                    message = "Your lead was promoted";
                    break;

                case FlowRoleToken.MarketingAgencyOwner:
                    bonus = model.MarketingAgencyBonus;
                    stream = model.MarketingAgencyStream;
                    streamName = "MAO Stream";
                    bonusName = "MAO Bonus";
                    message = "Your marketer's lead was promoted";
                    break;
                case FlowRoleToken.AgencyOwner:
                    bonus = model.MarketingAgencyBonus;
                    stream = model.MarketingAgencyStream;
                    streamName = "MAO Stream";
                    bonusName = "MAO Bonus";
                    message = "Your lead was promoted";
                    break;
                case FlowRoleToken.AccountManager:
                    bonus = model.MarketingAgencyBonus;
                    stream = model.MarketingAgencyStream;
                    streamName = "MA Stream";
                    bonusName = "MA Bonus";
                    message = "Your lead was promoted.";
                    break;
                case FlowRoleToken.None:
                    break;

            }

            var link = email.GetLeadUrl(model);

            var contractDetails = model.LeadDetails(stream, streamName, bonus, bonusName);

            return TemplateHelpers.ThreeParagraph(message, link, contractDetails);

        }

    }
}