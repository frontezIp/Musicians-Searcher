namespace Musicians.Domain.RequestFeatures
{
    public class MetaData
    {
        public int CurrentPage { get; set; }
        public long TotalPages { get; set; }
        public int PageSize { get; set; }
        public long TotalCount { get; set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
    }
}
