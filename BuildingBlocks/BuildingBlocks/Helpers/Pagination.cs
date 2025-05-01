namespace BuildingBlocks.Helpers;

public record Pagination(int TotalItemCount, int PageCount, bool IsFirstPage, bool IsLastPage, bool HasNextPage, bool HasPreviousPage);
