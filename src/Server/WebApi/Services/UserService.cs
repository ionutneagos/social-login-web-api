namespace WebApi.Services
{
    using Infrastructure;
    using Microsoft.AspNetCore.Identity;
    using System.Threading.Tasks;
    using WebApi.Interfaces;
    using WebApi.Models;
    using WebApi.Models.Auth;
    using static Google.Apis.Auth.GoogleJsonWebSignature;

    public class UserService : IUserService
    {
        protected readonly UserManager<AppUser> _userManager;
        protected readonly IConfiguration _configuration;
        public UserService(UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<AppUser> AuthenticateGoogleUserAsync(GoogleUserRequest request)
        {
            Payload payload = await ValidateAsync(request.IdToken, new ValidationSettings
            {
                Audience = new[] { _configuration["Authentication:Google:ClientId"] }
            });

            return await GetOrCreateExternalLoginUser(GoogleUserRequest.PROVIDER, payload.Subject, payload.Email, payload.GivenName, payload.FamilyName);
        }


        private async Task<AppUser> GetOrCreateExternalLoginUser(string provider, string key, string email, string firstName, string lastName)
        {
            var user = await _userManager.FindByLoginAsync(provider, key);
          
            if (user != null)
                return user;

            user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                user = new AppUser
                {
                    Email = email,
                    UserName = email,
                    FirstName = firstName,
                    LastName = lastName,
                    Id = key,
                };
                await _userManager.CreateAsync(user);
                var info = new UserLoginInfo(provider, key, provider.ToUpperInvariant());
                var result = await _userManager.AddLoginAsync(user, info);
              
                if(!result.Succeeded)
                    throw new AppException(string.Join(",", result.Errors.Select(x => x.Description).ToList()));
            }

            return user;
        }
    }
}