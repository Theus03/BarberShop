using BarberShop.Dados;
using BarberShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BarberShop.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        acoesLogin acoesLogin = new acoesLogin();
        [HttpPost]
        public ActionResult Login(modelLogin verLogin)
        {
            acoesLogin.TestarUsuario(verLogin);

            if (verLogin.user_login != null && verLogin.senha_login != null)
            {
                FormsAuthentication.SetAuthCookie(verLogin.user_login, false);
                Session["usuarioLogado"] = verLogin.user_login.ToString();
                Session["senhaLogado"] = verLogin.senha_login.ToString();

                if (verLogin.tipo_login == "1")
                {
                    Session["tipoLogado1"] = verLogin.tipo_login.ToString(); //=1;
                }
                else
                {
                    Session["tipoLogado2"] = verLogin.tipo_login.ToString();//=2
                }


                return RedirectToAction("About", "Home");
            }

            else
            {
                ViewBag.msgLogar = "Usuário não encontrado. Verifique o nome do usuário e a senha";
                return View();
            }
        }
    }
}
