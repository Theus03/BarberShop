using BarberShop.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BarberShop.Dados
{
    public class acoesReserva
    {
        int resultEdit;
        conexao con = new conexao();

        public void TestarReserva(modelReserva reserva) //verificar se a agenda está reservada      
        {
            MySqlCommand cmd = new MySqlCommand("select * from tbl_reserva where data_reserva = @data and hora = @hora", con.MyConectarBD());
            cmd.Parameters.Add("@data", MySqlDbType.VarChar).Value = reserva.data_reserva;
            cmd.Parameters.Add("@hora", MySqlDbType.VarChar).Value = reserva.hora;
            MySqlDataReader leitor;

            leitor = cmd.ExecuteReader();

            if (leitor.HasRows)
            {
                while (leitor.Read())
                {
                    reserva.confReserva = "0";
                }
            }

            else
            {
                reserva.confReserva = "1";
            }
            con.MyDesconectarBD();
        }

        public void inserirReserva(modelReserva cm)// Cadastrar o atendimento no BD 
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbl_reserva(cd_reserva, cd_cliente, cd_barbeiro, data_reserva, hora) values (default, @cd_cliente, @cd_barbeiro, @data_reserva, @hora)", con.MyConectarBD());
            cmd.Parameters.Add("@cd_cliente", MySqlDbType.VarChar).Value = cm.cd_cliente;
            cmd.Parameters.Add("@cd_barbeiro", MySqlDbType.VarChar).Value = cm.cd_barbeiro;
            cmd.Parameters.Add("@data_reserva", MySqlDbType.VarChar).Value = cm.data_reserva;
            cmd.Parameters.Add("@hora", MySqlDbType.VarChar).Value = cm.hora;

            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();

        }

        public List<modelReserva> GetAgendaReserva()
        {
            List<modelReserva> Reservalist = new List<modelReserva>();

            MySqlCommand cmd = new MySqlCommand("select tbl_reserva.cd_reserva, tbl_reserva.data_reserva,tbl_reserva.hora, tbl_cliente.nm_cliente,tbl_barbeiro.nm_barbeiro from tbl_reserva, tbl_cliente,tbl_barbeiro where tbl_reserva.cd_barbeiro = tbl_barbeiro.cd_barbeiro and tbl_reserva.cd_cliente = tbl_cliente.cd_cliente;", con.MyConectarBD());
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            sd.Fill(dt);

            con.MyDesconectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                Reservalist.Add(
                    new modelReserva
                    {
                        cd_reserva = Convert.ToString(dr["cd_reserva"]),
                        nm_barbeiro = Convert.ToString(dr["nm_barbeiro"]),
                        nm_cliente = Convert.ToString(dr["nm_cliente"]),
                        data_reserva = Convert.ToString(dr["data_reserva"]),
                        hora = Convert.ToString(dr["hora"])
                    });
            }
            return Reservalist;
        }

        public List<modelReserva> BuscarReserva()
        {
            List<modelReserva> Reservalist = new List<modelReserva>();

            MySqlCommand cmd = new MySqlCommand("select * from tbl_reserva", con.MyConectarBD());
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            sd.Fill(dt);

            con.MyDesconectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                Reservalist.Add(
                    new modelReserva
                    {
                        cd_reserva = Convert.ToString(dr["cd_reserva"]),
                        data_reserva = Convert.ToString(dr["data_reserva"]),
                        hora = Convert.ToString(dr["hora"])
                    });
            }
            return Reservalist;
        }

        public bool editarReserva(modelReserva reserva)
        {
            MySqlCommand cmd = new MySqlCommand("update tbl_reserva set data_reserva=@data_reserva, hora=@hora where cd_reserva=@cd_reserva", con.MyConectarBD());

            cmd.Parameters.AddWithValue("@data_reserva", reserva.data_reserva);
            cmd.Parameters.AddWithValue("@hora", reserva.hora);
            cmd.Parameters.AddWithValue("@cd_reserva", reserva.cd_reserva);
            
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
    }
}
