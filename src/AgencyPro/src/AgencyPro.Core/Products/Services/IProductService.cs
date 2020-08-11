// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Threading.Tasks;
using AgencyPro.Core.Products.Models;

namespace AgencyPro.Core.Products.Services
{
    public interface IProductService
    {
        Task<int> PushProduct(Product product, bool commit = true);

        Task<int> ExportProducts();
    }
}
