// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace AgencyPro.Core.Config
{
    public class DbSettings
    {
        public DbSettings()
        {
            InMemoryProvider = false;
        }

        public string ConnectionString { get; set; }
        public bool InMemoryProvider { get; set; }
    }
}