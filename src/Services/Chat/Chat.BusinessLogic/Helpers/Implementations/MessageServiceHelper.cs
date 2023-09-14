using Chat.BusinessLogic.Exceptions.BadRequestException;
using Chat.BusinessLogic.Exceptions.NotFoundExceptions;
using Chat.BusinessLogic.Helpers.Interfaces;
using Chat.DataAccess.Models;
using Chat.DataAccess.Repositories.Interfaces;

namespace Chat.BusinessLogic.Helpers.Implementations
{
    internal class MessageServiceHelper : IMessageServiceHelper
    {
        private readonly IMessageRepository _messageRepository;

        public MessageServiceHelper(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<Message> CheckIfMessageExistsByIdAndGetAsync(Guid messageId, bool trackChanges, CancellationToken cancellationToken = default)
        {
            var message = await _messageRepository.GetByIdAsync(messageId, trackChanges, cancellationToken);

            if (message == null)
                throw new MessageNotFoundException(messageId);

            return message;
        } 

        public async Task<IEnumerable<Message>> CheckIfMessagesByIdsExistsAndGetAsync(IEnumerable<Guid> ids, bool trackChanges, CancellationToken cancellationToken = default)
        {
            if (ids is null)
                throw new IdParameterBadRequestException(nameof(Message));

            var messages = await _messageRepository.GetByIdsAsync(ids, trackChanges, cancellationToken);

            if (messages.Count() != ids.Count())
                throw new CollectionByIdsBadRequestException(nameof(Message));

            return messages;
        }

        public async Task CheckIfMessagesByIdsExistsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        {
            await CheckIfMessagesByIdsExistsAsync(ids, cancellationToken);
        }
    }
}
