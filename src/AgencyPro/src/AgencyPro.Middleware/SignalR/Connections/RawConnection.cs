// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace AgencyPro.Middleware.SignalR.Connections
{
    // https://github.com/aspnet/SignalR/issues/1528
    //public class RawConnection : PersistentConnection
    //{
    //    private static readonly ConcurrentDictionary<string, string> Users = new ConcurrentDictionary<string, string>();
    //    private static readonly ConcurrentDictionary<string, string> Clients = new ConcurrentDictionary<string, string>();


    //    protected async Task Connected(HttpRequest request, string connectionId)
    //    {
    //        var identity = request.HttpContext.User.Identity;
    //        var status = identity.IsAuthenticated ? "Authenticated" : "Unauthenticated";

    //        //Logger.LogInformation($"{status} connection {connectionId} has just connected.");

    //        var userName = identity.Name;
    //        if (!string.IsNullOrEmpty(userName))
    //        {
    //            Clients[connectionId] = userName;
    //            Users[userName] = connectionId;
    //        }

    //        var clientIp = GetClientIP(request);

    //        var user = GetUser(connectionId);

    //        await Groups.Add(connectionId, "AureliaAuthUsers");

    //        var data = new MessageToClient("welcome", $"Connection is {status}, hello {userName}!");

    //        var message = JsonConvert.SerializeObject(data);

    //        await Connection.Send(connectionId, message);

    //        data = new MessageToClient("connected", $"{DateTime.Now:MM-dd-HH-mm-ss}: {user} connected");
    //        message = JsonConvert.SerializeObject(data);
    //        await Connection.Broadcast(message);
    //    }

    //    protected Task OnReconnected(HttpRequest request, string connectionId)
    //    {
    //        var user = GetUser(connectionId);

    //        var data = new MessageToClient("reconnected", $"{DateTime.Now:MM-dd-HH-mm-ss}: {user} reconnected");
    //        var message = JsonConvert.SerializeObject(data);
    //        return Connection.Broadcast(message);
    //    }

    //    protected Task OnDisconnected(HttpRequest request, string connectionId, bool stopCalled)
    //    {
    //        string ignored;
    //        Users.TryRemove(connectionId, out ignored);
    //        var suffix = stopCalled ? "cleanly" : "uncleanly";
    //        var msg = DateTime.Now.ToString("MM-dd-HH-mm-ss") + ": " + GetUser(connectionId) + " disconnected " + suffix;
    //        var data = new MessageToClient("disconnected", msg);
    //        var message = JsonConvert.SerializeObject(data);
    //        return Connection.Broadcast(message);
    //    }

    //    private static string GetUser(string connectionId)
    //    {
    //        string user;
    //        return !Clients.TryGetValue(connectionId, out user) ? connectionId : user;
    //    }

    //    private string GetClient(string user)
    //    {
    //        string connectionId;
    //        return Users.TryGetValue(user, out connectionId) ? connectionId : null;
    //    }

    //    private static string GetClientIP(HttpRequest request)
    //    {
    //        var conn = request.HttpContext.Features.Get<IHttpConnectionFeature>();
    //        return conn?.RemoteIpAddress.ToString();
    //    }
    //}
}