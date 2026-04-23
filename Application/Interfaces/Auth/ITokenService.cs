namespace Application.Interfaces.Auth
{
    public interface ITokenService
    {
        Task<TokenResult> GenerateJwtToken(ApplicationUser user);

        Task<RefreshToken> GenerateRefreshToken(ApplicationUser user);

        Task<RefreshToken?> GetRefreshTokenAsync(string token);
        Task RevokeRefreshTokenAsync(string token);
    }

    public class TokenResult
    {
        public string Token { get; set; } = string.Empty;

        public string RefreshToken { get; set; } = string.Empty;

        public DateTime ExpiresAt { get; set; }
    }
}