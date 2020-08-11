// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AgencyPro.Middleware.Swashbuckle
{
    public class GlobalOperationDocumentationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            //operation.Responses.Add("422", new Response { Description = "Unprocessable Entity" });
            // operation.Responses.Add("400", new Response { Description = "Bad Request" });
        }
    }
}