using Domain.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositires
{
    public class ChatRoomRepo : IChatRoomRepo
    {
        private readonly AppDbContext _context;

        public ChatRoomRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ChatRoom?> GetChatRoomByIdAsync(int chatRoomId)
        {
            return await _context.ChatRooms
                .AsNoTracking()
                .FirstOrDefaultAsync(c=>c.Id == chatRoomId);
        }

        public async Task<ChatRoom?> GetChatRoomByIdForMutationAsync(int chatRoomId)
        {
            return await _context.ChatRooms.FindAsync(chatRoomId);
        }

        public async Task<ChatRoom?> GetChatRoomByNameAsync(string chatRoomName)
        {
            return await _context.ChatRooms
                .FirstOrDefaultAsync(c => c.Name == chatRoomName);
        }

        public async Task<List<ChatRoom>> GetChatRoomsAsync()
        {
            return await _context.ChatRooms.ToListAsync();
        }

        public async Task CreateChatRoomAsync(ChatRoom chatRoom)
        {
            await _context.ChatRooms.AddAsync(chatRoom);
        }

        public async Task DeleteChatRoomAsync(int chatRoomId)
        {
            await _context.ChatRooms.Where(c => c.Id == chatRoomId).ExecuteDeleteAsync();
        }

        public async Task RemoveUserFromRoom(Guid userId, int chatRoomId)
        {
            var chatRoom = await _context.ChatRooms
                .Include(c => c.Users)
                .FirstOrDefaultAsync(c => c.Id == chatRoomId);

            if (chatRoom is null) return;

            var user = chatRoom.Users.FirstOrDefault(u => u.Id == userId);
            if (user is null) return;

            chatRoom.Users.Remove(user);
        }
    }
}
