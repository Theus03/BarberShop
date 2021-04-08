using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BarberShop.Models
{
    public class modelReserva
    {
        public string cd_reserva { get; set; }
        public string cd_cliente { get; set; } 
        public string cd_barbeiro { get; set; }
        public string data_reserva { get; set; }
        public string hora { get; set; }

        //NÃO ESTÁ NO BANCO DE DADOS
        public string confReserva { get; set; }
    }
}