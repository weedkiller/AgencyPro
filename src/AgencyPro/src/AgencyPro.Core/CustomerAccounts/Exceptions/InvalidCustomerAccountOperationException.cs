// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core.CustomerAccounts.Exceptions
{
    public class InvalidCustomerAccountOperationException : InvalidOperationException
    {
        public InvalidCustomerAccountOperationException(string message) : base(message)
        {

        }
    }
}