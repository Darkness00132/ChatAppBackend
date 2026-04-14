namespace Application.Interfaces
{
    public interface IRefreshToken
    {
        Task CreateRefreshToken();

        Task ReadRefreshToken(string refreshToken);

        Task DeleteRefreshToken(string refreshToken);
    }
}
