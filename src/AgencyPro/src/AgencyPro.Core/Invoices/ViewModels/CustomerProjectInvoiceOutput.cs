// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Metadata;

namespace AgencyPro.Core.Invoices.ViewModels
{
    [FlowDirective(FlowRoleToken.Customer, "invoices")]
    public class CustomerProjectInvoiceOutput : ProjectInvoiceOutput
    {
        public override Guid TargetOrganizationId => this.ProviderOrganizationId;

    }
}