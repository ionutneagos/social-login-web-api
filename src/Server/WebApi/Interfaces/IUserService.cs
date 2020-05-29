namespace WebApi.Interfaces
{
    using Infrastructure;
    using System.Threading.Tasks;
    using WebApi.Models.Auth;

    public interface IUserService
    {
        Task<AppUser> AuthenticateGoogleUserAsync(GoogleUserRequest request);
    }
}
