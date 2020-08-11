using IdeaFortune.Core.EmailSending.Services;
using IdeaFortune.Core.Metadata;
using IdeaFortune.Core.Orders.ViewModels;
using IdeaFortune.Core.UrlHelpers;
using Microsoft.AspNetCore.Html;

namespace IdeaFortune.Mailer.TagHelpers
{
    public static class WorkOrderHelpers
    {
        public static IHtmlContent WorkOrderCreated<T>(this IBasicEmail email,
           T entry) where T : WorkOrderOutput, new()
        {
            var role = FlowExtensions.GetRole(typeof(T));
            var message = "";
            switch (role)
            {
                case FlowRoleToken.Customer:
                    message = $"You submitted a work order to {entry.AccountManagerOrganizationName}";
                    break;

                default:
                    message = "A new Work Order was created";
                    break;
            }


            var link = email.GetWorkOrderUrl(entry);

            var workEntryDetails = entry.WorkOrderDetails();

            return TemplateHelpers.ThreeParagraph(message, link, workEntryDetails);
        }
        public static IHtmlContent WorkOrderApproved<T>(this IBasicEmail email,
            T entry) where  T : WorkOrderOutput, new()
        {
            var role = FlowExtensions.GetRole(typeof(T));

            var message = "";
            switch (role)
            {
                
                default:
                    message = "A work order was approved";
                    break;


            }

            var link = email.GetWorkOrderUrl(entry);

            var workEntryDetails = entry.WorkOrderDetails();

            return TemplateHelpers.ThreeParagraph(message, link, workEntryDetails);
        }
        public static IHtmlContent WorkOrderRejected<T>(this IBasicEmail email,
            T entry) where T : WorkOrderOutput, new()
        {

            var role = FlowExtensions.GetRole(typeof(T));

            var message = "";
            switch (role)
            {
                default:
                    message = $"Your work order was rejected";
                    break;
            }

            var link = email.GetWorkOrderUrl(entry);

            var workEntryDetails = entry.WorkOrderDetails();

            return TemplateHelpers.ThreeParagraph(message, link, workEntryDetails);
        }
    }
}