// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.DisperseFunds.Events;
using AgencyPro.Core.DisperseFunds.ViewModels;
using AgencyPro.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationRoles.Services;

namespace AgencyPro.Services.DisperseFunds
{
    public partial class DisperseFundsService
    {
       
        private async Task<DisperseFundsResult> DisperseFundsToIndividual(IProviderAgencyOwner ao, string invoiceId)
        {
            _logger.LogInformation(GetLogMessage("Dispersing Funds for invoice: {0}"), invoiceId);
            var retVal = new DisperseFundsResult
            {
                InvoiceId = invoiceId,
                Amount = 0
            };

            var individualPayouts = await _individualPayoutIntents
                .Queryable()
                .Include(x => x.InvoiceTransfer)
                .Include(x => x.InvoiceItem)
                .ThenInclude(x => x.Contract)
                .Include(x => x.InvoiceItem)
                .ThenInclude(x => x.TimeEntries)
                .Include(x => x.Person)
                .ThenInclude(x => x.IndividualFinancialAccount)
                .ThenInclude(x => x.FinancialAccount)
                .Where(x => x.InvoiceItem.InvoiceId == invoiceId && x.Person.IndividualFinancialAccount != null &&
                            x.Person.IndividualFinancialAccount.FinancialAccount.PayoutsEnabled && x.InvoiceTransfer == null)
                .ToListAsync();

            _logger.LogDebug(GetLogMessage("Payouts ready for payment: {0}"), individualPayouts.Count);


            var totalTransfersMade = 0;
            var totalAmountTransferred = 0m;


            if (individualPayouts.Count > 0)
            {
                var transfersToMake = new List<TransferCreateOptions>();

                var people = individualPayouts.Select(x => x.Person).Distinct().ToList();

                _logger.LogDebug(GetLogMessage("Unique Individuals: {0}"), people.Count);

                foreach (var person in people)
                {
                    _logger.LogDebug(GetLogMessage("Processing Individual: {0}"), person.Id);

                    var personPayouts = individualPayouts
                        .Where(x => x.PersonId == person.Id)
                        .ToList();

                    _logger.LogDebug(GetLogMessage("Payouts for individual: {0}"), personPayouts.Count);

                    var individualPayoutIds = personPayouts
                        .Select(x => x.Id)
                        .ToArray();

                    var payoutIdString = string.Join(",", individualPayoutIds);


                    var decimalAmount = personPayouts.Sum(x => x.Amount);
                    var longAmount = Convert.ToInt64(decimalAmount * 100);


                    var sb = new StringBuilder();

                    foreach (var payout in personPayouts)
                    {
                        sb.AppendLine($@"{payout.Type.GetDescription()}: {decimalAmount:C}");
                    }

                    transfersToMake.Add(new TransferCreateOptions()
                    {
                        Amount = longAmount,
                        Currency = "usd",
                        Description = sb.ToString(),
                        Destination = person.IndividualFinancialAccount.FinancialAccountId,
                        TransferGroup = invoiceId,
                        Metadata = new Dictionary<string, string>()
                    {
                        { "invoice-id", invoiceId },
                        { "individual-payout-ids", payoutIdString },
                        { "person-id", person.Id.ToString() }
                    }
                    });
                }

                _logger.LogDebug(GetLogMessage("{0} Transfers to make"), transfersToMake.Count);

                foreach (var tran in transfersToMake)
                {
                    _logger.LogDebug(GetLogMessage("Transfer Amount: {0}"), tran.Amount);

                    var transfer = _transferService.Create(tran);

                    var transferResult = await TransferCreated(transfer);
                    _logger.LogDebug(GetLogMessage("Transfer Result: {@0}"), transferResult);
                    if (transferResult.Succeeded)
                    {
                        _logger.LogDebug(GetLogMessage("Transfer Creation Succeeded: {@result}"), transferResult);
                        totalAmountTransferred += transferResult.Amount;
                        totalTransfersMade += 1;
                    }

                    else
                    {
                        _logger.LogDebug(GetLogMessage("Transfer Creation Failed"));
                    }
                }

                if (totalTransfersMade > 0)
                {
                    retVal.Succeeded = true;
                    retVal.Amount = totalAmountTransferred;
                    retVal.TotalTransfersMade = totalTransfersMade;

                    await Task.Run(() =>
                    {
                        RaiseEvent(new FundsDispersedEvent()
                        {
                            InvoiceId = invoiceId,
                            TotalTransfersMade = totalTransfersMade
                        });
                    });
                }
            }



            return await Task.FromResult(retVal);
        }
        

    }
}