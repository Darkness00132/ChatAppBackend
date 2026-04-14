

using Domain.Entities;

namespace Infrastructure.Interfaces
{
    public interface IRefreshTokenRepo
    {
        Task<string> CreateRefreshTokenAsync(Guid userId);

        Task<RefreshToken?> GetRefreshTokenAsync(string refreshToken);

        Task RevokeCurrent(string refreshToken);

        Task RevokeAll(Guid userId);
    }
}
