using Chat.BusinessLogic.DTOs.ResponseDTOs;
using Chat.BusinessLogic.Exceptions.BadRequestException;
using Chat.BusinessLogic.Helpers.Interfaces;
using Chat.BusinessLogic.Services.Interfaces;
using Chat.DataAccess.Repositories.Interfaces;
using MapsterMapper;

namespace Chat.BusinessLogic.Services.Implementations
{
    internal class ChatRoleService : IChatRoleService
    {
        private readonly IChatRoleRepository _chatRoleRepository;
        private readonly IMapper _mapper;

        public ChatRoleService(IChatRoleRepository chatRoleRepository,
            IMapper mapper)
        {
            _chatRoleRepository = chatRoleRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ChatRoleResponseDto>> GetAllChatRolesAsync(CancellationToken cancellationToken = default)
        {
            var chatRoles = await _chatRoleRepository.GetAllAsync(false, cancellationToken);

            var chatRolesDtos = _mapper.Map<IEnumerable<ChatRoleResponseDto>>(chatRoles);

            return chatRolesDtos;
        }
    }
}
