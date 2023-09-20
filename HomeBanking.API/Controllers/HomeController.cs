using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HomeBanking.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        /// <summary>
        /// Home page
        /// </summary>
        /// <returns>Redirect on swgger</returns>
        [HttpGet]
        public IActionResult Index()
        {
            return new RedirectResult("~/swagger");
        }
    }
}
