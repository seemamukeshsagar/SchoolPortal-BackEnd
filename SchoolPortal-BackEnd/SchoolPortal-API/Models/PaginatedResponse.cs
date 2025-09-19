using System.Collections.Generic;

namespace SchoolPortal_API.Models
{
    /// <summary>
    /// Represents a paginated response with metadata
    /// </summary>
    /// <typeparam name="T">Type of items in the response</typeparam>
    public class PaginatedResponse<T>
    {
        /// <summary>
        /// The items for the current page
        /// </summary>
        public IEnumerable<T> Items { get; set; }

        /// <summary>
        /// Current page number (1-based)
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Number of items per page
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Total number of items across all pages
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Total number of pages
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Indicates if there is a previous page
        /// </summary>
        public bool HasPreviousPage => PageNumber > 1;

        /// <summary>
        /// Indicates if there is a next page
        /// </summary>
        public bool HasNextPage => PageNumber < TotalPages;
    }
}
