﻿// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core.Organizations.ViewModels
{
    public class OrganizationUpdateInput
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PrimaryColor { get; set; }
        public string SecondaryColor { get; set; }
        public string TertiaryColor { get; set; }
        public string ImageUrl { get; set; }
        public string Iso2 { get; set; }
        public string ProvinceState { get; set; }
    }
}