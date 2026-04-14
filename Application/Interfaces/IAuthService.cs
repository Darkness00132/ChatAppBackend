using Application.DTOs.Auth;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<RefreshResponse> Login(LoginDTO dto);

        Task Register(RegisterDTO dto);

        Task<RefreshResponse> Refresh(string refreshToken);

        Task RevokeCurrent(string refreshToken);

        Task RevokeAll(Guid userId);
    }
}
