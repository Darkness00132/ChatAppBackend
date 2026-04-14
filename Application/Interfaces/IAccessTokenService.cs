using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAccessTokenService
    {
        string CreateAccessToken(User user);
    }
}
