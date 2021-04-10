using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BarberShop.Models
{
    public class modelBarbeiro
    {
        [DisplayName("Código")]
        public string cd_barbeiro { get; set; }

        [DisplayName("Nome")]
        public string nm_barbeiro { get; set; }

        [DisplayName("CPF")]
        public string cpf_barbeiro { get; set; }

        [DisplayName("Telefone")]
        public string telefone_barbeiro { get; set; }
    }
}