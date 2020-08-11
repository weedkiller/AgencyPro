// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Candidates.ViewModels;
using AgencyPro.Core.Contracts.ViewModels;
using AgencyPro.Core.EmailSending.Services;
using AgencyPro.Core.Extensions;
using AgencyPro.Core.Invoices.ViewModels;
using AgencyPro.Core.Leads.ViewModels;
using AgencyPro.Core.Metadata;
using AgencyPro.Core.Orders.ViewModels;
using AgencyPro.Core.OrganizationPeople.ViewModels;
using AgencyPro.Core.Projects.ViewModels;
using AgencyPro.Core.Proposals.ViewModels;
using AgencyPro.Core.Stories.ViewModels;
using AgencyPro.Core.TimeEntries.ViewModels;

namespace AgencyPro.Core.UrlHelpers
{
    public static class FlowHelpers
    {
        public static string GetLeadUrl<T>(this IBasicEmail mail, T model) where T : LeadOutput, new()
        {
            var token = FlowExtensions.GetRole(typeof(T));
            var path = FlowExtensions.GetPath(typeof(T));

            return $"{mail.FlowUrl}{model.TargetOrganizationId}/{token.GetDescription()}/{path}/{model.Id}/leaddetails";

        }

        public static string GetCandidateUrl<T>(this IBasicEmail mail, T model) where T : CandidateOutput, new()
        {
            var token = FlowExtensions.GetRole(typeof(T));
            var path = FlowExtensions.GetPath(typeof(T));

            return $"{mail.FlowUrl}{model.TargetOrganizationId}/{token.GetDescription()}/{path}/{model.Id}/candidatedetails";

        }

        public static string GetContractUrl<T>(this IBasicEmail mail, T model) where T : ContractOutput, new()
        {
            var token = FlowExtensions.GetRole(typeof(T));
            var path = FlowExtensions.GetPath(typeof(T));

            return $"{mail.FlowUrl}{model.TargetOrganizationId}/{token.GetDescription()}/{path}/{model.Id}/contractdetails";


        }


        public static string GetProjectUrl<T>(this IBasicEmail mail, T model) where T : ProjectOutput, new()
        {
            var token = FlowExtensions.GetRole(typeof(T));
            var path = FlowExtensions.GetPath(typeof(T));

            return $"{mail.FlowUrl}{model.TargetOrganizationId}/{token.GetDescription()}/{path}/{model.Id}/projectdetails";


        }

        public static string GetStoryUrl<T>(this IBasicEmail mail, T model) where T : StoryOutput, new()
        {
            var token = FlowExtensions.GetRole(typeof(T));
            var path = FlowExtensions.GetPath(typeof(T));

            return $"{mail.FlowUrl}{model.TargetOrganizationId}/{token.GetDescription()}/{path}/{model.Id}/storydetails";


        }

        public static string GetTimeEntryUrl<T>(this IBasicEmail mail, T model) where T : TimeEntryOutput, new()
        {
            var token = FlowExtensions.GetRole(typeof(T));
            var path = FlowExtensions.GetPath(typeof(T));

            return $"{mail.FlowUrl}{model.TargetOrganizationId}/{token.GetDescription()}/{path}/{model.Id}/timedetails";


        }

        public static string GetWorkOrderUrl<T>(this IBasicEmail mail, T model) where T : WorkOrderOutput, new()
        {
            var token = FlowExtensions.GetRole(typeof(T));
            var path = FlowExtensions.GetPath(typeof(T));

            return $"{mail.FlowUrl}{model.TargetOrganizationId}/{token.GetDescription()}/{path}/{model.Id}/workorderdetails";


        }

        public static string GetProposalUrl<T>(this IBasicEmail mail, T model) where T : FixedPriceProposalOutput, new()
        {
            var token = FlowExtensions.GetRole(typeof(T));
            var path = FlowExtensions.GetPath(typeof(T));

            return $"{mail.FlowUrl}{model.TargetOrganizationId}/{token.GetDescription()}/{path}/{model.Id}/proposaldetails";
        }

        public static string GetPublicProposalUrl<T>(this IBasicEmail mail, T model) where T : FixedPriceProposalOutput, new()
        {
            return $"{mail.FlowUrl}/Proposal/Detail/{model.Id}";
        }

        public static string GetInvoiceUrl<T>(this IBasicEmail mail, T model) where T : ProjectInvoiceOutput, new()
        {
            var token = FlowExtensions.GetRole(typeof(T));
            var path = FlowExtensions.GetPath(typeof(T));

            return $"{mail.FlowUrl}{model.TargetOrganizationId}/{token.GetDescription()}/{path}/{model.Id}/invoicedetails";


        }

        public static string GetPersonUrl<T>(this IBasicEmail mail, T model) where T : OrganizationPersonOutput, new()
        {
            var token = FlowExtensions.GetRole(typeof(T));
            var path = FlowExtensions.GetPath(typeof(T));

            return $"{mail.FlowUrl}{model.TargetOrganizationId}/{token.GetDescription()}/people/{model.PersonId}/peopledetails";


        }
    }
}
