// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core.Orders.Services
{
    public interface IOrderProcessor
    {
        void Process(Guid order);
    }
}
