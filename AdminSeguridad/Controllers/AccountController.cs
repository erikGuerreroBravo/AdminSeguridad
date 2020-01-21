//cargamos entityFraework para acceso a datos
using AdminSeguridad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//cargamos el repositorio generico para operaciones CRUD
using RepositorioGenerico;
using AdminSeguridad.Helpers;
using AdminSeguridad.ViewModels;
//esta libreria la utilizamos para cargar los claims
using System.Security.Claims;
//esta libreria la utilizamos para cargar el tipo de autenticacion
using Microsoft.AspNet.Identity;
//esta libreria la cargamos para utilizar : IAuthenticationManager
using Microsoft.Owin.Security;
using System.Threading.Tasks;

namespace AdminSeguridad.Controllers
{
    public class AccountController : Controller
    {

        IdentityEntities contexto = new IdentityEntities(); //creamos el conteto de datos
        // GET: Account
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel loginView, string returnUrl)
        {
            ActionResult result; //esto se debe inicializar
            string clave = Funciones.Encrypt(loginView.Password); //encryptamos el password
            RepositorioGenerico.Repositorio<Usuario> Usuario = new RepositorioGenerico.Repositorio<Usuario>(contexto); //le pasamos el contexto de datos
            Usuario User = Usuario.Retrieve(p => p.Email == loginView.Email && p.Clave == clave, "Usuario_Rol", "Usuario_Rol.Rol");
            if (User != null)
            {
                result = SigInUser(User, loginView.RememberMe, returnUrl);
            }
            else
            {
                return View(loginView);
            }

            return result;

        }



        private ActionResult SigInUser(Usuario user, bool rememberMe, string returnUrl)
        {
            ActionResult Result;
            //un claims puede almacenar cualquier tipo de informacion del usuario, nombre, mail, password, lo que sea
            List<Claim> Claims = new List<Claim>(); //listado de claims
            Claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())); //primer claims
            Claims.Add(new Claim(ClaimTypes.Email, user.Email));
            Claims.Add(new Claim(ClaimTypes.Name, user.Nombres));
            ///podemos crear un tipo de claims personalizado
            Claims.Add(new Claim("FullName", $"{user.Nombres} {user.Apellidos}"));
            ///estos claims se almacenan en la cookie para identificar al usuario y sus atributos o permisos
            ///
            //ahora establñecemos los claims con los roles del usuario
            if (user.Usuario_Rol != null && user.Usuario_Rol.Any())
            {
                //agregamos los roles que trae el usuario
                Claims.AddRange(user.Usuario_Rol.Select(p => new Claim(ClaimTypes.Role, p.Rol.Nombre)));
            }
            var Identity = new ClaimsIdentity(Claims, DefaultAuthenticationTypes.ApplicationCookie); //entonces ahora creamos una identidad basada en claims con sus roles permisos y nombre del usuario
            ///cuando el usuario se logea se crea una cookie de autenticacion 
            ///
            //este AutenticationManager se encarga de administrarla cookie y da el seguimiento a todo el sitio con todo y los permisos del usuario
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = rememberMe }, Identity); //el rememberMe es para recordarlo true/false

            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = Url.Action("Index", "Home");
            }
            Result = Redirect(returnUrl);


            return Result;

        }


        public ActionResult LogOff()
        {
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            return RedirectToAction("Index", "Home");
        }


        public ActionResult ExternalLinkLogin(string provider, string returnUrl)
        {
            string UserID = null;
            //obtenemos el identificador del usuario autenticado
            if (this.User.Identity.IsAuthenticated && User is ClaimsPrincipal)
            {
                var Identity = User as ClaimsPrincipal;
                var Claims = Identity.Claims.ToList();
                UserID = Claims.FirstOrDefault(p => p.Type == ClaimTypes.NameIdentifier).Value;
            }
            //solicitamos el redirect al proveedor externo
            return new ChallengeResult(provider, Url.Action("ExternalLinkLoginCallback", "Account", new { ReturnUrl = returnUrl }), UserID);
        }


        public async Task<ActionResult> ExternalLinkLoginCallback()
        {
            ActionResult Result;
            // Obtener la información devuelta por el proveedor externo
            var LoginInfo =
                await HttpContext.GetOwinContext().
                Authentication.GetExternalLoginInfoAsync(
                    ChallengeResult.XsrfKey, User.Identity.GetUserId());

            if (LoginInfo == null)
                Result = Content("No se pudo realizar la autenticación con el proveedor externo");
            else
            {
                // El usuario ha sido autenticado por el proveedor externo!
                // Obtener la llave del proveedor de autenticación.
                // Esta llave es específica del usuario.
                string ProviderKey = LoginInfo.Login.ProviderKey;
                // Obtener el nombre del proveedor de autenticación.
                string ProviderName = LoginInfo.Login.LoginProvider;
                // Enlazar los datos de la cuenta externa con la cuenta de usuario local. 
                int IdUsuario = int.Parse(Funciones.GetClaimInfo(ClaimTypes.NameIdentifier));
                //User.Identity.GetUserId<int>()
                Repositorio<Usuario> Usuario = new Repositorio<Usuario>(contexto);
                Repositorio.Excepcion += Repositorio_Excepcion;
                Usuario.Update(x => x.Id == IdUsuario, "ProviderKey", ProviderKey);
                Usuario.Update(x => x.Id == IdUsuario, "ProviderName", ProviderName);
                Repositorio.Excepcion -= Repositorio_Excepcion;
                Result = Content($"Se ha enlazado la cuenta local con la cuenta de {ProviderName}");
            }
            return Result;
        }

        private void Repositorio_Excepcion(object sender, ExceptionEvenArgs e)
        {

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Solicitamos un Redirect al proveedor externo.
            return new
                ChallengeResult(provider, Url.Action(
                    "ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }


        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            ActionResult Result;

            // Obtener la información devuelta por el proveedor externo
            var LoginInfo = await HttpContext.GetOwinContext().
                Authentication.GetExternalLoginInfoAsync();

            if (LoginInfo == null)
                // No se pudo autenticar.
                Result = RedirectToAction("Login");
            else
            {
                // El usuario ha sido autenticado por el proveedor externo!
                // Obtener la llave del proveedor que identifica al usuario.
                string ProviderKey = LoginInfo.Login.ProviderKey;
                // Buscar al usuario
                Repositorio<Usuario> Usuario = new Repositorio<Usuario>(contexto);

                var User = Usuario.Retrieve(x => x.ProviderKey == ProviderKey);
                if (User != null)// Se ha encontrado al usuario. Iniciar la sesión del usuario.                    
                    Result = SigInUser(User, false, returnUrl);
                else
                    Result = Content($"Imposible iniciar sesión con {LoginInfo.Login.LoginProvider}");
            }
            return Result;
        }

    }
}