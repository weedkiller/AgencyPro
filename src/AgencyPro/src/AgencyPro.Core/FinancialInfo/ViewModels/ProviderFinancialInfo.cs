// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace AgencyPro.Core.FinancialInfo.ViewModels
{
    public class ProviderFinancialInfo
    {
        public decimal ContractorHoursLogged { get; set; }
        public decimal RecruiterHoursLogged { get; set; }
        public decimal MarketerHoursLogged { get; set; }
        public decimal ProjectManagerHoursLogged { get; set; }
        public decimal AccountManagerHoursLogged { get; set; }

        public decimal ContractorDollarsLogged { get; set; }
        public decimal MarketerDollarsLogged { get; set; }
        public decimal RecruiterDollarsLogged { get; set; }
        public decimal ProjectManagerDollarsLogged { get; set; }
        public decimal AccountManagerDollarsLogged { get; set; }

        public decimal TotalHoursLogged => ContractorHoursLogged 
                                           + RecruiterHoursLogged
                                           + MarketerHoursLogged
                                           + ProjectManagerHoursLogged
                                           + AccountManagerHoursLogged;

        public decimal TotalDollarsLogged => ContractorDollarsLogged
                                             + MarketerDollarsLogged
                                             + RecruiterDollarsLogged
                                             + ProjectManagerDollarsLogged
                                             + AccountManagerDollarsLogged;

        public decimal ContractorHoursApproved { get; set; }
        public decimal RecruiterHoursApproved { get; set; }
        public decimal MarketerHoursApproved { get; set; }
        public decimal ProjectManagerHoursApproved { get; set; }
        public decimal AccountManagerHoursApproved { get; set; }

        public decimal ContractorDollarsApproved { get; set; }
        public decimal RecruiterDollarsApproved { get; set; }
        public decimal MarketerDollarsApproved { get; set; }
        public decimal ProjectManagerDollarsApproved { get; set; }
        public decimal AccountManagerDollarsApproved { get; set; }

        public decimal TotalHoursApproved => ContractorHoursApproved
                                           + RecruiterHoursApproved
                                           + MarketerHoursApproved
                                           + ProjectManagerHoursApproved
                                           + AccountManagerHoursApproved;

        public decimal TotalDollarsApproved => ContractorDollarsApproved
                                             + MarketerDollarsApproved
                                             + RecruiterDollarsApproved
                                             + ProjectManagerDollarsApproved
                                             + AccountManagerDollarsApproved;

        public decimal ContractorHoursInvoiced { get; set; }
        public decimal RecruiterHoursInvoiced { get; set; }
        public decimal MarketerHoursInvoiced { get; set; }
        public decimal ProjectManagerHoursInvoiced { get; set; }
        public decimal AccountManagerHoursInvoiced { get; set; }

        public decimal ContractorDollarsInvoiced { get; set; }
        public decimal RecruiterDollarsInvoiced { get; set; }
        public decimal MarketerDollarsInvoiced { get; set; }
        public decimal ProjectManagerDollarsInvoiced { get; set; }
        public decimal AccountManagerDollarsInvoiced { get; set; }

        public decimal TotalHoursInvoiced => ContractorHoursInvoiced
                                             + RecruiterHoursInvoiced
                                             + MarketerHoursInvoiced
                                             + ProjectManagerHoursInvoiced
                                             + AccountManagerHoursInvoiced;

        public decimal TotalDollarsInvoiced => ContractorDollarsInvoiced
                                               + RecruiterDollarsInvoiced
                                               + MarketerDollarsInvoiced
                                               + ProjectManagerDollarsInvoiced
                                               + AccountManagerDollarsInvoiced;

       


    }
}
