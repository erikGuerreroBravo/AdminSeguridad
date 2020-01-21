using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace AdminSeguridad.Controllers
{
    [AllowAnonymous]
    public class EjemploController : Controller
    {
        // GET: Ejemplo
        [Authorize(Roles = "Administrador")]
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var dataTemp = User.Identity as ClaimsPrincipal;
                var claim = dataTemp.Claims.Where(p => p.ValueType == "NameIdentifier");
                
                ViewBag.ID = claim;
            }
            return Content(ViewBag.ID);
        }
    }
}