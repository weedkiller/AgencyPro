// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using AgencyPro.Core.Transfers.ViewModels;

namespace AgencyPro.Core.FinancialAccounts.ViewModels
{
    public class FinancialAccountDetails : FinancialAccountOutput
    {
        public ICollection<TransferOutput> Transfers { get; set; } 
    }
}