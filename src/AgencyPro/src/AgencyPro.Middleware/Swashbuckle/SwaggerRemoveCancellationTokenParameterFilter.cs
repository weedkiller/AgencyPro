// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Linq;
using System.Threading;
using Microsoft.Win32.SafeHandles;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AgencyPro.Middleware.Swashbuckle
{
    public class SwaggerRemoveCancellationTokenParameterFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            context.ApiDescription.ParameterDescriptions
                .Where(pd =>
                    pd.ModelMetadata.ContainerType == typeof(CancellationToken) ||
                    pd.ModelMetadata.ContainerType == typeof(WaitHandle) ||
                    pd.ModelMetadata.ContainerType == typeof(SafeWaitHandle))
                .ToList()
                .ForEach(
                    pd =>
                    {
                        if (operation.Parameters != null)
                        {
                            var cancellationTokenParameter = operation.Parameters.Single(p => p.Name == pd.Name);
                            operation.Parameters.Remove(cancellationTokenParameter);
                        }
                    });
        }
    }
}