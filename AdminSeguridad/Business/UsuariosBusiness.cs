using AdminSeguridad.Contract;
using AdminSeguridad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminSeguridad.Business
{
    public class UsuariosBusiness : IUsuarios
    {
        IdentityEntities contexto = new IdentityEntities(); //creamos el conteto de datos

        /// <summary>
        /// Este metodo se encarga de regresar una lista de usuarios
        /// </summary>
        /// <returns>lista de usuarios</returns>
        public List<Usuario> Consultar()
        {
            RepositorioGenerico.Repositorio<Usuario> repositorio = new RepositorioGenerico.Repositorio<Usuario>(contexto);
            List<Usuario> usuarios = repositorio.All("Empleado", "Empleado.HorarioLaboral", "Empleado.HorarioLaboral.Turno");
            return usuarios;

        }
    }
}