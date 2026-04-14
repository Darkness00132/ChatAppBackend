using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public ICollection<ChatRoom> ChatRooms { get; set; }
    }
}
