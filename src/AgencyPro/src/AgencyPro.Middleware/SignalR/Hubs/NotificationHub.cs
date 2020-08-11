// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.People.Services;
using Microsoft.AspNetCore.SignalR;

namespace AgencyPro.Middleware.SignalR.Hubs
{
    // [HubName("notification")]
    public class NotificationHub : Hub
    {
        public static string ConnectionId;

        protected static readonly ConcurrentDictionary<string, List<string>> UserConnectionIds =
            new ConcurrentDictionary<string, List<string>>();

        private readonly IPersonService _userProfileService;

        public NotificationHub(IPersonService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        public void NewUpdate(string command, double state)
        {
            Clients.Others.SendAsync(command, state);
        }

        public override Task OnConnectedAsync()
        {
            Connect();
            return base.OnConnectedAsync();
        }


        /// <summary>
        /// Called when the connection reconnects to this hub instance.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task" />
        /// </returns>
        //public override Task OnReconnected()
        //{
        //    Connect(true);
        //    return base.OnReconnected();
        //}

        /// <summary>
        ///     Connects the specified is reconnect.
        /// </summary>
        /// <param name="isReconnect">if set to <c>true</c> [is reconnect].</param>
        /// <returns></returns>
        public Task Connect(bool isReconnect = false)
        {
            var connectionId = Context.ConnectionId;
            // check the authenticated user principal from environment
            //var environment = Context.Request.Environment;
            //var principal = environment["server.User"] as ClaimsPrincipal;
            //if (principal != null && principal.Identity != null && principal.Identity.IsAuthenticated)
            //{
            //    // create a new HubCallerContext instance with the principal generated from token
            //    // and replace the current context so that in hubs we can retrieve current user identity
            //    Context = new HubCallerContext(new HttpRequest(environment), connectionId);
            //}


            //Clients.Caller.handleEvent(evt);

            return null;
        }
    }
}