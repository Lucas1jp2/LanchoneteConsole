using LanchoneteConsole.Database;
using LanchoneteConsole.Interface;
using LanchoneteConsole.Mapeamento;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanchoneteConsole.DAO
{
    internal class PedidoDAO : IDAO<Pedido>
    {
        public void Insert(Pedido p)
        {
            try
            {
                string sql = @"INSERT INTO Pedido(dataPedido, preferencial, status, observacao, tipo, total, valorDesconto, porcentagemDesconto, totalDesconto, fk_funcionarioID, fk_clienteID)
                                VALUES (@dataPedido, @preferencial, @status, @observacao, @tipo, @total, @valorDesconto, @porcentagemDesconto, @totalDesconto, @fk_funcionarioID, @fk_clienteID)";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.Add("@dataPedido", MySqlDbType.Date).Value = p.DataPedido.ToDateTime(TimeOnly.MinValue);
                    cmd.Parameters.AddWithValue("@preferencial", p.Preferencial);
                    cmd.Parameters.AddWithValue("@status", string.IsNullOrEmpty(p.Status) ? DBNull.Value : (object)p.Status);
                    cmd.Parameters.AddWithValue("@observacao", string.IsNullOrEmpty(p.Observacao) ? DBNull.Value : (object)p.Observacao);
                    cmd.Parameters.AddWithValue("@tipo", string.IsNullOrEmpty(p.Tipo) ? DBNull.Value : (object)p.Tipo);
                    cmd.Parameters.AddWithValue("@total", p.Total);
                    cmd.Parameters.AddWithValue("@valorDesconto", p.ValorDesconto.HasValue ? (object)p.ValorDesconto.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@porcentagemDesconto", p.PorcentagemDesconto.HasValue ? (object)p.PorcentagemDesconto.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@totalDesconto", p.TotalDesconto.HasValue ? (object)p.TotalDesconto.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@fk_funcionarioID", p.fk_funcionarioID);
                    cmd.Parameters.AddWithValue("@fk_clienteID", p.fk_clienteID.HasValue ? (object)p.fk_clienteID.Value : DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao inserir pedido: {ex.Message}");
            }
        }

        public void Update(Pedido p)
        {
            try
            {
                string sql = @"UPDATE Pedido SET dataPedido = @dataPedido, preferencial = @preferencial, status = @status, observacao = @observacao, tipo = @tipo, total = @total, valorDesconto = @valorDesconto, porcentagemDesconto = @porcentagemDesconto, totalDesconto = @totalDesconto, fk_funcionarioID = @fk_funcionarioID, fk_clienteID = @fk_clienteID
                                WHERE pedidoID = @pedidoID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.Add("@dataPedido", MySqlDbType.Date).Value = p.DataPedido.ToDateTime(TimeOnly.MinValue);
                    cmd.Parameters.AddWithValue("@preferencial", p.Preferencial);
                    cmd.Parameters.AddWithValue("@status", string.IsNullOrEmpty(p.Status) ? DBNull.Value : (object)p.Status);
                    cmd.Parameters.AddWithValue("@observacao", string.IsNullOrEmpty(p.Observacao) ? DBNull.Value : (object)p.Observacao);
                    cmd.Parameters.AddWithValue("@tipo", string.IsNullOrEmpty(p.Tipo) ? DBNull.Value : (object)p.Tipo);
                    cmd.Parameters.AddWithValue("@total", p.Total);
                    cmd.Parameters.AddWithValue("@valorDesconto", p.ValorDesconto.HasValue ? (object)p.ValorDesconto.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@porcentagemDesconto", p.PorcentagemDesconto.HasValue ? (object)p.PorcentagemDesconto.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@totalDesconto", p.TotalDesconto.HasValue ? (object)p.TotalDesconto.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@fk_funcionarioID", p.fk_funcionarioID);
                    cmd.Parameters.AddWithValue("@fk_clienteID", p.fk_clienteID.HasValue ? (object)p.fk_clienteID.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@pedidoID", p.PedidoID);
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

        public void Delete(int pedidoID)
        {
            try
            {
                string sql = "DELETE FROM Pedido WHERE pedidoID = @pedidoID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@pedidoID", pedidoID);
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

        public List<Pedido> GetAll()
        {
            List<Pedido> pedidos = new List<Pedido>();
            try
            {
                string sql = "SELECT * FROM Pedido ORDER BY pedidoID";

                using (var conexao = Conexao.Conectar())
                using (var comando = new MySqlCommand(sql, conexao))
                using (MySqlDataReader dr = comando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Pedido pedido = new Pedido
                        {
                            PedidoID = Convert.ToInt32(dr["pedidoID"]),
                            DataPedido = DateOnly.FromDateTime(Convert.ToDateTime(dr["dataPedido"])),
                            Preferencial = Convert.ToBoolean(dr["preferencial"]),
                            Status = dr["status"] == DBNull.Value ? null : dr["status"].ToString(),
                            Observacao = dr["observacao"] == DBNull.Value ? null : dr["observacao"].ToString(),
                            Tipo = dr["tipo"] == DBNull.Value ? null : dr["tipo"].ToString(),
                            Total = Convert.ToDecimal(dr["total"]),
                            ValorDesconto = dr["valorDesconto"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["valorDesconto"]),
                            PorcentagemDesconto = dr["porcentagemDesconto"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["porcentagemDesconto"]),
                            TotalDesconto = dr["totalDesconto"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["totalDesconto"]),
                            fk_funcionarioID = Convert.ToInt32(dr["fk_funcionarioID"]),
                            fk_clienteID = dr["fk_clienteID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["fk_clienteID"])
                        };

                        pedidos.Add(pedido);
                    }
                }
                return pedidos;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar os pedidos: {ex.Message}");
            }
        }

        public Pedido BuscarID(int id_pedido)
        {
            try
            {
                string sql = "SELECT * FROM Pedido WHERE pedidoID = @pedidoID";

                using (var conexao = Conexao.Conectar())
                using (var comando = new MySqlCommand(sql, conexao))
                {
                    comando.Parameters.AddWithValue("@pedidoID", id_pedido);

                    using (MySqlDataReader dr = comando.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Pedido pedido = new Pedido();
                            pedido.PedidoID = Convert.ToInt32(dr["pedidoID"]);
                            pedido.DataPedido = DateOnly.FromDateTime(Convert.ToDateTime(dr["dataPedido"]));
                            pedido.Preferencial = Convert.ToBoolean(dr["preferencial"]);
                            pedido.Status = dr["status"] == DBNull.Value ? null : dr["status"].ToString();
                            pedido.Observacao = dr["observacao"] == DBNull.Value ? null : dr["observacao"].ToString();
                            pedido.Tipo = dr["tipo"] == DBNull.Value ? null : dr["tipo"].ToString();
                            pedido.Total = Convert.ToDecimal(dr["total"]);
                            pedido.ValorDesconto = dr["valorDesconto"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["valorDesconto"]);
                            pedido.PorcentagemDesconto = dr["porcentagemDesconto"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["porcentagemDesconto"]);
                            pedido.TotalDesconto = dr["totalDesconto"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["totalDesconto"]);
                            pedido.fk_funcionarioID = Convert.ToInt32(dr["fk_funcionarioID"]);
                            pedido.fk_clienteID = dr["fk_clienteID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["fk_clienteID"]);

                            return pedido;
                        }
                        else
                        {
                            throw new Exception("Nenhum pedido encontrado com esse ID.");
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