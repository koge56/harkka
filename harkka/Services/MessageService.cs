using harkka.models;
using harkka.Reporsitories;

namespace harkka.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _repository;
        private readonly IUserRepository _userRepository;
        public MessageService(IMessageRepository repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        public async Task<bool> DeleteMessageAsync(long id)
        {
            Message? message = await _repository.GetMessageAsync(id);
            if (message != null)
            {
                await _repository.DeleteMessageAsync(message);
                return true;
            }
            return false;
        }

        public async Task<MessageDTO?> GetMessageAsync(long id)
        {
            return MessageToDTO(await _repository.GetMessageAsync(id));
        }

        public async Task<IEnumerable<MessageDTO>> GetMessagesAsync()
        {
            IEnumerable<Message> messages = await _repository.GetMessagesAsync();
            List<MessageDTO> result = new List<MessageDTO>();
            foreach (Message message in messages)
            {
                result.Add(MessageToDTO(message));
            }
            return result;
        }

        public async Task<MessageDTO?> NewMessageAsync(MessageDTO message)
        {
            return MessageToDTO(await _repository.NewMessageAsync(await DTOToMessageAsync(message)));
        }

        public async Task<bool> UpdateMessageAsync(MessageDTO message)
        {
            Message? dbmessage = await _repository.GetMessageAsync(message.id);
            if (dbmessage != null) 
            {
                dbmessage.title = message.title;
                dbmessage.body = message.body;
                return await _repository.UpdateMessageAsync(dbmessage);
            }
            return false;


        }


        private MessageDTO MessageToDTO(Message message)
        {
            MessageDTO dTO = new MessageDTO();
            dTO.id = message.id;
            dTO.title = message.title;
            dTO.body = message.body;
            dTO.sender = message.sender.username;
            if (message.recipent != null) 
            {
                dTO.recipent = message.recipent.username;
            }
            if (message.prevmessage != null)
            {
                dTO.PrevmessageId = message.prevmessage.id;
            }



            return dTO;
        }

        private async Task<Message> DTOToMessageAsync(MessageDTO dTO)
        {
            Message newMessage = new Message();
            newMessage.id = dTO.id;
            newMessage.title = dTO.title;
            newMessage.body = dTO.body;

            

            User? sender = await _userRepository.GetUserAsync(dTO.sender);
            if (sender != null)
            {
                newMessage.sender = sender;
            }
            if (dTO.recipent != null)
            {
                User? recipient = await _userRepository.GetUserAsync(dTO.recipent);
                if (recipient != null) 
                {
                    newMessage.recipent = recipient;
                }
            }

            if(dTO.PrevmessageId != null)
            {
                Message prevMessage = await _repository.GetMessageAsync((long)dTO.PrevmessageId);
                newMessage.prevmessage = prevMessage;
            }
            return newMessage;
        }
    }


    
}
