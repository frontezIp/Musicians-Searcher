using Chat.BusinessLogic.RequestFeatures;

namespace Chat.BusinessLogic.Extensions
{
    public static class IEnumerableExtension
    {
        public static MetaData GetMetaData(this IEnumerable<object> enumerable,
            long count,
            int pageNumber, 
            int pageSize) 
        {
            return new MetaData
            {
                TotalCount = count,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = (long)Math.Ceiling(count / (double)pageSize)
            };
        }
    }
}
