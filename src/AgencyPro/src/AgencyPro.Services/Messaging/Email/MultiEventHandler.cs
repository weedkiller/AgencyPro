// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.EmailSending.Services;
using AgencyPro.Core.Messaging.Email;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Runtime.CompilerServices;
using AgencyPro.Core;
using AgencyPro.Core.Contracts.ViewModels;
using AgencyPro.Core.Leads.ViewModels;
using AgencyPro.Core.Notifications.Models;
using AgencyPro.Core.Projects.ViewModels;
using AgencyPro.Core.Templates.Services;
using AgencyPro.Core.UrlHelpers;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Messaging.Email
{
    public abstract class MultiEventHandler<TEvent> : MessagingEventHandler<TEvent>
    {
        private readonly IEmailSender _emailService;
        private readonly ITemplateParser _razorEngine;
        private readonly ILogger<MultiEventHandler<TEvent>> _logger;
        
        protected readonly DbContextOptions<AppDbContext> DbContextOptions;

        protected MultiEventHandler(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            if (serviceProvider == null) throw new ArgumentNullException(nameof(serviceProvider));
            
            _emailService = serviceProvider.GetRequiredService<IEmailSender>();
            _razorEngine = serviceProvider.GetRequiredService<ITemplateParser>();
            _logger = serviceProvider.GetRequiredService<ILogger<MultiEventHandler<TEvent>>>();

            DbContextOptions = serviceProvider
                .GetRequiredService<DbContextOptions<AppDbContext>>();
            
        }

        private EmailMessage GetMessage<T>([NotNull]string template, T model, string subject, string to, string name = "")
        {
            var html = _razorEngine.RenderAsync(template, model).Result;

            return new EmailMessage()
            {
                Body = html,
                AsHtml = true,
                Subject = subject,
                From = $"{Settings.Information.ContactName} {Settings.Information.ContactEmail}",
                To = to
            };
        }

        protected void Send<T>([NotNull]string template, T model, string subject, bool overrideUser = false, bool overrideSettings  =false)
        where T: IBasicEmail
        {
            _logger.LogInformation(GetLogMessage("Subject: {0}; To: {1}"), subject, model.RecipientEmail);
            
            if ((Settings.Email.SendMail || overrideSettings) && (model.SendMail || overrideUser))
            {
                var message = GetMessage(template, model, subject, model.RecipientEmail, model.RecipientName);

                if (message == null) return;
                
                _logger.LogDebug(GetLogMessage("Sending message asynchronously"));

                _emailService.SendAsync(message);
            }
            else
            {
                _logger.LogWarning(GetLogMessage("Email notifications are not enabled"));
                _logger.LogDebug(GetLogMessage("Email enabled for environment: {0}"), Settings.Email.SendMail);
                _logger.LogDebug(GetLogMessage("Email enabled for person: {0}"), model.SendMail);
            }

        }
        
        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[EmailEventHandler.{callerName}] - {message}";
        }


        protected void AddProjectNotification<T>(AppDbContext context, string message, T project) where T : ProjectOutput, IBasicEmail, new()
        {
            var projectNotification = new ProjectNotification()
            {
                RequiresAcknowledgement = false,
                OrganizationId = project.TargetOrganizationId,
                UserId = project.TargetPersonId,
                ProjectId = project.Id,
                Message = message,
                Url = project.GetProjectUrl(project)
            };

            context.ProjectNotifications.Add(projectNotification);

            var result = context.SaveChanges(true);

            _logger.LogDebug(GetLogMessage("{0} results updated"), result);
        }

        protected void AddContractNotification<T>(AppDbContext context, string message, T contract) where T : ContractOutput, IBasicEmail, new()
        {
            var contractNotification = new ContractNotification()
            {
                RequiresAcknowledgement = false,
                UserId = contract.TargetPersonId,
                OrganizationId = contract.TargetOrganizationId,

                ContractId = contract.Id,
                Message = message,
                Url = contract.GetContractUrl(contract)
            };

            context.ContractNotifications.Add(contractNotification);

            var result = context.SaveChanges(true);

            _logger.LogDebug(GetLogMessage("{0} results updated"), result);
        }

        protected void AddLeadNotification<T>(AppDbContext context, string message, T lead) where T : LeadOutput, IBasicEmail, new()
        {
            var leadNotification = new LeadNotification()
            {
                RequiresAcknowledgement = false,
                OrganizationId = lead.TargetOrganizationId,
                UserId = lead.TargetPersonId,
                LeadId = lead.Id,
                Message = message,
                Url = lead.GetLeadUrl(lead)
            };

            context.LeadNotifications.Add(leadNotification);

            var result = context.SaveChanges(true);

            _logger.LogDebug(GetLogMessage("{0} results updated"), result);
        }
    }
}