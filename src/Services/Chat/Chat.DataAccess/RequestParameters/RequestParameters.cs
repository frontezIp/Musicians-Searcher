using Chat.DataAccess.Constants;

namespace Chat.DataAccess.RequestParameters
{
    public abstract class RequestParameters
    {
        private int MaxPageSize { get; init; } = RequestParametersConstants.DEFAULT_MAX_PAGE_SIZE;

        public int PageNumber { get; set; } = RequestParametersConstants.DEFAULT_PAGE_NUMBER;

        private int _pageSize = RequestParametersConstants.DEFAULT_PAGE_SIZE;

        public int Skip
        {
            get => (PageNumber - 1) * PageSize;
        }

        public int Take
        {
            get => PageSize;
        }

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}
