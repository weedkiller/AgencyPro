// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using AgencyPro.Core.TimeEntries.Enums;

namespace AgencyPro.Core.Commission.ViewModels
{
    public class StreamOutput
    {
        public StreamOutput()
        {
            MarketerStream = new Dictionary<TimeStatus, decimal>();
            RecruiterStream = new Dictionary<TimeStatus, decimal>();
            AccountManagerStream = new Dictionary<TimeStatus, decimal>();
            ProjectManagerStream = new Dictionary<TimeStatus, decimal>();
            ContractorStream = new Dictionary<TimeStatus, decimal>();
            RecruitingAgencyStream = new Dictionary<TimeStatus, decimal>();
            MarketingAgencyStream = new Dictionary<TimeStatus, decimal>();
            ProviderAgencyStream = new Dictionary<TimeStatus, decimal>();
        }
        
        public Dictionary<TimeStatus, decimal> Totals
        {
            get
            {
                var dictionary = new Dictionary<TimeStatus, decimal>();

                foreach (var x in MarketingAgencyStream.Keys)
                {
                    if (dictionary.ContainsKey(x))
                    {
                        dictionary[x] += MarketingAgencyStream[x];
                    }
                    else
                    {
                        dictionary[x] = MarketingAgencyStream[x];
                    }
                }


                foreach (var x in RecruitingAgencyStream.Keys)
                {
                    if (dictionary.ContainsKey(x))
                    {
                        dictionary[x] += RecruitingAgencyStream[x];
                    }
                    else
                    {
                        dictionary[x] = RecruitingAgencyStream[x];
                    }
                }


                foreach (var x in ProviderAgencyStream.Keys)
                {
                    if (dictionary.ContainsKey(x))
                    {
                        dictionary[x] += ProviderAgencyStream[x];
                    }
                    else
                    {
                        dictionary[x] = ProviderAgencyStream[x];
                    }
                }


                foreach (var x in MarketerStream.Keys)
                {
                    if (dictionary.ContainsKey(x))
                    {
                        dictionary[x] += MarketerStream[x];
                    }
                    else
                    {
                        dictionary[x] = MarketerStream[x];
                    }
                }

                foreach (var x in RecruiterStream.Keys)
                {
                    if (dictionary.ContainsKey(x))
                    {
                        dictionary[x] += RecruiterStream[x];
                    }
                    else
                    {
                        dictionary[x] = RecruiterStream[x];
                    }
                }

                foreach (var x in ProjectManagerStream.Keys)
                {
                    if (dictionary.ContainsKey(x))
                    {
                        dictionary[x] += ProjectManagerStream[x];
                    }
                    else
                    {
                        dictionary[x] = ProjectManagerStream[x];
                    }
                }

                foreach (var x in AccountManagerStream.Keys)
                {
                    if (dictionary.ContainsKey(x))
                    {
                        dictionary[x] += AccountManagerStream[x];
                    }
                    else
                    {
                        dictionary[x] = AccountManagerStream[x];
                    }
                }

                foreach (var x in ContractorStream.Keys)
                {
                    if (dictionary.ContainsKey(x))
                    {
                        dictionary[x] += ContractorStream[x];
                    }
                    else
                    {
                        dictionary[x] = ContractorStream[x];
                    }
                }

                return dictionary;
            }
        }
        
        public Dictionary<TimeStatus, decimal> MarketerStream { get; set; }
        public Dictionary<TimeStatus, decimal> MarketingAgencyStream { get; set; }
        public Dictionary<TimeStatus, decimal> RecruiterStream { get; set; }
        public Dictionary<TimeStatus, decimal> RecruitingAgencyStream { get; set; }
        public Dictionary<TimeStatus, decimal> ProjectManagerStream { get; set; }
        public Dictionary<TimeStatus, decimal> AccountManagerStream { get; set; }
        public Dictionary<TimeStatus, decimal> ContractorStream { get; set; }
        public Dictionary<TimeStatus, decimal> ProviderAgencyStream { get; set; }

    }
}
