// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.Stripe.Model;
using AgencyPro.Core.StripeSources.Services;
using AgencyPro.Core.StripeSources.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Stripe;

namespace AgencyPro.Services.StripeSources
{
    public class SourceService : Service<StripeSource>, ISourceService
    {
        private readonly ILogger<SourceService> _logger;

        public async Task<SourceResult> SourceChargeable(Source source)
        {
            _logger.LogInformation(GetLogMessage("Source: {0}; Source.Customer: {1}; Source.Card: {2}"), source.Id, source.Customer, source.Card?.Last4);

            var retVal = new SourceResult()
            {
                SourceId = source.Id
            };

            var entity = await Repository.Queryable()
                .Where(x => x.Id == source.Id)
                .FirstOrDefaultAsync();

            if (entity == null)
            {
                entity = new StripeSource
                {
                    Id = source.Id,
                    ObjectState = ObjectState.Added
                };
            }
            else
            {
                entity.ObjectState = ObjectState.Modified;
                entity.Updated = DateTimeOffset.UtcNow;
            }

            entity.ClientSecret = source.ClientSecret;
            entity.Created = source.Created;
            entity.Status = source.Status;
            entity.Flow = source.Flow;
            entity.Type = source.Type;
            entity.Amount = source.Amount;
            
            var records = Repository.InsertOrUpdateGraph(entity, true);

            _logger.LogDebug(GetLogMessage("{0} updated records"), records);

            if (records > 0)
            {
                retVal.Succeeded = true;
            }

            return retVal;
        }

        public SourceService(
            ILogger<SourceService> logger,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _logger = logger;
        }

        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[{nameof(SourceService)}.{callerName}] - {message}";
        }
    }
}