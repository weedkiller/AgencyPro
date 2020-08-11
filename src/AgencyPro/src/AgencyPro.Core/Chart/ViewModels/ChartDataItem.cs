// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.TimeEntries.Enums;
using Newtonsoft.Json;
using System;

namespace AgencyPro.Core.Chart.ViewModels
{
    public class ChartDataItem
    {
        public ChartDataItem() { }
        public ChartDataItem(ChartDataItem item)
        {
            Dummy = item.Dummy;
            Date = item.Date;
            Name = item.Name;
            Hours = item.Hours;
            TimeStatus = item.TimeStatus;
            AccountManagerId = item.AccountManagerId;
            ContractId = item.ContractId;
            ContractorId = item.ContractorId;
            ProjectId = item.ProjectId;
            ProjectManagerId = item.ProjectManagerId;
            RecruiterId = item.RecruiterId;
            MarketerId = item.MarketerId;
            TotalAgStream = item.TotalAgStream;
            TotalRagStream = item.TotalRagStream;
            TotalMagStream = item.TotalMagStream;
            TotalAmStream = item.TotalAmStream;
            TotalCoStream = item.TotalCoStream;
            TotalMaStream = item.TotalMaStream;
            TotalPmStream = item.TotalPmStream;
            TotalReStream = item.TotalReStream;
            TotalCuAmount = item.TotalCuAmount;
            TotalSysStream = item.TotalSysStream;
        }
        public ChartDataItem(ChartDataItem item, string name) : this(item)
        {
            Name = name;
        }
        [JsonIgnore] public bool Dummy { get; set; }
        [JsonIgnore] public DateTime Date { get; set; }
        [JsonIgnore] public Guid RecruiterId { get; set; }
        [JsonIgnore] public Guid MarketerId { get; set; }
        [JsonIgnore] public Guid ProjectId { get; set; }
        [JsonIgnore] public Guid ProjectManagerId { get; set; }
        [JsonIgnore] public Guid AccountManagerId { get; set; }
        [JsonIgnore] public Guid ContractId { get; set; }
        [JsonIgnore] public Guid ContractorId { get; set; }
        public string Name { get; set; }
       [JsonIgnore] public TimeStatus TimeStatus { get; set; }
        public decimal Hours { get; set; }
        public virtual decimal TotalReStream { get; set; }
        public virtual decimal TotalMaStream { get; set; }
        public virtual decimal TotalAgStream { get; set; }
        public virtual decimal TotalMagStream { get; set; }
        public virtual decimal TotalRagStream { get; set; }
        public virtual decimal TotalCoStream { get; set; }
        public virtual decimal TotalPmStream { get; set; }
        public virtual decimal TotalAmStream { get; set; }

        public virtual decimal TotalCuAmount { get; set; }
        public virtual decimal TotalSysStream { get; set; }
    }
}