using Domain.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositires
{
    public class MessageRepo : IMessageRepo
    {
        private readonly AppDbContext _context;

        public MessageRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Message?> GetMessageByIdAsync(int messageId)
        {
            return await _context.Messages
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == messageId);
        }

        public async Task<Message?> GetMessageByIdForMutationAsync(int messageId)
        {
           return await _context.Messages.FindAsync([messageId]);
        }

        public async Task<List<Message>> GetMessagesByChatRoomIdAsync(int chatRoomId)
        {
            return await _context.Messages
                .AsNoTracking()
                .Where(m => m.ChatRoomId == chatRoomId)
                .OrderBy(m => m.CreatedAt)
                .ToListAsync();
        }

        public async Task CreateMessageAsync(Message message)
        {
           await _context.Messages.AddAsync(message);
        }

        public async Task DeleteMessageAsync(int messageId)
        {
            await _context.Messages
                .Where(m => m.Id == messageId)
                .ExecuteDeleteAsync();
        }
    }
}
