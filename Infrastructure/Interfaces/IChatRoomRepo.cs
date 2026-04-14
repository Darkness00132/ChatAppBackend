using Domain.Entities;

namespace Infrastructure.Interfaces
{
    public interface IChatRoomRepo
    {
        Task<List<ChatRoom>> GetChatRoomsAsync();

        Task<ChatRoom?> GetChatRoomByIdAsync(int chatRoomId);

        Task<ChatRoom?> GetChatRoomByIdForMutationAsync(int chatRoomId);

        Task<ChatRoom?> GetChatRoomByNameAsync(string chatRoomName);

        Task RemoveUserFromRoom(Guid userId , int chatRoomId);

        Task CreateChatRoomAsync(ChatRoom chatRoom);

        Task DeleteChatRoomAsync(int chatRoomId);
    }
}
