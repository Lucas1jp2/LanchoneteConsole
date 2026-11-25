using LanchoneteConsole.Database;
using LanchoneteConsole.Interface;
using LanchoneteConsole.Mapeamento;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanchoneteConsole.DAO
{
    internal class ProdutoDAO : IDAO<Produto>
    {
        public void Insert(Produto p)
        {
            try
            {
                List<Produto> produtosExistentes = GetAll();
                foreach (Produto item in produtosExistentes)
                {
                    if (item.Nome.Trim().ToUpper() == p.Nome.Trim().ToUpper())
                    {
                        throw new Exception("Já existe um produto cadastrado com esse nome.");
                    }
                }

                string sql = @"INSERT INTO Produto(nome, tipo, valor)
                                VALUES (@nome, @tipo, @valor)";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@nome", p.Nome.Trim());
                    cmd.Parameters.AddWithValue("@tipo", p.Tipo.Trim());
                    cmd.Parameters.AddWithValue("@valor", p.Valor);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao inserir produto: {ex.Message}");
            }
        }

        public void Update(Produto p)
        {
            try
            {
                List<Produto> produtosExistentes = GetAll();
                foreach (Produto item in produtosExistentes)
                {
                    if (item.ProdutoID != p.ProdutoID &&
                        item.Nome.Trim().ToUpper() == p.Nome.Trim().ToUpper())
                    {
                        throw new Exception("Já existe outro produto cadastrado com esse nome.");
                    }
                }

                string sql = @"UPDATE Produto SET nome = @nome, tipo = @tipo, valor = @valor
                                WHERE produtoID = @produtoID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@nome", p.Nome.Trim());
                    cmd.Parameters.AddWithValue("@tipo", p.Tipo.Trim());
                    cmd.Parameters.AddWithValue("@valor", p.Valor);
                    cmd.Parameters.AddWithValue("@produtoID", p.ProdutoID);
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

        public void Delete(int produtoID)
        {
            try
            {
                string sql = "DELETE FROM Produto WHERE produtoID = @produtoID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@produtoID", produtoID);
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

        public List<Produto> GetAll()
        {
            List<Produto> produtos = new List<Produto>();
            try
            {
                string sql = "SELECT * FROM Produto ORDER BY nome";

                using (var conexao = Conexao.Conectar())
                using (var comando = new MySqlCommand(sql, conexao))
                using (MySqlDataReader dr = comando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Produto produto = new Produto
                        {
                            ProdutoID = Convert.ToInt32(dr["produtoID"]),
                            Nome = dr["nome"].ToString(),
                            Tipo = dr["tipo"].ToString(),
                            Valor = Convert.ToDecimal(dr["valor"])
                        };

                        produtos.Add(produto);
                    }
                }
                return produtos;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar os produtos: {ex.Message}");
            }
        }

        public Produto BuscarID(int id_produto)
        {
            try
            {
                string sql = "SELECT * FROM Produto WHERE produtoID = @produtoID";

                using (var conexao = Conexao.Conectar())
                using (var comando = new MySqlCommand(sql, conexao))
                {
                    comando.Parameters.AddWithValue("@produtoID", id_produto);

                    using (MySqlDataReader dr = comando.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Produto produto = new Produto();
                            produto.ProdutoID = Convert.ToInt32(dr["produtoID"]);
                            produto.Nome = dr["nome"].ToString();
                            produto.Tipo = dr["tipo"].ToString();
                            produto.Valor = Convert.ToDecimal(dr["valor"]);

                            return produto;
                        }
                        else
                        {
                            throw new Exception("Nenhum produto encontrado com esse ID.");
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