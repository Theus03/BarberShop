using BarberShop.Dados;
using BarberShop.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BarberShop.Controllers
{
    public class BarbeiroController : Controller
    {
        acoesReserva ac = new acoesReserva();
        acoesBarbeiro ab = new acoesBarbeiro();

        public void carregaBarbeiros()
        {
            List<SelectListItem> ag = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server=localhost;DataBase=db_barbershop;User=root;pwd=Matheus2003"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbl_barbeiro order by nm_barbeiro;", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    ag.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }

                con.Close();
                con.Open();
            }

            ViewBag.barbeiro = new SelectList(ag, "Value", "Text");
        }

        public void carregaClientes()
        {
            List<SelectListItem> ag = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server=localhost;DataBase=db_barbershop;User=root;pwd=Matheus2003"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbl_cliente order by nm_cliente;", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    ag.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }

                con.Close();
                con.Open();
            }

            ViewBag.cliente = new SelectList(ag, "Value", "Text");
        }
        // GET: Barbeiro

        public ActionResult Home()
        {
            if (Session["usuarioLogado"] == null && Session["senhaLogado"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else if (Session["tipoLogado2"] != null)
            {
                return RedirectToAction("SemAcesso", "Barbeiro");
            }
            else
            {
                return View();
            }
        }
        public ActionResult AgendarReserva()
        {
            if (Session["usuarioLogado"] == null && Session["senhaLogado"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else if (Session["tipoLogado2"] != null){
                return RedirectToAction("SemAcesso", "Barbeiro");
            }
            else
            {
                carregaBarbeiros();
                carregaClientes();
                return View();
            }
        }


        [HttpPost]
        public ActionResult AgendarReserva(modelReserva reserva)
        {
            carregaBarbeiros();
            carregaClientes();
            reserva.cd_barbeiro = Request["barbeiro"];
            reserva.cd_cliente = Request["cliente"];
            ac.TestarReserva(reserva);

            if (reserva.confReserva == "1")
            {
                ac.inserirReserva(reserva);
                ViewBag.msg = " ✅ Agendamento Realizado com sucesso!";
                return View();
            }

            else if (reserva.confReserva == "0")
            {
                ViewBag.msg = " ❌ Horário indisponível, selecione outro horário";
                return View();
            }
            return View();
        }
        public ActionResult VerReserva(modelLogin verLogin)
        {
            if (Session["usuarioLogado"] == null && Session["senhaLogado"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else if (Session["tipoLogado2"] != null)
            {
                return RedirectToAction("SemAcesso", "Barbeiro");
            }
            else
            {
                acoesReserva dbhandle = new acoesReserva();
                ModelState.Clear();
                return View(dbhandle.GetAgendaReserva());
            }
        }
        public ActionResult EditarReserva(string id)
        {
            return View(ac.BuscarReserva().Find(reserva => reserva.cd_reserva == id));

        }
        [HttpPost]
        public ActionResult EditarReserva(modelReserva reserva)
        {
            try
            {
                ac.editarReserva(reserva);
                ViewBag.msg = " ✅ Atualizado com sucesso!";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Deu um erro aqui: " + ex);
                return View();
            }
            return View();
        }
        public ActionResult CancelarReserva(int id)
        {
            if (Session["usuarioLogado"] == null || Session["senhaLogado"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                try
                {
                    acoesReserva sdb = new acoesReserva();

                    if (sdb.CancelarReserva(id))
                    {
                        ViewBag.AlertMsg = "Reserva cancelada com sucesso";
                    }
                    return RedirectToAction("VerReserva");
                }
                catch
                {
                    return View();
                }
            }
        }

        public ActionResult CadBarbeiro()
        {
            return View();
        }

        [HttpPost]
        public ActionResult cadBarbeiro(modelBarbeiro barbeiro)
        {
            ab.CadastrarBarbeiro(barbeiro);
            ViewBag.confCadastro = " ✅ Cadastro Realizado com sucesso";
            return View();
        }
        public ActionResult VerBarbeiro(modelBarbeiro barbeiro)
        {
            if (Session["usuarioLogado"] == null && Session["senhaLogado"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else if (Session["tipoLogado2"] != null)
            {
                return RedirectToAction("SemAcesso", "Barbeiro");
            }
            else
            {
                ModelState.Clear();
                return View(ab.BuscarBarbeiro());
            }
        }
        public ActionResult EditarBarbeiro(string id)
        {
            return View(ab.BuscarBarbeiro().Find(barbeiro => barbeiro.cd_barbeiro == id));

        }
        [HttpPost]
        public ActionResult EditarBarbeiro(modelBarbeiro barbeiro)
        {
            try
            {
                ab.editarBarbeiro(barbeiro);
                ViewBag.msg = " ✅ Atualizado com sucesso!";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Deu um erro aqui: " + ex);
                return View();
            }
            return View();
        }
        public ActionResult ExcluirBarbeiro(int id)
        {
            if (Session["usuarioLogado"] == null || Session["senhaLogado"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                try
                {
                    if (ab.ExcluirBarbeiro(id))
                    {
                        ViewBag.AlertMsg = "Barbeiro excluído com sucesso";
                    }
                    return RedirectToAction("VerBarbeiro");
                }
                catch
                {
                    return View();
                }
            }
        }
        public ActionResult SemAcesso()
        {
            return View();
        }
    }
}
