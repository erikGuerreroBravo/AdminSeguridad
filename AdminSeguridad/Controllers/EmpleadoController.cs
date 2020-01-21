using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdminSeguridad.ViewModels;
using AdminSeguridad.Models;

namespace AdminSeguridad.Controllers
{
    
    public class EmpleadoController : Controller
    {
        IdentityEntities contexto = new IdentityEntities(); //creamos el conteto de datos

        [Authorize(Roles ="Administrador")]
        [HttpGet]
        public ActionResult Registro()
        {
            RepositorioGenerico.Repositorio<Turno> Turno = new  RepositorioGenerico.Repositorio<Turno>(contexto);
            RepositorioGenerico.Repositorio<Rol> Rol = new RepositorioGenerico.Repositorio<Rol>(contexto);
            ViewBag.Turno = new SelectList(Turno.All(), "Id", "StrNombre");
            ViewBag.Roles = new SelectList(Rol.All(), "Id", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Administrador")]
        public ActionResult Registro(EmpleadoViewModel empleadoViewModel)
        {
            return View();
        }

    }
}