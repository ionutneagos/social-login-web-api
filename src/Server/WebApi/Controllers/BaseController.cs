namespace WebApi.Controllers
{
    using Infrastructure;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading.Tasks;

    [Route("api/[controller]/[action]")]
    public abstract class BaseController : Controller
    {
        protected readonly UserManager<AppUser> UserManager;

        protected BaseController(UserManager<AppUser> userManager)
        {
            UserManager = userManager;
        }

        protected async Task<AppUser> GetCurrentUser() => await UserManager.FindByIdAsync(User.Identity.Name);

        protected string GetUserId() => User.Identity.Name;

        protected IActionResult ReturnBadRequest(ILogger _logger, Exception e)
        {
            _logger.LogError("Get Property Failed " + e);
            return BadRequest(e.Message);
        }
    }
}
