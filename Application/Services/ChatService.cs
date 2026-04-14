using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Application.Services
{
    public class ChatService : IChatService
    {
        private readonly UserManager<User> _userManger;
        private readonly IChatRoomRepo _chatRoomRepo;
        private readonly IMessageRepo _messageRepo;
        private readonly IUnitOfWork _unitOfWork;

        public ChatService(UserManager<User> userManger,IChatRoomRepo chatRoomRepo, IMessageRepo messageRepo, IUnitOfWork unitOfWork)
        {
            _userManger = userManger;
            _chatRoomRepo = chatRoomRepo;
            _messageRepo = messageRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateMessageAsync(Guid userId, int chatId, string message)
        {
            await _messageRepo.CreateMessageAsync(new Message
            {
                ChatRoomId = chatId,
                UserId = userId,
                Content = message,
            });
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<ChatRoom>> GetAllRooms()
        {
            return await _chatRoomRepo.GetChatRoomsAsync();
        }

        public async Task<List<Message>> GetChatMessagesAsync(int chatId)
        {
            return await _messageRepo.GetMessagesByChatRoomIdAsync(chatId);
        }

        public async Task<ChatRoom?> GetChatRoomAsync(int chatId)
        {
            return await _chatRoomRepo.GetChatRoomByIdAsync(chatId);
        }

        public async Task JoinRoomAsync(Guid userId, int chatId)
        {
            var user = await _userManger.FindByIdAsync(userId.ToString());
            if(user is null) throw new Exception("User not found");
            var room = await _chatRoomRepo.GetChatRoomByIdForMutationAsync(chatId);
            if(room is null) throw new Exception("Chat room not found");
            room.Users.Add(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task RemoveUserFromRoomAsync(Guid userId, int chatId)
        {
            await _chatRoomRepo.RemoveUserFromRoom(userId, chatId);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateMessageAsync(int messageId, string message)
        {
            var existingMessage = await _messageRepo.GetMessageByIdForMutationAsync(messageId);
            if (existingMessage == null)
            {
                throw new Exception("Message not found");
            }

            existingMessage.Content = message;
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
