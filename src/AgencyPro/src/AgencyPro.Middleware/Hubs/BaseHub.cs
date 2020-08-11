// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Microsoft.AspNetCore.SignalR;

namespace AgencyPro.Middleware.Hubs
{
    public abstract class AccountHub : BaseHub
    {
    }

    public abstract class ProjectHub : BaseHub
    {
    }

    public abstract class ContractHub : BaseHub
    {
    }

    public abstract class BaseHub : Hub
    {
    }
}