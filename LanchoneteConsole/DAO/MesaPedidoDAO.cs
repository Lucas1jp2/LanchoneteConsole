using LanchoneteConsole.Database;
using LanchoneteConsole.Interface;
using LanchoneteConsole.Mapeamento;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanchoneteConsole.DAO
{
    internal class MesaPedidoDAO : IDAO<MesaPedido>
    {
        public void Insert(MesaPedido mp)
        {
            try
            {
                string sql = @"INSERT INTO MesaPedido(fk_pedidoID, fk_mesaID)
                                VALUES (@fk_pedidoID, @fk_mesaID)";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@fk_pedidoID", mp.fk_pedidoID);
                    cmd.Parameters.AddWithValue("@fk_mesaID", mp.fk_mesaID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao inserir vínculo de mesa e pedido: {ex.Message}");
            }
        }

        public void Update(MesaPedido mp)
        {
            try
            {
                string sql = @"UPDATE MesaPedido SET fk_pedidoID = @fk_pedidoID, fk_mesaID = @fk_mesaID
                                WHERE mesaPedidoID = @mesaPedidoID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@fk_pedidoID", mp.fk_pedidoID);
                    cmd.Parameters.AddWithValue("@fk_mesaID", mp.fk_mesaID);
                    cmd.Parameters.AddWithValue("@mesaPedidoID", mp.MesaPedidoID);
                    var linhas = cmd.ExecuteNonQuery();

                    if (linhas == 0)
                        throw new Exception("Nenhum registro foi atualizado");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(int mesaPedidoID)
        {
            try
            {
                string sql = "DELETE FROM MesaPedido WHERE mesaPedidoID = @mesaPedidoID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@mesaPedidoID", mesaPedidoID);
                    var linhasAfetadas = cmd.ExecuteNonQuery();

                    if (linhasAfetadas == 0)
                        throw new Exception("Nenhum registro encontrado com esse ID..");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<MesaPedido> GetAll()
        {
            List<MesaPedido> lista = new List<MesaPedido>();
            try
            {
                string sql = "SELECT * FROM MesaPedido ORDER BY mesaPedidoID";

                using (var conexao = Conexao.Conectar())
                using (var comando = new MySqlCommand(sql, conexao))
                using (MySqlDataReader dr = comando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        MesaPedido mp = new MesaPedido
                        {
                            MesaPedidoID = Convert.ToInt32(dr["mesaPedidoID"]),
                            fk_pedidoID = Convert.ToInt32(dr["fk_pedidoID"]),
                            fk_mesaID = Convert.ToInt32(dr["fk_mesaID"])
                        };

                        lista.Add(mp);
                    }
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar os vínculos de mesa e pedido: {ex.Message}");
            }
        }

        public MesaPedido BuscarID(int id_mesaPedido)
        {
            try
            {
                string sql = "SELECT * FROM MesaPedido WHERE mesaPedidoID = @mesaPedidoID";

                using (var conexao = Conexao.Conectar())
                using (var comando = new MySqlCommand(sql, conexao))
                {
                    comando.Parameters.AddWithValue("@mesaPedidoID", id_mesaPedido);

                    using (MySqlDataReader dr = comando.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            MesaPedido mp = new MesaPedido();
                            mp.MesaPedidoID = Convert.ToInt32(dr["mesaPedidoID"]);
                            mp.fk_pedidoID = Convert.ToInt32(dr["fk_pedidoID"]);
                            mp.fk_mesaID = Convert.ToInt32(dr["fk_mesaID"]);

                            return mp;
                        }
                        else
                        {
                            throw new Exception("Nenhum registro de mesa/pedido encontrado com esse ID.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
