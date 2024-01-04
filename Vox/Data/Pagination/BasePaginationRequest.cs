namespace Vox.Data.Pagination
{
    public class BasePaginationRequest
    {
        public int Page { get; set; } = 1;
        public int PerPage { get; set; } = 10;
    }
}
