// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.Products.Services;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Stripe;
using Product = AgencyPro.Core.Products.Models.Product;

namespace AgencyPro.Services.Products
{
    public class ProductService : Service<Product>, IProductService
    {

        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[{nameof(ProductService)}.{callerName}] - {message}";
        }

        private readonly ILogger<ProductService> _logger;
        private readonly global::Stripe.ProductService _productService;

        public ProductService(
            ILogger<ProductService> logger,
            IServiceProvider serviceProvider, global::Stripe.ProductService productService) : base(serviceProvider)
        {
            _logger = logger;
            _productService = productService;
        }

        public Task<int> PushProduct(Product product, bool commit = true)
        {
            global::Stripe.Product stripeProduct = null;

            if (!string.IsNullOrWhiteSpace(product.StripeId))
                stripeProduct = _productService.Get(product.StripeId);

            if (stripeProduct != null)
            {
                var options = new ProductUpdateOptions()
                {
                    Url = product.Url,
                    StatementDescriptor = product.StatementDescriptor,
                    Active = product.IsActive,
                    Caption = product.Caption,
                    Description = product.Description,
                    Name = product.Name,
                    Metadata = new Dictionary<string, string>()
                    {
                        {"ref_id", product.Id.ToString()}
                    },
                };

                stripeProduct = _productService.Update(stripeProduct.Id, options);
            }
            else
            {
                // this is a new product
                var options = new ProductCreateOptions()
                {
                    Type = product.Type,
                    Id = product.UniqueId,

                    // same as update
                    Name = product.Name,
                    Url = product.Url,
                    Active = product.IsActive,
                    Caption = product.Caption,
                    Description = product.Description,
                    StatementDescriptor = product.StatementDescriptor,
                    Metadata = new Dictionary<string, string>()
                    {
                        {"ref_id", product.Id.ToString()}
                    }
                };
                stripeProduct = _productService.Create(options);
            }

            product.StripeId = stripeProduct.Id;
            product.StripeBlob = JsonConvert.SerializeObject(stripeProduct);
            product.ObjectState = ObjectState.Modified;

            return Task.FromResult(Repository.InsertOrUpdateGraph(product, commit));
        }

        public async Task<int> ExportProducts()
        {

            _logger.LogInformation(GetLogMessage("Exporting Products..."));

            var products = Repository.Queryable().ToList();

            var totals = 0;
            foreach (var product in products)
            {
                var result = await PushProduct(product);
                totals += result;
            }

            return totals;
        }
    }
}
