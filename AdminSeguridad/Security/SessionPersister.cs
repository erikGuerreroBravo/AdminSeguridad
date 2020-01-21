using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminSeguridad.Security
{
    public static class SessionPersister
    {
        /// <summary>
        /// Esta propiedad de la sesion regresa un objeto del tipo AccountViewmODEL
        /// </summary>
        public static IUser AccountSession
        {
            get
            {
                if (HttpContext.Current == null)
                    return null;
                IUser user = (IUser)HttpContext.Current.Session["AccountManager"];
                //AccountViewModel sessionObject = (AccountViewModel)HttpContext.Current.Session["AccountManager"]; ///nombre de la sesion de objeto
                if (user != null)
                    //return sessionObject as AccountViewModel;
                    return user; 
                return null;
            }
            set
            {
                HttpContext.Current.Session["AccountManager"] = value;
            }

         
        }
        
        /// <summary>
        /// Este metodo se encarga de eliminar todos los elementos establecidos en la sesion asi como todos
        /// los elementos de autenticacion de la sesion.
        /// </summary>
        public static void LogOutSession()
        {
            HttpContext.Current.Session.RemoveAll();
            HttpContext.Current.Session.Abandon();
        }


    }
}