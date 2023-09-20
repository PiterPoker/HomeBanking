namespace HomeBanking.API.Infrastructure.Services
{
    public interface IIdentityService
    {
        string GetUserIdentity();

        string GetUserName();
    }
}
