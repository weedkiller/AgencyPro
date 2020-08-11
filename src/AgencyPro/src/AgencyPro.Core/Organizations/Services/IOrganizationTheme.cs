// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace AgencyPro.Core.Organizations.Services
{
    public interface IOrganizationTheme
    {
        string PrimaryColor { get; set; }
        string SecondaryColor { get; set; }
        string TertiaryColor { get; set; }
        //string ColumnBgColor { get; set; }
        //string MenuBgHoverColor { get; set; }
        //string HoverItemColor { get; set; }
        //string TextColor { get; set; }
        //string ActiveItemColor { get; set; }
        //string ActivePresenceColor { get; set; }
        //string ActiveItemTextColor { get; set; }
        //string MentionBadgeColor { get; set; }
    }
}