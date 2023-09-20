namespace HomeBanking.API.Infrastructure.Auth
{
    public interface IAuthorizationParameters
    {
        string ValidAudience { get; set; }
        string ValidIssuer { get; set; }
        string Secret { get; set; }
        string TokenValidityInMinutes { get; set; }
        string RefreshTokenValidityInDays { get; set; }
    }
}