using BarberShop.Models;
using MySql.Data.MySqlClient;
using System;

namespace BarberShop.Dados
{
    public class acoesLogin
    {
        conexao con = new conexao();

        public void TestarUsuario(modelLogin login)
        {
            MySqlCommand cmd = new MySqlCommand("select * from tbl_login where user_login = @user_login and senha_login = @senha_login ", con.MyConectarBD());

            cmd.Parameters.Add("@user_login", MySqlDbType.VarChar).Value = login.user_login;
            cmd.Parameters.Add("@senha_login", MySqlDbType.VarChar).Value = login.senha_login;

            MySqlDataReader leitor;

            leitor = cmd.ExecuteReader();

            if (leitor.HasRows)
            {
                while (leitor.Read())
                {
                    login.user_login = Convert.ToString(leitor["user_login"]);
                    login.senha_login = Convert.ToString(leitor["senha_login"]);
                    login.tipo_login = Convert.ToString(leitor["tipo_login"]);
                }
            }

            else
            {
                login.user_login = null;
                login.senha_login = null;
                login.tipo_login = null;
            }
            con.MyDesconectarBD();
        }

    }
}
