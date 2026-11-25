using LanchoneteConsole.Database;
using LanchoneteConsole.Interface;
using LanchoneteConsole.Mapeamento;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanchoneteConsole.DAO
{
    internal class PagamentoPedidoDAO : IDAO<PagamentoPedido>
    {
        public void Insert(PagamentoPedido pp)
        {
            try
            {
                string sql = @"INSERT INTO PagamentoPedido(fk_pagamentoID, fk_pedidoID)
                                VALUES (@fk_pagamentoID, @fk_pedidoID)";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@fk_pagamentoID", pp.fk_pagamentoID);
                    cmd.Parameters.AddWithValue("@fk_pedidoID", pp.fk_pedidoID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao inserir vínculo de pagamento e pedido: {ex.Message}");
            }
        }

        public void Update(PagamentoPedido pp)
        {
            try
            {
                string sql = @"UPDATE PagamentoPedido SET fk_pagamentoID = @fk_pagamentoID, fk_pedidoID = @fk_pedidoID
                                WHERE pagamentoPedidoID = @pagamentoPedidoID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@fk_pagamentoID", pp.fk_pagamentoID);
                    cmd.Parameters.AddWithValue("@fk_pedidoID", pp.fk_pedidoID);
                    cmd.Parameters.AddWithValue("@pagamentoPedidoID", pp.PagamentoPedidoID);
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

        public void Delete(int pagamentoPedidoID)
        {
            try
            {
                string sql = "DELETE FROM PagamentoPedido WHERE pagamentoPedidoID = @pagamentoPedidoID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@pagamentoPedidoID", pagamentoPedidoID);
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

        public List<PagamentoPedido> GetAll()
        {
            List<PagamentoPedido> lista = new List<PagamentoPedido>();
            try
            {
                string sql = "SELECT * FROM PagamentoPedido ORDER BY pagamentoPedidoID";

                using (var conexao = Conexao.Conectar())
                using (var comando = new MySqlCommand(sql, conexao))
                using (MySqlDataReader dr = comando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        PagamentoPedido pp = new PagamentoPedido
                        {
                            PagamentoPedidoID = Convert.ToInt32(dr["pagamentoPedidoID"]),
                            fk_pagamentoID = Convert.ToInt32(dr["fk_pagamentoID"]),
                            fk_pedidoID = Convert.ToInt32(dr["fk_pedidoID"])
                        };

                        lista.Add(pp);
                    }
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar os vínculos de pagamento e pedido: {ex.Message}");
            }
        }

        public PagamentoPedido BuscarID(int id_pagamentoPedido)
        {
            try
            {
                string sql = "SELECT * FROM PagamentoPedido WHERE pagamentoPedidoID = @pagamentoPedidoID";

                using (var conexao = Conexao.Conectar())
                using (var comando = new MySqlCommand(sql, conexao))
                {
                    comando.Parameters.AddWithValue("@pagamentoPedidoID", id_pagamentoPedido);

                    using (MySqlDataReader dr = comando.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            PagamentoPedido pp = new PagamentoPedido();
                            pp.PagamentoPedidoID = Convert.ToInt32(dr["pagamentoPedidoID"]);
                            pp.fk_pagamentoID = Convert.ToInt32(dr["fk_pagamentoID"]);
                            pp.fk_pedidoID = Convert.ToInt32(dr["fk_pedidoID"]);

                            return pp;
                        }
                        else
                        {
                            throw new Exception("Nenhum registro de pagamento/pedido encontrado com esse ID.");
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