using Application.DTOs.Auth;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccessTokenService _accessTokenService;
        private readonly IRefreshTokenRepo _refreshTokenRepo;

        public AuthService(UserManager<User> userManager, IUnitOfWork unitOfWork, IAccessTokenService accessTokenService, IRefreshTokenRepo refreshTokenRepo)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _accessTokenService = accessTokenService;
            _refreshTokenRepo = refreshTokenRepo;
        }

        public async Task<RefreshResponse> Login(LoginDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if(user is null) throw ApiException.BadRequest("email already exist");
            var match = await _userManager.CheckPasswordAsync(user, dto.Password);
            if (!match) throw ApiException.BadRequest("password is wrong, try again");

            var refreshToken = await _refreshTokenRepo.CreateRefreshTokenAsync(user.Id);
            var accessToken = _accessTokenService.CreateAccessToken(user);

            await _unitOfWork.SaveChangesAsync();

            return new RefreshResponse { AccessToken = accessToken, RefreshToken = refreshToken };
        }

        public async Task<RefreshResponse> Refresh(string refreshToken)
        {
            var existingRefreshToken = await _refreshTokenRepo.GetRefreshTokenAsync(refreshToken);

            if (existingRefreshToken is null) throw ApiException.Unauthorized("Invalid refresh token");

            if (existingRefreshToken is null || existingRefreshToken.IsRevoked)
                throw ApiException.Unauthorized("Invalid refresh token");

            await _refreshTokenRepo.RevokeCurrent(refreshToken);

            var user = await _userManager.FindByIdAsync(existingRefreshToken.UserId.ToString());
            if (user is null) ApiException.NotFound("User not found");

            var newRefreshToken = await _refreshTokenRepo.CreateRefreshTokenAsync(user.Id);
            var accessToken = _accessTokenService.CreateAccessToken(user);

            await _unitOfWork.SaveChangesAsync();

            return new RefreshResponse { AccessToken = accessToken, RefreshToken = newRefreshToken };
        }

        public async Task Register(RegisterDTO dto)
        {
           var result = await _userManager.CreateAsync(new User { UserName = dto.UserName, Email = dto.Email }, dto.Password);
            if (!result.Succeeded) throw ApiException.BadRequest(string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        public async Task RevokeAll(Guid userId)
        {
            await _refreshTokenRepo.RevokeAll(userId);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task RevokeCurrent(string refreshToken)
        {
            await _refreshTokenRepo.RevokeCurrent(refreshToken);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
