using LanchoneteConsole.Database;
using LanchoneteConsole.Interface;
using LanchoneteConsole.Mapeamento;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanchoneteConsole.DAO
{
    internal class CidadeDAO : IDAO<Cidade>
    {
        public void Insert(Cidade c)
        {
            try
            {
                string sql = @"INSERT INTO Cidade(nome, fk_estadoID)
                                VALUES (@nome, @fk_estadoID)";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@nome", c.Nome);
                    cmd.Parameters.AddWithValue("@fk_estadoID", c.fk_estadoID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao inserir cidade: {ex.Message}");
            }
        }

        public void Update(Cidade c)
        {
            try
            {
                string sql = @"UPDATE Cidade SET nome = @nome, fk_estadoID = @fk_estadoID
                                WHERE cidadeID = @cidadeID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@nome", c.Nome);
                    cmd.Parameters.AddWithValue("@fk_estadoID", c.fk_estadoID);
                    cmd.Parameters.AddWithValue("@cidadeID", c.CidadeID);
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

        public void Delete(int cidadeID)
        {
            try
            {
                string sql = "DELETE FROM Cidade WHERE cidadeID = @cidadeID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@cidadeID", cidadeID);
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

        public List<Cidade> GetAll()
        {
            List<Cidade> cidades = new List<Cidade>();
            try
            {
                string sql = "SELECT * FROM Cidade ORDER BY nome";

                using (var conexao = Conexao.Conectar())
                using (var comando = new MySqlCommand(sql, conexao))
                using (MySqlDataReader dr = comando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Cidade cidade = new Cidade
                        {
                            CidadeID = Convert.ToInt32(dr["cidadeID"]),
                            Nome = dr["nome"].ToString(),
                            fk_estadoID = Convert.ToInt32(dr["fk_estadoID"])
                        };

                        cidades.Add(cidade);
                    }
                }
                return cidades;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar as cidades: {ex.Message}");
            }
        }

        public Cidade BuscarID(int id_cidade)
        {
            try
            {
                string sql = "SELECT * FROM Cidade WHERE cidadeID = @cidadeID";

                using (var conexao = Conexao.Conectar())
                using (var comando = new MySqlCommand(sql, conexao))
                {
                    comando.Parameters.AddWithValue("@cidadeID", id_cidade);

                    using (MySqlDataReader dr = comando.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Cidade cidade = new Cidade();
                            cidade.CidadeID = Convert.ToInt32(dr["cidadeID"]);
                            cidade.Nome = dr["nome"].ToString();
                            cidade.fk_estadoID = Convert.ToInt32(dr["fk_estadoID"]);

                            return cidade;
                        }
                        else
                        {
                            throw new Exception("Nenhuma cidade encontrada com esse ID.");
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