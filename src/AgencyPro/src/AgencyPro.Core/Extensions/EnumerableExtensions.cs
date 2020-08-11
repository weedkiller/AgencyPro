// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;

namespace AgencyPro.Core.Extensions
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items) action(item);
        }

        public static string ToString<T>(this IEnumerable<T> l, string separator)
        {
            return "[" + string.Join(separator, l.Select(i => i.ToString()).ToArray()) + "]";
        }
    }
}