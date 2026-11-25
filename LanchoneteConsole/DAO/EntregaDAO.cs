using LanchoneteConsole.Database;
using LanchoneteConsole.Interface;
using LanchoneteConsole.Mapeamento;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanchoneteConsole.DAO
{
    internal class EntregaDAO : IDAO<Entrega>
    {
        public void Insert(Entrega e)
        {
            try
            {
                string sql = @"INSERT INTO Entrega(taxaEntrega, entregador, observacao, fk_enderecoID, fk_pedidoID)
                                VALUES (@taxaEntrega, @entregador, @observacao, @fk_enderecoID, @fk_pedidoID)";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@taxaEntrega", e.TaxaEntrega);
                    cmd.Parameters.AddWithValue("@entregador", string.IsNullOrEmpty(e.Entregador) ? DBNull.Value : (object)e.Entregador);
                    cmd.Parameters.AddWithValue("@observacao", string.IsNullOrEmpty(e.Observacao) ? DBNull.Value : (object)e.Observacao);
                    cmd.Parameters.AddWithValue("@fk_enderecoID", e.fk_enderecoID);
                    cmd.Parameters.AddWithValue("@fk_pedidoID", e.fk_pedidoID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao inserir entrega: {ex.Message}");
            }
        }

        public void Update(Entrega e)
        {
            try
            {
                string sql = @"UPDATE Entrega SET taxaEntrega = @taxaEntrega, entregador = @entregador, observacao = @observacao, fk_enderecoID = @fk_enderecoID, fk_pedidoID = @fk_pedidoID
                                WHERE entregaID = @entregaID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@taxaEntrega", e.TaxaEntrega);
                    cmd.Parameters.AddWithValue("@entregador", string.IsNullOrEmpty(e.Entregador) ? DBNull.Value : (object)e.Entregador);
                    cmd.Parameters.AddWithValue("@observacao", string.IsNullOrEmpty(e.Observacao) ? DBNull.Value : (object)e.Observacao);
                    cmd.Parameters.AddWithValue("@fk_enderecoID", e.fk_enderecoID);
                    cmd.Parameters.AddWithValue("@fk_pedidoID", e.fk_pedidoID);
                    cmd.Parameters.AddWithValue("@entregaID", e.EntregaID);
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

        public void Delete(int entregaID)
        {
            try
            {
                string sql = "DELETE FROM Entrega WHERE entregaID = @entregaID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@entregaID", entregaID);
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

        public List<Entrega> GetAll()
        {
            List<Entrega> entregas = new List<Entrega>();
            try
            {
                string sql = "SELECT * FROM Entrega ORDER BY entregaID";

                using (var conexao = Conexao.Conectar())
                using (var comando = new MySqlCommand(sql, conexao))
                using (MySqlDataReader dr = comando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Entrega entrega = new Entrega
                        {
                            EntregaID = Convert.ToInt32(dr["entregaID"]),
                            TaxaEntrega = Convert.ToDecimal(dr["taxaEntrega"]),
                            Entregador = dr["entregador"] == DBNull.Value ? null : dr["entregador"].ToString(),
                            Observacao = dr["observacao"] == DBNull.Value ? null : dr["observacao"].ToString(),
                            fk_enderecoID = Convert.ToInt32(dr["fk_enderecoID"]),
                            fk_pedidoID = Convert.ToInt32(dr["fk_pedidoID"])
                        };

                        entregas.Add(entrega);
                    }
                }
                return entregas;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar as entregas: {ex.Message}");
            }
        }

        public Entrega BuscarID(int id_entrega)
        {
            try
            {
                string sql = "SELECT * FROM Entrega WHERE entregaID = @entregaID";

                using (var conexao = Conexao.Conectar())
                using (var comando = new MySqlCommand(sql, conexao))
                {
                    comando.Parameters.AddWithValue("@entregaID", id_entrega);

                    using (MySqlDataReader dr = comando.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Entrega entrega = new Entrega();
                            entrega.EntregaID = Convert.ToInt32(dr["entregaID"]);
                            entrega.TaxaEntrega = Convert.ToDecimal(dr["taxaEntrega"]);
                            entrega.Entregador = dr["entregador"] == DBNull.Value ? null : dr["entregador"].ToString();
                            entrega.Observacao = dr["observacao"] == DBNull.Value ? null : dr["observacao"].ToString();
                            entrega.fk_enderecoID = Convert.ToInt32(dr["fk_enderecoID"]);
                            entrega.fk_pedidoID = Convert.ToInt32(dr["fk_pedidoID"]);

                            return entrega;
                        }
                        else
                        {
                            throw new Exception("Nenhuma entrega encontrada com esse ID.");
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