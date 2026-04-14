using Domain.Entities;

namespace Application.Interfaces
{

    public interface IChatService
    {
        Task<List<ChatRoom>> GetAllRooms();

        Task<ChatRoom?> GetChatRoomAsync(int chatId);

        Task JoinRoomAsync(Guid userId, int chatId);

        Task RemoveUserFromRoomAsync(Guid userId ,  int chatId);

        Task<List<Message>> GetChatMessagesAsync(int chatId);

        Task CreateMessageAsync(Guid userId, int chatId, string message);

        Task UpdateMessageAsync(int messageId, string message);
    }

}
