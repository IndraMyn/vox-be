namespace Vox.Data.Pagination
{
    public class BasePaginationResponse<T>
    {
        public List<T> Data { get; set; }
        public Meta Meta { get; set; }

    }

    public class Meta
    {
        public Pagination Pagination { get; set; }
    }

    public class Pagination
    {
        public int Total { get; set; }
        public int Count { get; set; }
        public int PerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
        public PaginationLinks Links { get; set; }

    }

    public class PaginationLinks
    {
        public string Next { get; set; }
    }
}
