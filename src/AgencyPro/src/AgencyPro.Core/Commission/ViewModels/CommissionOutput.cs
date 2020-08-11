// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using AgencyPro.Core.TimeEntries.Enums;

namespace AgencyPro.Core.Commission.ViewModels
{
    public class CommissionOutput
    {

        public CommissionOutput(BonusOutput bonus, StreamOutput stream)
        {
            this.Bonus = bonus;
            this.Stream = stream;

            this.MarketerStream = stream.MarketerStream;
            this.MarketingAgencyStream = stream.MarketingAgencyStream;
            this.RecruiterStream = stream.RecruiterStream;
            this.RecruitingAgencyStream = stream.RecruitingAgencyStream;
            this.ProjectManagerStream = stream.ProjectManagerStream;
            this.AccountManagerStream = stream.AccountManagerStream;
            this.ContractorStream = stream.ContractorStream;
            this.ProviderAgencyStream = stream.ProviderAgencyStream;
            this.MarketerBonus = bonus.MarketerBonus;
            this.MarketingAgencyBonus = bonus.MarketingAgencyBonus;
            this.RecruiterBonus = bonus.RecruiterBonus;
            this.RecruitingAgencyBonus = bonus.RecruitingAgencyBonus;
        }

        public BonusOutput Bonus { get; set; }
        public StreamOutput Stream { get; set; }

        public Dictionary<TimeStatus, decimal> MarketerStream { get;  }
    
        public Dictionary<TimeStatus, decimal> MarketingAgencyStream { get;  }
        public Dictionary<TimeStatus, decimal> RecruiterStream { get;  }
        public Dictionary<TimeStatus, decimal> RecruitingAgencyStream { get;  }
        public Dictionary<TimeStatus, decimal> ProjectManagerStream { get;  }
        public Dictionary<TimeStatus, decimal> AccountManagerStream { get; }
        public Dictionary<TimeStatus, decimal> ContractorStream { get; }
        public Dictionary<TimeStatus, decimal> ProviderAgencyStream { get;  }

        public Dictionary<TimeStatus, decimal> MarketerBonus { get;  }
        public Dictionary<TimeStatus, decimal> MarketingAgencyBonus { get; }

        public Dictionary<TimeStatus, decimal> RecruiterBonus { get;  }
        public Dictionary<TimeStatus, decimal> RecruitingAgencyBonus { get;  }
    }
}