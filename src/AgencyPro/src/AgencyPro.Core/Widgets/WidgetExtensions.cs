// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Extensions;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Widgets.Enums;
using AgencyPro.Core.Widgets.Models;
using System.Linq;

namespace AgencyPro.Core.Widgets
{
    public static class WidgetExtensions
    {
        public static IQueryable<CategoryWidget> WithAgencyOwnerFlags(this IQueryable<CategoryWidget> categories,
            IAgencyOwner ao)
        {
            return categories.Where(x => x.Widget.AccessFlag.HasFlag(WidgetAccess.AgencyOwner));
        }

        public static IQueryable<CategoryWidget> WithProjectManagerFlags(this IQueryable<CategoryWidget> categories,
            IAgencyOwner ao)
        {
            return categories.Where(x => x.Widget.AccessFlag.HasFlag(WidgetAccess.ProjectManager));
        }

        public static IQueryable<CategoryWidget> WithAccountManagerFlags(this IQueryable<CategoryWidget> categories,
            IAgencyOwner ao)
        {
            return categories.Where(x => x.Widget.AccessFlag.HasFlag(WidgetAccess.AccountManager));
        }

        public static IQueryable<CategoryWidget> WithContractorFlags(this IQueryable<CategoryWidget> categories,
            IAgencyOwner ao)
        {
            return categories.Where(x => x.Widget.AccessFlag.HasFlag(WidgetAccess.Contractor));
        }

        public static IQueryable<CategoryWidget> WithRecruiterFlags(this IQueryable<CategoryWidget> categories,
            IAgencyOwner ao)
        {
            return categories.Where(x => x.Widget.AccessFlag.HasFlag(WidgetAccess.Recruiter));
        }

        public static IQueryable<CategoryWidget> WithMarketerFlags(this IQueryable<CategoryWidget> categories,
            IAgencyOwner ao)
        {
            return categories.Where(x => x.Widget.AccessFlag.HasFlag(WidgetAccess.Marketer));
        }

        public static IQueryable<CategoryWidget> WithCustomerFlags(this IQueryable<CategoryWidget> categories,
            IAgencyOwner ao)
        {
            return categories.Where(x => x.Widget.AccessFlag.HasFlag(WidgetAccess.Customer));
        }
    }
}