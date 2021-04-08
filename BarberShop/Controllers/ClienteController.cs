using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BarberShop.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Home()
        {
            //if (Session["usuarioLogado"] == null || Session["senhaLogado"] == null)
            //{
            //    return RedirectToAction("Login", "Login");
            //}
            //else
            //{
                return View();
            //}
        }
    }
}