using System.Collections.Generic;
using MediatR;
using EmployeeManagement.Core.CQRS.DTOs;

namespace EmployeeManagement.Core.CQRS.Queries
{
    public class GetPositionsQuery : IRequest<GetPositionsResponse>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SearchTerm { get; set; }
        public string? SortBy { get; set; }
        public bool SortDescending { get; set; }

        public GetPositionsQuery()
        {
        }

        public GetPositionsQuery(int pageNumber, int pageSize, string? searchTerm, string? sortBy, bool sortDescending)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            SearchTerm = searchTerm;
            SortBy = sortBy;
            SortDescending = sortDescending;
        }
    }

    public class GetPositionsResponse
    {
        public IEnumerable<PositionDto> Positions { get; set; } = new List<PositionDto>();
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
    }
} 