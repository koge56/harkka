using harkka.models;
using harkka.Reporsitories;
using Microsoft.EntityFrameworkCore;

namespace BackEndHarjoitusTyo.Repositories
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

        //Get public messages
        public async Task<IEnumerable<Message>> GetMessagesAsync()
        {
            return await _context.Messages
                .Include(s => s.sender)
                .Where(x => x.recipent == null)
                .OrderByDescending(x => x.id)
                .Take(10)
                .ToListAsync();
        }

        public async Task<IEnumerable<Message>> GetMyReceivedMessagesAsync(User user)
        {
            return await _context.Messages
               .Include(s => s.sender)
               .Include(s => s.recipent)
               .Where(x => x.recipent == user)
               .OrderByDescending(x => x.id)
               .Take(10)
               .ToListAsync();
        }

        public async Task<IEnumerable<Message>> GetMySentMessagesAsync(User user)
        {
            return await _context.Messages
               .Include(s => s.sender)
               .Include(s => s.recipent)
               .Where(x => x.sender == user)
               .OrderByDescending(x => x.id)
               .Take(10)
               .ToListAsync();
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