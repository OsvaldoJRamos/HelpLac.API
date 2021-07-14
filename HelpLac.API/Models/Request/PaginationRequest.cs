namespace HelpLac.API.Models.Request
{
    public class PaginationRequest
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string OrderBy { get; set; }
        public bool Desc { get; set; }
    }
}
