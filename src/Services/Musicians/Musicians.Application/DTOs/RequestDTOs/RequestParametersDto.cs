using Musicians.Domain.Constants;

namespace Musicians.Application.DTOs.RequestDTOs
{
    public class RequestParametersDto
    {
        public int PageNumber { get; set; } = RequestParametersConstants.DEFAULT_PAGE_NUMBER;

        public int PageSize { get; set; } = RequestParametersConstants.DEFAULT_PAGE_SIZE;
    }
}
