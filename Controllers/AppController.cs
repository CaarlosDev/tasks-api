using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace tasks_api.Controllers
{
    [Controller]
    public class AppController : ControllerBase
    {
        public int GetUserId() =>
            int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
    }
}