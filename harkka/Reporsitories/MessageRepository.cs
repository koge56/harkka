using harkka.models;
using Microsoft.EntityFrameworkCore;

namespace harkka.Reporsitories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly MessageServiceContext _context;
        public MessageRepository(MessageServiceContext context) 
        { 
            _context = context; 
        }

        public async Task<bool> DeleteMessageAsync(Message message)
        {
            if (message == null)
            {
                return false;
            }
            else
            {
                _context.Messages.Remove(message);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<Message?> GetMessageAsync(long id)
        {
            return await _context.Messages.FindAsync(id);
        }

        //get public messages
        public async Task<IEnumerable<Message>> GetMessagesAsync()
        {
            return await _context.Messages.Include(s=>s.sender).Where(x => x.recipent==null).OrderByDescending(x => x.id).Take(10).ToListAsync();
        }

        public async Task<IEnumerable<Message>> GetMyReceivedMessagesAsync(User usr)
        {
            return await _context.Messages.Include(s => s.sender).Where(x => x.recipent == usr).OrderByDescending(x => x.id).Take(10).ToListAsync();
        }

        public async Task<IEnumerable<Message>> GetMySentMessagesAsync(User usr)
        {
            return await _context.Messages.Include(s => s.recipent).Where(x => x.sender == usr).OrderByDescending(x=> x.id).Take(10).ToListAsync();
        }

        public async Task<Message> NewMessageAsync(Message message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
            return message;
        }

        public async Task<bool> UpdateMessageAsync(Message message)
        {
            _context.Entry(message).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
