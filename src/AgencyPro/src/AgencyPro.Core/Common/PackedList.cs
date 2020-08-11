// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;

namespace AgencyPro.Core.Common
{
    public class PackedList<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int Total { get; set; }
    }
}