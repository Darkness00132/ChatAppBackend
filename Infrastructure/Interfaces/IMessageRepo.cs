using Domain.Entities;

namespace Infrastructure.Interfaces
{
    public interface IMessageRepo
    {
        Task<List<Message>> GetMessagesByChatRoomIdAsync(int chatRoomId);
    
            Task<Message?> GetMessageByIdAsync(int messageId);

            Task<Message?> GetMessageByIdForMutationAsync(int messageId);
    
            Task CreateMessageAsync(Message message);
    
            Task DeleteMessageAsync(int messageId);
    }
}
