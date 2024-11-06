using harkka.models;

namespace harkka.Reporsitories
{
    public interface IMessageRepository
    {
        Task<IEnumerable<Message>> GetMessagesAsync();
        Task<IEnumerable<Message>> GetMySentMessagesAsync(User usr);
        Task<IEnumerable<Message>> GetMyReceivedMessagesAsync(User usr);
        Task<Message?> GetMessageAsync(long id);
        Task<Message> NewMessageAsync(Message message);
        Task <bool> UpdateMessageAsync(Message message);
        Task <bool> DeleteMessageAsync(Message message);
    }
}
