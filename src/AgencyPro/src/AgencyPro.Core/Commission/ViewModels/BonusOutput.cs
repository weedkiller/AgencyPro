// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using AgencyPro.Core.TimeEntries.Enums;

namespace AgencyPro.Core.Commission.ViewModels
{
    public class BonusOutput
    {
        public BonusOutput()
        {
            MarketerBonus = new Dictionary<TimeStatus, decimal>();
            RecruiterBonus = new Dictionary<TimeStatus, decimal>();
            RecruitingAgencyBonus = new Dictionary<TimeStatus, decimal>();
            MarketingAgencyBonus = new Dictionary<TimeStatus, decimal>();
        }

        public Dictionary<TimeStatus, decimal> Totals
        {
            get
            {
                var dictionary = new Dictionary<TimeStatus, decimal>();

                foreach (var x in MarketingAgencyBonus.Keys)
                {
                    if (dictionary.ContainsKey(x))
                    {
                        dictionary[x] += MarketingAgencyBonus[x];
                    }
                    else
                    {
                        dictionary[x] = MarketingAgencyBonus[x];
                    }
                }


                foreach (var x in RecruitingAgencyBonus.Keys)
                {
                    if (dictionary.ContainsKey(x))
                    {
                        dictionary[x] += RecruitingAgencyBonus[x];
                    }
                    else
                    {
                        dictionary[x] = RecruitingAgencyBonus[x];
                    }
                }


                foreach (var x in MarketerBonus.Keys)
                {
                    if (dictionary.ContainsKey(x))
                    {
                        dictionary[x] += MarketerBonus[x];
                    }
                    else
                    {
                        dictionary[x] = MarketerBonus[x];
                    }
                }


                foreach (var x in RecruiterBonus.Keys)
                {
                    if (dictionary.ContainsKey(x))
                    {
                        dictionary[x] += RecruiterBonus[x];
                    }
                    else
                    {
                        dictionary[x] = RecruiterBonus[x];
                    }
                }

                return dictionary;
            }
        }
        
        public Dictionary<TimeStatus, decimal> MarketerBonus { get; set; }
        public Dictionary<TimeStatus, decimal> MarketingAgencyBonus { get; set; }

        public Dictionary<TimeStatus, decimal> RecruiterBonus { get; set; }
        public Dictionary<TimeStatus, decimal> RecruitingAgencyBonus { get; set; }

    }
}