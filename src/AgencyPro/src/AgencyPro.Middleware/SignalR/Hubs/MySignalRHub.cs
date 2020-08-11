// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Microsoft.AspNetCore.SignalR;

namespace AgencyPro.Middleware.SignalR.Hubs
{
    //[HubName("updater")]
    public class MySignalRHub : Hub
    {
        public void NewUpdate(string command, double state)
        {
            Clients.Others.SendAsync(command, state);
        }
    }
}