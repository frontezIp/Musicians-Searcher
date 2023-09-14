using Chat.DataAccess.Constants;

namespace Chat.BusinessLogic.DTOs.RequestDTOs
{
    public class RequestParamatersDto
    {
        public int PageNumber { get; set; } = RequestParametersConstants.DEFAULT_PAGE_NUMBER;

        public int PageSize { get; set; } = RequestParametersConstants.DEFAULT_PAGE_SIZE;
    }
}
