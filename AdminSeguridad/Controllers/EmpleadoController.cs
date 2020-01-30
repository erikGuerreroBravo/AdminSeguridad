using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdminSeguridad.ViewModels;
using AdminSeguridad.Models;
using AdminSeguridad.Contract;
using AdminSeguridad.Business;
using AdminSeguridad.Helpers;

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
            ViewBag.IdTurno = new SelectList(Turno.All(), "Id", "StrNombre");
            ViewBag.IdRoles = new SelectList(Rol.All(), "Id", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Administrador")] 
        ///el bind sirve para enlazar los atributos de la vista
        public ActionResult Registro([Bind(Include = "StrNombre,StrApellidoPaterno,StrApellidoMaterno,IdTurno,IdRoles,UsuarioViewModel,HorarioLaboralViewModel,DteFechaIngreso")] EmpleadoViewModel empleadoViewModel)
        {
            if (ModelState.IsValid)
            {
                #region Llenamos el usuario
                Usuario user = new Usuario();
                user.Nombres = empleadoViewModel.StrNombre.Trim();
                user.Apellidos = empleadoViewModel.StrApellidoPaterno + " " + empleadoViewModel.StrApellidoMaterno;
                user.Email = empleadoViewModel.UsuarioViewModel.Email.Trim();
                user.Clave = Funciones.Encrypt(empleadoViewModel.UsuarioViewModel.Clave);
                    
                #endregion

                #region Llenamos el rol del usuario
                Usuario_Rol userRol= new Usuario_Rol();
                userRol.Id_rol = empleadoViewModel.IdRoles;
                #endregion

                #region Llenamos la persona
                Empleado empleado = new Empleado();
                empleado.strNombre = empleadoViewModel.StrNombre.Trim();
                empleado.strApellidoPaterno = empleadoViewModel.StrApellidoPaterno.Trim();
                empleado.strApellidoMaterno = empleadoViewModel.StrApellidoMaterno.Trim();
                empleado.dteFechaIngreso = DateTime.Parse(empleadoViewModel.DteFechaIngreso.Trim());

                #endregion

                #region Horario Laboral
                HorarioLaboral horarioLaboral = new HorarioLaboral();
                horarioLaboral.dteHorarioEntrada = empleadoViewModel.HorarioLaboralViewModel.DteHorarioEntrada;
                horarioLaboral.dteHorarioSalida = empleadoViewModel.HorarioLaboralViewModel.DteHorarioSalida;
                horarioLaboral.dteFechaActual = DateTime.Now;
                #endregion

                #region Turno Empleado
                horarioLaboral.idTurno = empleadoViewModel.IdTurno;
                #endregion

                #region  Carga Global de objetos
                empleado.HorarioLaboral.Add(horarioLaboral);
                user.Empleado.Add(empleado);
                userRol.Usuario = user;
                #endregion

                #region Accion de Inercion del Controlador
                IUsuarioRol usuarioRol = new UsuarioRolBusiness();
                usuarioRol.Insert(userRol);
                #endregion


            }
            return RedirectToAction("Registro", "Empleado");
        }

    }
}