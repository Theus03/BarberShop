using BarberShop.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BarberShop.Dados
{
    public class acoesBarbeiro
    {
        int resultEdit;
        conexao con = new conexao();

        public void CadastrarBarbeiro(modelBarbeiro barbeiro)
        {

            MySqlCommand cmd = new MySqlCommand("insert into tbl_barbeiro values(@cd_barbeiro, @nm_barbeiro, @cpf_barbeiro, @telefone_barbeiro)", con.MyConectarBD());


            cmd.Parameters.Add("@cd_barbeiro", MySqlDbType.VarChar).Value = barbeiro.cd_barbeiro;
            cmd.Parameters.Add("@nm_barbeiro", MySqlDbType.VarChar).Value = barbeiro.nm_barbeiro;
            cmd.Parameters.Add("@cpf_barbeiro", MySqlDbType.VarChar).Value = barbeiro.cpf_barbeiro;
            cmd.Parameters.Add("@telefone_barbeiro", MySqlDbType.VarChar).Value = barbeiro.telefone_barbeiro;

            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }
        public List<modelBarbeiro> BuscarBarbeiro()
        {
            List<modelBarbeiro> BarbeiroList = new List<modelBarbeiro>();

            MySqlCommand cmd = new MySqlCommand("select * from tbl_barbeiro", con.MyConectarBD());
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            sd.Fill(dt);

            con.MyDesconectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                BarbeiroList.Add(
                    new modelBarbeiro
                    {
                        cd_barbeiro = Convert.ToString(dr["cd_barbeiro"]),
                        nm_barbeiro = Convert.ToString(dr["nm_barbeiro"]),
                        cpf_barbeiro = Convert.ToString(dr["cpf_barbeiro"]),
                        telefone_barbeiro = Convert.ToString(dr["telefone_barbeiro"])
                    });
            }
            return BarbeiroList;
        }
        public bool editarBarbeiro(modelBarbeiro barbeiro)
        {
            MySqlCommand cmd = new MySqlCommand("update tbl_barbeiro set nm_barbeiro=@nm_barbeiro, cpf_barbeiro=@cpf_barbeiro, telefone_barbeiro=@telefone_barbeiro where cd_barbeiro=@cd_barbeiro", con.MyConectarBD());

            cmd.Parameters.AddWithValue("@nm_barbeiro", barbeiro.nm_barbeiro);
            cmd.Parameters.AddWithValue("@cpf_barbeiro", barbeiro.cpf_barbeiro);
            cmd.Parameters.AddWithValue("@telefone_barbeiro", barbeiro.telefone_barbeiro);
            cmd.Parameters.AddWithValue("@cd_barbeiro", barbeiro.cd_barbeiro);

            try
            {
                resultEdit = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Deu um erro aqui: " + ex);
            }
            con.MyDesconectarBD();
            if (resultEdit >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ExcluirBarbeiro(int id)
        {
            MySqlCommand cmd = new MySqlCommand("delete from tbl_barbeiro where cd_barbeiro=@cd_barbeiro", con.MyConectarBD());

            cmd.Parameters.AddWithValue("@cd_barbeiro", id);

            int i = cmd.ExecuteNonQuery();
            con.MyDesconectarBD();

            if (i >= 1)
                return true;
            else
                return false;
        }
    }
}