using LanchoneteConsole.Database;
using LanchoneteConsole.Interface;
using LanchoneteConsole.Mapeamento;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanchoneteConsole.DAO
{
    internal class ComboProdutoDAO : IDAO<ComboProduto>
    {
        public void Insert(ComboProduto cp)
        {
            try
            {
                string sql = @"INSERT INTO ComboProduto(fk_comboID, fk_produtoID)
                                VALUES (@fk_comboID, @fk_produtoID)";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@fk_comboID", cp.fk_comboID);
                    cmd.Parameters.AddWithValue("@fk_produtoID", cp.fk_produtoID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao inserir vínculo de combo e produto: {ex.Message}");
            }
        }

        public void Update(ComboProduto cp)
        {
            try
            {
                string sql = @"UPDATE ComboProduto SET fk_comboID = @fk_comboID, fk_produtoID = @fk_produtoID
                                WHERE comboProdutoID = @comboProdutoID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@fk_comboID", cp.fk_comboID);
                    cmd.Parameters.AddWithValue("@fk_produtoID", cp.fk_produtoID);
                    cmd.Parameters.AddWithValue("@comboProdutoID", cp.ComboProdutoID);
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

        public void Delete(int comboProdutoID)
        {
            try
            {
                string sql = "DELETE FROM ComboProduto WHERE comboProdutoID = @comboProdutoID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@comboProdutoID", comboProdutoID);
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

        public List<ComboProduto> GetAll()
        {
            List<ComboProduto> lista = new List<ComboProduto>();
            try
            {
                string sql = "SELECT * FROM ComboProduto ORDER BY comboProdutoID";

                using (var conexao = Conexao.Conectar())
                using (var comando = new MySqlCommand(sql, conexao))
                using (MySqlDataReader dr = comando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ComboProduto cp = new ComboProduto
                        {
                            ComboProdutoID = Convert.ToInt32(dr["comboProdutoID"]),
                            fk_comboID = Convert.ToInt32(dr["fk_comboID"]),
                            fk_produtoID = Convert.ToInt32(dr["fk_produtoID"])
                        };

                        lista.Add(cp);
                    }
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar os itens de combo: {ex.Message}");
            }
        }

        public ComboProduto BuscarID(int id_comboProduto)
        {
            try
            {
                string sql = "SELECT * FROM ComboProduto WHERE comboProdutoID = @comboProdutoID";

                using (var conexao = Conexao.Conectar())
                using (var comando = new MySqlCommand(sql, conexao))
                {
                    comando.Parameters.AddWithValue("@comboProdutoID", id_comboProduto);

                    using (MySqlDataReader dr = comando.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            ComboProduto cp = new ComboProduto();
                            cp.ComboProdutoID = Convert.ToInt32(dr["comboProdutoID"]);
                            cp.fk_comboID = Convert.ToInt32(dr["fk_comboID"]);
                            cp.fk_produtoID = Convert.ToInt32(dr["fk_produtoID"]);

                            return cp;
                        }
                        else
                        {
                            throw new Exception("Nenhum registro de combo/produto encontrado com esse ID.");
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