// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace AgencyPro.Middleware.SignalR.Connections
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Attribute" />
    /// <seealso cref="IAuthorizeHubConnection" />
    /// <seealso cref="IAuthorizeHubMethodInvocation" />
    //[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    //public class HubAuthorizeAttribute : Attribute, IAuthorizeHubConnection, IAuthorizeHubMethodInvocation
    //{
    //    private readonly TokenProviderOptions _tokenOptions;
    //    private IRequest _request;

    //    public HubAuthorizeAttribute(TokenProviderOptions tokenOptions)
    //    {
    //        _tokenOptions = tokenOptions;
    //    }


    //    public virtual bool AuthorizeHubMethodInvocation(IHubIncomingInvokerContext hubIncomingInvokerContext, bool appliesToMethod)
    //    {
    //        var connectionId = hubIncomingInvokerContext.Hub.Context.ConnectionId;
    //        // check the authenticated user principal from environment
    //        var principal = hubIncomingInvokerContext.Hub.Context.Request.User;
    //        if (principal?.Identity == null || !principal.Identity.IsAuthenticated) return false;
    //        // create a new HubCallerContext instance with the principal generated from token
    //        // and replace the current context so that in hubs we can retrieve current user identity
    //        hubIncomingInvokerContext.Hub.Context = new HubCallerContext(_request, connectionId);
    //        return true;
    //    }


    //    public bool AuthorizeHubConnection(HubDescriptor hubDescriptor, IRequest request)
    //    {
    //        _request = request;
    //        // authenticate by using bearer token in query string
    //        var token = request.QueryString["Bearer"];

    //        var validationParameters = new TokenValidationParameters
    //        {
    //            ValidAudience = _tokenOptions.Audience,
    //            ValidIssuer = _tokenOptions.Issuer,
    //            ValidateLifetime = true,
    //            ValidateAudience = true,
    //            ValidateIssuer = true,
    //            ValidateIssuerSigningKey = true
    //        };

    //        var handler = new JwtSecurityTokenHandler();
    //        var ticket = handler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
    //        if (ticket?.Identity == null || !ticket.Identity.IsAuthenticated) return false;
    //        // set the authenticated user principal into environment so that it can be used in the future
    //        //request.User = new ClaimsPrincipal(ticket.Identity);
    //        return true;
    //    }
    //}
}