// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;

namespace AgencyPro.Core.Common
{
    public class PaginatedList<T> : List<T>
    {
        public PaginatedList(IEnumerable<T> source, int pageIndex, int pageSize, int totalCount)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            AddRange(source);
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
            TotalPageCount = (int) Math.Ceiling(totalCount / (double) pageSize);
        }

        public int PageIndex { get; }
        public int PageSize { get; }
        public int TotalCount { get; }
        public int TotalPageCount { get; }

        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPageCount;
    }
}