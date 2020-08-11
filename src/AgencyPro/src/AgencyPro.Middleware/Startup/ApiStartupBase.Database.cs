// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Data.DataContext;
using AgencyPro.Core.Data.Repositories;
using AgencyPro.Core.Data.UnitOfWork;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace AgencyPro.Middleware.Startup
{
    public abstract partial class ApiStartupBase
    {
        private readonly string _connectionString;

        private void ConfigureDatabaseServices(IServiceCollection services)
        {

            Log.Information(GetLogMessage("Configuring Database Services"));

            var dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                    x => x.MigrationsAssembly("AgencyPro.Data"))
                .Options;

            services.AddSingleton(dbContextOptions);

            // This Factory is used to create the DbContext from the custom DbContextOptions:
            services.AddSingleton<IAppDbContextFactory, AppDbContextFactory>();

            // Finally Add the Applications DbContext:
            services.AddDbContext<AppDbContext>();

            services.AddScoped(typeof(IDataContextAsync), typeof(AppDbContext));
            services.AddScoped(typeof(IUnitOfWorkAsync), typeof(UnitOfWork));
            services.AddScoped(typeof(IRepositoryAsync<>), typeof(Repository<>));
        }
    }
}