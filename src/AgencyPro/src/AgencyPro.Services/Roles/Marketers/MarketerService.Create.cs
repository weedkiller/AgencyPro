// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Core.Roles.Events;
using AgencyPro.Core.Roles.Models;
using AgencyPro.Core.Roles.ViewModels.Marketers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Omu.ValueInjecter;

namespace AgencyPro.Services.Roles.Marketers
{
    public partial class MarketerService
    {
        public Task<T> Create<T>(MarketerInput input)
            where T : MarketerOutput
        {
            return CreateInternal<T>(input);
        }

        private async Task<T> CreateInternal<T>(MarketerInput input)
            where T : MarketerOutput
        {
            _logger.LogTrace(GetLogMessage($@"Person Id: {input.PersonId}"));

            var marketer = await Repository.Queryable().Where(x=>x.Id==input.PersonId)
                .FirstOrDefaultAsync();

            if (marketer == null)
            {
                marketer = new Marketer {Id = input.PersonId};

                marketer.InjectFrom(input);

                await Repository.UpdateAsync(marketer, true);

                var output = await GetById<T>(input.PersonId);

                await Task.Run(() =>
                {
                    RaiseEvent(new MarketerCreatedEvent<T>
                    {
                        Marketer = output
                    });
                });

                return output;
            }

            throw new ApplicationException("Person is already a marketer");
        }
    }
}