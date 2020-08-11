// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core.Extensions
{
    public static class NumberExtensions
    {
        public static bool HasFlag(this int variable, Enum value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var num = Convert.ToUInt64(value);
            return (Convert.ToUInt64(variable) & num) == num;
        }

        public static bool HasFlag(this long variable, Enum value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var num = Convert.ToUInt64(value);
            return (Convert.ToUInt64(variable) & num) == num;
        }
    }
}