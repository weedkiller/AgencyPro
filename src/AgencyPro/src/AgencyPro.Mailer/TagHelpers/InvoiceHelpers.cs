using IdeaFortune.Core.DisperseFunds.Emails;
using IdeaFortune.Core.EmailSending.Services;
using IdeaFortune.Core.Invoices.ViewModels;
using IdeaFortune.Core.Metadata;
using IdeaFortune.Core.UrlHelpers;
using Microsoft.AspNetCore.Html;

namespace IdeaFortune.Mailer.TagHelpers
{
    public static class InvoiceHelpers
    {
        public static IHtmlContent InvoiceCreated<T>(this IBasicEmail email, T model) 
            where T : ProjectInvoiceOutput,new()
        {
            var role = FlowExtensions.GetRole(typeof(T));
            var message = "";
            switch (role)
            {
                case FlowRoleToken.AccountManager:
                case FlowRoleToken.AgencyOwner:
                    message = $"A new invoice was created";
                    break;
            }

            var link = email.GetInvoiceUrl(model);

            return TemplateHelpers.TwoParagraph(message, link);
        }

        public static IHtmlContent InvoiceFinalized<T>(this IBasicEmail email, T model)
            where T : ProjectInvoiceOutput,new()
        {
            var role = FlowExtensions.GetRole(typeof(T));

            var message = "";
            switch (role)
            {
                case FlowRoleToken.Customer:
                    message = $"You have a new invoice from {model.ProviderOrganizationName}";
                    break;
                case FlowRoleToken.AccountManager:
                case FlowRoleToken.AgencyOwner:
                    message = $"Invoice: {model.Number} was sent to {model.CustomerOrganizationName}";
                    break;
            }

            var link = email.GetInvoiceUrl(model);

            return TemplateHelpers.TwoParagraph(message, link);
        }

        public static IHtmlContent InvoicePaid<T>(this IBasicEmail email, T model)
            where T : ProjectInvoiceOutput,new()
        {
            var role = FlowExtensions.GetRole(typeof(T));

            var message = "";
            switch (role)
            {
                case FlowRoleToken.Customer:
                    message = $"Invoice has been paid";
                    break;
                case FlowRoleToken.AccountManager:
                case FlowRoleToken.AgencyOwner:
                    message = $"Invoice paid";
                    break;
            }
            var link = email.GetInvoiceUrl(model);

            return TemplateHelpers.TwoParagraph(message, link);
        }

        public static IHtmlContent InvoiceDispersedIndividual<T>(this IBasicEmail email, T model)
            where T : PersonTransferEmail, new()
        {
            var message = $"You just got paid! Project : " + model.ProjectName +
                " with Invoice No : " + model.Number + " & Transfer Amount : $" + model.Amount.ToString("N2");

            return TemplateHelpers.SingleParagraph(message);
        }

        public static IHtmlContent InvoiceDispersedOrganization<T>(this IBasicEmail email, T model)
            where T : OrganizationTransferEmail, new()
        {
            var message = $"Your organization : " + model.OrganizationName + " just got paid! Project : " + model.ProjectName +
                " with Invoice No : " + model.Number + " & Transfer Amount : $" + model.Amount.ToString("N2");

            return TemplateHelpers.SingleParagraph(message);
        }
    }
}