// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace AgencyPro.Core.ViewModels
{
    public sealed class StreamInput
    {
        public decimal AgencyStream { get; set; }
        public decimal AccountManagerStream { get; set; }
        public decimal ProjectManagerStream { get; set; }
        public decimal ContractorStream { get; set; }
        public decimal RecruiterStream { get; set; }
        public decimal MarketerStream { get; set; }
    }
}