﻿//cargamos el middleware de seguridad de owin 
using Microsoft.Owin.Security;
using System.Web;
//cargamos la libreria necesaria para cargar el HttpUnauthorizedResult
using System.Web.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;


namespace AdminSeguridad.Helpers
{
    public class ChallengeResult : HttpUnauthorizedResult
    {
        // Utilizado como protección XSRF cuando agregamos inicio de sesión externo.
        public const string XsrfKey = "XsrfId";
        public ChallengeResult(string provider, string redirectUri) : this(provider, redirectUri, null)
        {
        }

        public ChallengeResult(string provider, string redirectUri, string userId)
        {
            LoginProvider = provider;
            RedirectUri = redirectUri;
            UserId = userId;
        }
        public string LoginProvider { get; set; }
        public string RedirectUri { get; set; }
        public string UserId { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
            if (UserId != null)
                properties.Dictionary[XsrfKey] = UserId;

            context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);

            base.ExecuteResult(context);
        }
    }
}