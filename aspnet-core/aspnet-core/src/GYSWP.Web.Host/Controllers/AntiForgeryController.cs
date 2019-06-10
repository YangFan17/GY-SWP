using Microsoft.AspNetCore.Antiforgery;
using GYSWP.Controllers;

namespace GYSWP.Web.Host.Controllers
{
    public class AntiForgeryController : GYSWPControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
