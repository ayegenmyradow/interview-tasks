using System;
using System.Collections.Generic;
using MediatR;
using EmployeeManagement.Core.CQRS.DTOs;

namespace EmployeeManagement.Core.CQRS.Queries
{
    public class GetEmployeesQuery : IRequest<GetEmployeesResponse>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SearchTerm { get; set; }  // For filtering by name or position
        public string? SortBy { get; set; }      // Column to sort by
        public bool SortDescending { get; set; } = false;

        public GetEmployeesQuery()
        {
        }

        public GetEmployeesQuery(int pageNumber, int pageSize, string? searchTerm, string? sortBy, bool sortDescending)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            SearchTerm = searchTerm;
            SortBy = sortBy;
            SortDescending = sortDescending;
        }
    }

    public class GetEmployeesResponse
    {
        public IEnumerable<EmployeeDto> Employees { get; set; } = new List<EmployeeDto>();
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
    }
} 