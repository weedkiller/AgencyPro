// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using AgencyPro.Core.Models;
using AgencyPro.Core.Notifications.Models;
using AgencyPro.Core.Orders.Model;
using AgencyPro.Core.Projects.Models;
using AgencyPro.Core.Proposals.Enums;
using AgencyPro.Core.Proposals.Services;
using AgencyPro.Core.Retainers.Models;

namespace AgencyPro.Core.Proposals.Models
{
    public class FixedPriceProposal : AuditableEntity, IFixedPriceProposal
    {
        [ForeignKey("Id")] public Project Project { get; set; }
        public ProposalAcceptance ProposalAcceptance { get; set; }

        public ICollection<ProposalWorkOrder> WorkOrders { get; set; }
        public ICollection<ProposalNotification> Notifications { get; set; }

        private ICollection<ProposalStatusTransition> _statusTransitions;

        public virtual ICollection<ProposalStatusTransition> StatusTransitions
        {
            get => _statusTransitions ?? (_statusTransitions = new Collection<ProposalStatusTransition>());
            set => _statusTransitions = value;
        }

        public Guid Id { get; set; }

        public decimal VelocityBasis { get; set; }

        public decimal WeeklyMaxHourBasis { get; set; }

        public string AgreementText { get; set; }

        public decimal? BudgetBasis { get; set; }

        public ProposalStatus Status { get; set; }

        public Guid CreatedById { get; set; }
        public Guid UpdatedById { get; set; }

        public string ConcurrencyStamp { get; set; }
        
        public ProposalType ProposalType { get; set; }

        public decimal WeeklyCapacity
        {
            get => WeeklyMaxHourBasis * VelocityBasis;
            set { }
        }

        public decimal DailyCapacity
        {
            get => WeeklyCapacity / 7;
            set { }
        }

        public int StoryPointBasis { get; set; }

        public int EstimationBasis { get; set; }

        public decimal OtherPercentBasis { get; set; }

        public int ExtraDayBasis { get; set; }

        public decimal CustomerRateBasis { get; set; }

        public int StoryHours
        {
            get => StoryPointBasis * EstimationBasis;
            set { }
        }

        public decimal TotalHours
        {
            get => StoryHours * (1 + OtherPercentBasis);
            set { }
        }

        public decimal TotalPriceQuoted
        {
            get => TotalHours * CustomerRateBasis;
            set { }
        }

        public decimal TotalDays
        {
            get => (TotalHours / DailyCapacity) + ExtraDayBasis;
            set { }
        }

        public decimal RetainerPercent { get; set; }

    }
}