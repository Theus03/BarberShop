using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BarberShop.Models
{
    public class modelReserva
    {
        [DisplayName("Código")]
        public string cd_reserva { get; set; }
        [DisplayName("Cliente")]
        public string cd_cliente { get; set; }
        [DisplayName("Barbeiro")]
        public string cd_barbeiro { get; set; }
        [DisplayName("Data")]
        public string data_reserva { get; set; }
        [DisplayName("Hora")]
        public string hora { get; set; }

        //NÃO ESTÁ NO BANCO DE DADOS
        public string confReserva { get; set; }
        [DisplayName("Cliente")]
        public string nm_cliente { get; set; }
        [DisplayName("Barbeiro")]
        public string nm_barbeiro { get; set; }

    }
}