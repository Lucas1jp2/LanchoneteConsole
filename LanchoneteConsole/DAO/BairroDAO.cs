using LanchoneteConsole.Database;
using LanchoneteConsole.Interface;
using LanchoneteConsole.Mapeamento;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanchoneteConsole.DAO
{
    internal class BairroDAO : IDAO<Bairro>
    {
        public void Insert(Bairro b)
        {
            try
            {
                string sql = @"INSERT INTO Bairro(nome, taxa, fk_cidadeID)
                                VALUES (@nome, @taxa, @fk_cidadeID)";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@nome", b.Nome);
                    cmd.Parameters.AddWithValue("@taxa", b.Taxa);
                    cmd.Parameters.AddWithValue("@fk_cidadeID", b.fk_cidadeID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao inserir bairro: {ex.Message}");
            }
        }

        public void Update(Bairro b)
        {
            try
            {
                string sql = @"UPDATE Bairro SET nome = @nome, taxa = @taxa, fk_cidadeID = @fk_cidadeID
                                WHERE bairroID = @bairroID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@nome", b.Nome);
                    cmd.Parameters.AddWithValue("@taxa", b.Taxa);
                    cmd.Parameters.AddWithValue("@fk_cidadeID", b.fk_cidadeID);
                    cmd.Parameters.AddWithValue("@bairroID", b.BairroID);
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

        public void Delete(int bairroID)
        {
            try
            {
                string sql = "DELETE FROM Bairro WHERE bairroID = @bairroID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@bairroID", bairroID);
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

        public List<Bairro> GetAll()
        {
            List<Bairro> bairros = new List<Bairro>();
            try
            {
                string sql = "SELECT * FROM Bairro ORDER BY nome";

                using (var conexao = Conexao.Conectar())
                using (var comando = new MySqlCommand(sql, conexao))
                using (MySqlDataReader dr = comando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Bairro bairro = new Bairro
                        {
                            BairroID = Convert.ToInt32(dr["bairroID"]),
                            Nome = dr["nome"].ToString(),
                            Taxa = Convert.ToDecimal(dr["taxa"]),
                            fk_cidadeID = Convert.ToInt32(dr["fk_cidadeID"])
                        };

                        bairros.Add(bairro);
                    }
                }
                return bairros;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar os bairros: {ex.Message}");
            }
        }

        public Bairro BuscarID(int id_bairro)
        {
            try
            {
                string sql = "SELECT * FROM Bairro WHERE bairroID = @bairroID";

                using (var conexao = Conexao.Conectar())
                using (var comando = new MySqlCommand(sql, conexao))
                {
                    comando.Parameters.AddWithValue("@bairroID", id_bairro);

                    using (MySqlDataReader dr = comando.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Bairro bairro = new Bairro();
                            bairro.BairroID = Convert.ToInt32(dr["bairroID"]);
                            bairro.Nome = dr["nome"].ToString();
                            bairro.Taxa = Convert.ToDecimal(dr["taxa"]);
                            bairro.fk_cidadeID = Convert.ToInt32(dr["fk_cidadeID"]);

                            return bairro;
                        }
                        else
                        {
                            throw new Exception("Nenhum bairro encontrado com esse ID.");
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