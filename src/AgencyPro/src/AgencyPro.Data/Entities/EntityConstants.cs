// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("AgencyPro.Services.UnitTests")]
namespace AgencyPro.Data.Entities
{
    public static class EntityConstants
    {
        // {F4EE19BB-645F-49A2-915B-E5EF98B15E97}
      
        public const string DefaultAvatarUrl = "https://www.dropbox.com/s/icxbbieztc2rrwd/default-avatar.png?raw=1";
        public static Guid SystemAdminUserId = new Guid("7c61a0ba-3066-46c1-9d7c-1cb9dbef7c62");
        public static Guid MarketerOnlyId = new Guid("990fe614-5e67-4064-b672-4a134a428c0c");
        public static Guid AgencyProId = new Guid("01a89b00-e5cd-4837-b2e4-c90ddd1025d7");
        public static Guid WCPROId = new Guid("4ffb1fd3-e13e-4aa4-988c-4ec17891f64b");
        public static Guid SystemAdminRoleId = new Guid("530057d7-0d5c-45dc-a0af-dc6c7e861fa9");
        public static Guid UserRoleId = new Guid("51a6df76-cc2e-4991-952d-5c45d45d3178");
        public static Guid Contract2 = new Guid("37669010-45f0-438c-ae3b-f434cb9f6619");
        public static Guid Project2 = new Guid("1cdbc5f9-daab-446d-9276-16e65b6d7037");
        
        public static Guid DefaultProjectId = new Guid("6040c27b-30a9-48da-b770-2bd6462af576");
        public static Guid Story1 = new Guid("53ba632e-287d-45d4-a793-3a104b3b41c6");
        public static Guid DefaultContractId = new Guid("a56e7890-7897-4276-92f6-431ee48a8669");

        public static decimal DefaultMarketerStream => 2.5m;
        public static decimal DefaultRecruiterStream => 2.5m;
        public static decimal DefaultAgencyStream => 16m;
        public static decimal DefaultRecruitingAgencyStream => 2m;
        public static decimal DefaultMarketingAgencyStream => 2m;
        public static decimal DefaultProjectManagerStream => 10m;
        public static decimal DefaultAccountManagerStream => 5m;

        public static string DefaultOrgImageUrl =>
            "https://www.agencypro.com/images/blank_agency_logo.png";

        public static string DefaultMarketerCode => "";
        public static string PortfolioUrl => null;
        public static decimal DefaultContractorStream => 50m;

        public static DateTimeOffset DefaultDateTime =>
            new DateTimeOffset(new DateTime(2019,8,27));

        public static decimal DefaultSystemStream => 5m;

        public static Guid FinancialAccountId = new Guid("a56e7890-7897-4276-92f6-431ee48a8669");

        public static Guid NetProgrammingSkillId = new Guid("a4d34479-26fc-4dd0-9a0e-402fc5527d37");
        public static Guid SqlProgrammingSkillId = new Guid("5d8f3505-0f2a-41c6-8fba-e832f65f9940");
        public static Guid TypeScriptProgrammingSkillId = new Guid("ba486179-ad9c-41ac-8cdc-a1ef6deaa279");

        public static Guid MotionGraphicsSkillId = new Guid("a20301e0-7f15-44cf-a4ae-85bd10a71753");
       
        public static Guid StoryBoardingSkillId = new Guid("dda0fdd3-b086-43d9-b29b-060599bb96ef");

        //generic skills
        public static Guid CreativeWritingSkillId = new Guid("d2266f18-80da-45dd-8b14-a27a4302f128");
        public static Guid TechnicalWritingSkillId = new Guid("11dbadc2-14d7-4d68-b9d1-ce3e15b51d20");

        public static Guid BusinessManagementSkillId = new Guid("15ead12f-3a08-4d4c-81ac-d229488cae57");
        public static Guid BusinessConsultingSkillId = new Guid("dc7d9df8-41e2-4bfa-93e5-73897cac1856");
        public static Guid DraftingSkillId = new Guid("1c09a340-e046-4662-973a-988a28a3635b");



        public static Guid FitnessModelingSkillId = new Guid("41e6d2df-8a0e-4821-bceb-25cec0185942");
        public static Guid SwimsuitModelingSkillId = new Guid("4829137c-0424-48c5-8d2e-2d3fac61212b");
        public static Guid CommercialModelingSkillId = new Guid("008ca394-b3a7-449b-831c-19a44de760dc");



        public static Guid GraphicDesignSkillId = new Guid("84ba4c3e-55c2-4b89-9b59-22a86ec7ca49");
        public static Guid WebDesignUXSkillId = new Guid("67d0e914-d3b1-4225-aa55-e94f973b83af");
        public static Guid MarketingSkillId = new Guid("3e2de629-c70e-4e4e-b8b6-010592d46be1");
        public static Guid AdvertisingSkillId = new Guid("4fcd2231-039f-494d-94e5-65e085fffda7");
        public static Guid ApplicationDevelopmentSkillId = new Guid("42ff744d-1325-4726-9bf1-4a34af6abae8");
        public static Guid MobileDevelopmentSkillId = new Guid("f1c89b14-3492-46fd-9702-e0578106984a");
        public static Guid SystemAdministrationSkillId = new Guid("26794060-2466-419b-9b4b-7888f7d49443");

        public static Guid PHPProgrammingSkillId = new Guid("557fda38-1dc8-4b2f-9fd5-c2ade9359c43");

        public static Guid JavaProgrammingSkillId = new Guid("46b85251-3f2f-46e1-a252-4ed0a20dfb42");
        public static Guid JavaScriptProgrammingSkillId = new Guid("58f38516-b404-4228-b645-85e498192ee6");
        public static Guid CPlusPlusProgrammingSkillId = new Guid("da231383-e23d-47f2-8345-708825c2a4e4");
        public static Guid PythonProgrammingSkillId = new Guid("fff10f3f-e09f-48eb-8240-1af12d278dda");
        public static Guid CProgrammingSkillId = new Guid("fcf6e58f-59e7-460d-99ec-84af4ba170a1");
        public static Guid RubyProgrammingSkillId = new Guid("5249cc32-978f-4987-b5c5-162b6e1f23e5");
    }
}