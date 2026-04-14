using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Backend.Hubs
{
    [Authorize]
    public class ChatHub : Hub<IChatClient>
    {
        private readonly IChatService _chatService;

        public ChatHub(IChatService chatService)
        {
            _chatService = chatService;
        }

        override public async Task OnConnectedAsync()
        {
            var userName = Context.User?.Identity?.Name;
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var chatRoomId = Context.GetHttpContext()?.Request.Query["chatRoomId"].ToString();

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(userName)) 
            {
                await Clients.Caller.ReceiveMessage("System", "Unauthorized");
                Context.Abort();
                return;
            }
            if(string.IsNullOrEmpty(chatRoomId))
            {
                await Clients.Caller.ReceiveMessage("System", "Chat room not specified");
                Context.Abort();
                return;
            }

            try
            {
                await _chatService.JoinRoomAsync(Guid.Parse(userId), int.Parse(chatRoomId));
            }
            catch (Exception ex)
            {
                await Clients.Caller.ReceiveMessage("System", $"Error joining chat room: {ex.Message}");
                Context.Abort();
                return;
            }
            var chatRoom = await _chatService.GetChatRoomAsync(int.Parse(chatRoomId));
            if (chatRoom == null) 
            {
                await Clients.Caller.ReceiveMessage("System", "Chat room not found");
                Context.Abort();
                return;
            }
            Context.Items["UserId"] = userId;
            Context.Items["UserName"] = userName;
            Context.Items["ChatRoomId"] = chatRoomId;
            Context.Items["ChatRoom"] = chatRoom.Name;

            await Groups.AddToGroupAsync(Context.ConnectionId, chatRoomId);
            await Clients.Group(chatRoomId).ReceiveMessage("System", $"{userName} has joined the chat.");
        }

        public async Task SendMessage(string message)
        {
            string userName = Context.Items["UserName"] as string;
            string chatRoom = Context.Items["ChatRoom"] as string;
            string userId = Context.Items["UserId"] as string;
            string chatRoomId = Context.Items["ChatRoomId"] as string;

            await _chatService.CreateMessageAsync(Guid.Parse(userId), int.Parse(chatRoomId), message);
            await Clients.Groups(chatRoomId).ReceiveMessage(userName, message);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            string userName = Context.Items["UserName"] as string;
            string chatRoom = Context.Items["ChatRoom"] as string;
            string userId = Context.Items["UserId"] as string;
            string chatRoomId = Context.Items["ChatRoomId"] as string;

            await _chatService.RemoveUserFromRoomAsync(Guid.Parse(userId), int.Parse(chatRoomId));

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatRoomId);
            Context.Items.Remove("UserName");
            Context.Items.Remove("ChatRoom");
            Context.Items.Remove("UserId");
            Context.Items.Remove("ChatRoomId");

            await Clients.Group(chatRoomId).ReceiveMessage("System", $"{userName} has left the chat.");
        }
    }
}
