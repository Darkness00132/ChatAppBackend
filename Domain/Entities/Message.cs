
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities
{
    [Index(nameof(UserId))]
    public class Message
    {
        public int Id { get; set; }

        public required Guid UserId { get; set; }
        public User User { get; set; }

        public required string Content { get; set; }

        public required int ChatRoomId { get; set; }
        public ChatRoom ChatRoom { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    }
}
