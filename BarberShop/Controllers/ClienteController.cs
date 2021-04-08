﻿using BarberShop.Dados;
using BarberShop.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BarberShop.Controllers
{
    public class ClienteController : Controller
    {
        acoesReserva ac = new acoesReserva();

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
        public ActionResult AgendarReserva()
        {
            carregaBarbeiros();
            return View();
        }


        [HttpPost]
        public ActionResult AgendarReserva(modelReserva reserva)
        {
            carregaBarbeiros();
            reserva.cd_barbeiro = Request["barbeiro"];
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
    }
}