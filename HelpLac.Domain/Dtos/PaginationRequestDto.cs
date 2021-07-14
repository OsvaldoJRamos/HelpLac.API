namespace HelpLac.Domain.Dtos
{
    public class PaginationRequestDto
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string OrderBy { get; set; }
        public bool Desc { get; set; }
    }
}
