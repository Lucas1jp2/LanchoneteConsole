using LanchoneteConsole.Database;
using LanchoneteConsole.Interface;
using LanchoneteConsole.Mapeamento;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanchoneteConsole.DAO
{
    internal class ItemPedidoDAO : IDAO<ItemPedido>
    {
        public void Insert(ItemPedido ip)
        {
            try
            {
                string sql = @"INSERT INTO ItemPedido(quantidade, valorUnitario, valorDesconto, porcentagemDesconto, totalDesconto, totalItem, observacao, fk_produtoID, fk_pedidoID)
                                VALUES (@quantidade, @valorUnitario, @valorDesconto, @porcentagemDesconto, @totalDesconto, @totalItem, @observacao, @fk_produtoID, @fk_pedidoID)";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@quantidade", ip.Quantidade);
                    cmd.Parameters.AddWithValue("@valorUnitario", ip.ValorUnitario);
                    cmd.Parameters.AddWithValue("@valorDesconto", ip.ValorDesconto.HasValue ? (object)ip.ValorDesconto.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@porcentagemDesconto", ip.PorcentagemDesconto.HasValue ? (object)ip.PorcentagemDesconto.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@totalDesconto", ip.TotalDesconto.HasValue ? (object)ip.TotalDesconto.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@totalItem", ip.TotalItem);
                    cmd.Parameters.AddWithValue("@observacao", string.IsNullOrEmpty(ip.Observacao) ? DBNull.Value : (object)ip.Observacao);
                    cmd.Parameters.AddWithValue("@fk_produtoID", ip.fk_produtoID);
                    cmd.Parameters.AddWithValue("@fk_pedidoID", ip.fk_pedidoID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao inserir item do pedido: {ex.Message}");
            }
        }

        public void Update(ItemPedido ip)
        {
            try
            {
                string sql = @"UPDATE ItemPedido SET quantidade = @quantidade, valorUnitario = @valorUnitario, valorDesconto = @valorDesconto, porcentagemDesconto = @porcentagemDesconto, totalDesconto = @totalDesconto, totalItem = @totalItem, observacao = @observacao, fk_produtoID = @fk_produtoID, fk_pedidoID = @fk_pedidoID
                                WHERE itemPedidoID = @itemPedidoID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@quantidade", ip.Quantidade);
                    cmd.Parameters.AddWithValue("@valorUnitario", ip.ValorUnitario);
                    cmd.Parameters.AddWithValue("@valorDesconto", ip.ValorDesconto.HasValue ? (object)ip.ValorDesconto.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@porcentagemDesconto", ip.PorcentagemDesconto.HasValue ? (object)ip.PorcentagemDesconto.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@totalDesconto", ip.TotalDesconto.HasValue ? (object)ip.TotalDesconto.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@totalItem", ip.TotalItem);
                    cmd.Parameters.AddWithValue("@observacao", string.IsNullOrEmpty(ip.Observacao) ? DBNull.Value : (object)ip.Observacao);
                    cmd.Parameters.AddWithValue("@fk_produtoID", ip.fk_produtoID);
                    cmd.Parameters.AddWithValue("@fk_pedidoID", ip.fk_pedidoID);
                    cmd.Parameters.AddWithValue("@itemPedidoID", ip.ItemPedidoID);
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

        public void Delete(int itemPedidoID)
        {
            try
            {
                string sql = "DELETE FROM ItemPedido WHERE itemPedidoID = @itemPedidoID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@itemPedidoID", itemPedidoID);
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

        public List<ItemPedido> GetAll()
        {
            List<ItemPedido> itens = new List<ItemPedido>();
            try
            {
                string sql = "SELECT * FROM ItemPedido ORDER BY itemPedidoID";

                using (var conexao = Conexao.Conectar())
                using (var comando = new MySqlCommand(sql, conexao))
                using (MySqlDataReader dr = comando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ItemPedido item = new ItemPedido
                        {
                            ItemPedidoID = Convert.ToInt32(dr["itemPedidoID"]),
                            Quantidade = Convert.ToInt32(dr["quantidade"]),
                            ValorUnitario = Convert.ToDecimal(dr["valorUnitario"]),
                            ValorDesconto = dr["valorDesconto"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["valorDesconto"]),
                            PorcentagemDesconto = dr["porcentagemDesconto"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["porcentagemDesconto"]),
                            TotalDesconto = dr["totalDesconto"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["totalDesconto"]),
                            TotalItem = Convert.ToDecimal(dr["totalItem"]),
                            Observacao = dr["observacao"] == DBNull.Value ? null : dr["observacao"].ToString(),
                            fk_produtoID = Convert.ToInt32(dr["fk_produtoID"]),
                            fk_pedidoID = Convert.ToInt32(dr["fk_pedidoID"])
                        };

                        itens.Add(item);
                    }
                }
                return itens;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar os itens do pedido: {ex.Message}");
            }
        }

        public ItemPedido BuscarID(int id_itemPedido)
        {
            try
            {
                string sql = "SELECT * FROM ItemPedido WHERE itemPedidoID = @itemPedidoID";

                using (var conexao = Conexao.Conectar())
                using (var comando = new MySqlCommand(sql, conexao))
                {
                    comando.Parameters.AddWithValue("@itemPedidoID", id_itemPedido);

                    using (MySqlDataReader dr = comando.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            ItemPedido item = new ItemPedido();
                            item.ItemPedidoID = Convert.ToInt32(dr["itemPedidoID"]);
                            item.Quantidade = Convert.ToInt32(dr["quantidade"]);
                            item.ValorUnitario = Convert.ToDecimal(dr["valorUnitario"]);
                            item.ValorDesconto = dr["valorDesconto"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["valorDesconto"]);
                            item.PorcentagemDesconto = dr["porcentagemDesconto"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["porcentagemDesconto"]);
                            item.TotalDesconto = dr["totalDesconto"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["totalDesconto"]);
                            item.TotalItem = Convert.ToDecimal(dr["totalItem"]);
                            item.Observacao = dr["observacao"] == DBNull.Value ? null : dr["observacao"].ToString();
                            item.fk_produtoID = Convert.ToInt32(dr["fk_produtoID"]);
                            item.fk_pedidoID = Convert.ToInt32(dr["fk_pedidoID"]);

                            return item;
                        }
                        else
                        {
                            throw new Exception("Nenhum item de pedido encontrado com esse ID.");
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