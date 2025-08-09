// <copyright file="PaginatedResult{T}.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.Patterns
{
    using System;
    using System.Collections.Generic;

    public class PaginatedResult<T>(List<T> items, int count, int page, int pageSize)
    {
        public List<T> Items { get; set; } = items;

        public int TotalItems { get; set; } = count;

        public int TotalPages { get; set; } = (int)Math.Ceiling(count / (double)pageSize);

        public int CurrentPage { get; set; } = page;

        public int PageSize { get; set; } = pageSize;
    }
}
