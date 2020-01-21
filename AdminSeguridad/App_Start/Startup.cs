using System;
using System.Threading.Tasks;
//cargamos la libreria necesaria para trabajar con la autenticacion de ASP.NET
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(AdminSeguridad.App_Start.Startup))]

namespace AdminSeguridad.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Para obtener más información sobre cómo configurar la aplicación, visite https://go.microsoft.com/fwlink/?LinkID=316888
            app.UseCookieAuthentication(
                new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions()
                {
                    //especificamos el tipo de autenticacion que vamos a necesitar
                    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                    //login path indica que debe cambiar al status 401 unauthorized por el status 302 el cual nos redicrecciona a una ruta especifica
                    LoginPath = new PathString("/Account/Login")

                }
                );
            ///utilizamos una cookie para autenticacion externa de la siguiente manera 
            ///utilizaremos proveedores de servicios de autenticacion como facebook y google
            app.UseExternalSignInCookie(
                DefaultAuthenticationTypes.ExternalCookie ///utilizamos una cookie externa    
            );
        }
    }
}
