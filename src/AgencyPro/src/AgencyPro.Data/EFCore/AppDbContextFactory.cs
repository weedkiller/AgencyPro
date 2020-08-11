// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AgencyPro.Data.EFCore
{
    public class AppDbContextFactory : IAppDbContextFactory
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            // Used only for EF .NET Core CLI tools (update database/migrations etc.)
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                .AddJsonFile("appsettings.json", false, true);

            var config = builder.Build();

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
                .EnableSensitiveDataLogging()
                .UseSqlServer(config.GetConnectionString("DefaultConnection"));

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}