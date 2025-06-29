using ARC.Application.Common.Queries;

namespace ARC.Application.Common.Models
{
    /// <summary>
    /// Base class for paginated queries that provides common pagination functionality.
    /// </summary>
    /// <typeparam name="TData">The type of data in the paginated result.</typeparam>
    public enum SortDirection
    {
        Ascending = 0,
        Descending = 1
    }

    public abstract record PaginatedQueryBase<TData> : IPaginatedQuery<TData>
    {
        // maximum page size you will let clients request
        protected virtual short MaxPageSize { get; set; } = 50;

        private short _pageSize = 10;
        private int _pageNumber = 1;

        public int PageNumber
        {
            get => _pageNumber;
            set => _pageNumber = value < 1 ? 1 : value;
        }

        public short PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value < 1 ? _pageSize : value;
        }

        public SortDirection SortDirection { get; set; } = SortDirection.Ascending;

        public string? SearchQuery { get; set; }
    }

    /// <summary>
    /// Base class for paginated queries that include additional metadata.
    /// </summary>
    /// <typeparam name="TData">The type of data in the paginated result.</typeparam>
    /// <typeparam name="TMeta">The type of metadata to include with the paginated result.</typeparam>
    public abstract record PaginatedQueryBase<TData, TMeta>
    : PaginatedQueryBase<TData>,
      IPaginatedQuery<TData, TMeta>;

}



