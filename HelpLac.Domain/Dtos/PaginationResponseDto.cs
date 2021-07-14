namespace HelpLac.Domain.Dtos
{
    public class PaginationResponseDto
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalItens { get; set; }
        public string OrderBy { get; set; }
        public bool Desc { get; set; }

        public PaginationResponseDto(int pageNumber, int pageSize, int totalPages, int totalItens, string orderBy, bool desc)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPages = totalPages;
            TotalItens = totalItens;
            OrderBy = orderBy;
            Desc = desc;
        }
    }
}
