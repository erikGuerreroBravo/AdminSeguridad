using AdminSeguridad.Contract;
using AdminSeguridad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminSeguridad.Business
{
    public class UsuarioRolBusiness : IUsuarioRol
    {
        IdentityEntities contexto = new IdentityEntities(); //creamos el conteto de datos

        /// <summary>
        /// Este metodo se encarga de insertar un UsuarioRol
        /// </summary>
        /// <param name="usuario_Rol">Usuario_Rol</param>
        /// <returns>respuesta booleana</returns>
        public bool Insert(Usuario_Rol usuario_Rol)
        {
            bool respuesta = false;
            try
            {
                RepositorioGenerico.Repositorio<Usuario_Rol> repositorio = new RepositorioGenerico.Repositorio<Usuario_Rol>(contexto);
                repositorio.Create(usuario_Rol);
                respuesta = true;

            }
            catch (Exception ex)
            {
                string mensajeErr = ex.Message;
                
            }
            return respuesta;
        }



    }
}