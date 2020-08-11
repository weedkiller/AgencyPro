// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace AgencyPro.Middleware.SignalR.Connections
{
    public class MessageToClient
    {
        public MessageToClient(string method, string content)
        {
            Method = method;
            Content = content;
        }

        public string Method { get; set; }
        public string Content { get; set; }
    }
}