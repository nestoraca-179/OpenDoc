﻿using SLO.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;

namespace SLO.Controllers
{
    public class UserController : Repository
    {
        public static int LogIn(string username, string password)
        {
            int result = 0;

            try
            {
                string encrypted_pass = SecurityController.Encrypt(password);
                Usuario user = db.Usuario.SingleOrDefault(u => u.username == username && u.password == encrypted_pass);

                if (user != null)
                {
                    if (user.activo)
                    {
                        FormsAuthentication.SetAuthCookie(username, true);
                        HttpContext.Current.Session["USER"] = user;

                        result = 1;
                    }
                    else
                    {
                        result = 2;
                    }
                }
            }
            catch (Exception ex)
            {
                result = 3;
                IncidentController.CreateIncident("ERROR INICIANDO SESION " + username, ex);
            }

            return result;
        }

        public static void LogOut()
        {
            FormsAuthentication.SignOut();
            HttpContext.Current.Session.Clear();
        }
    }
}