using LanchoneteConsole.Database;
using LanchoneteConsole.Interface;
using LanchoneteConsole.Mapeamento;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanchoneteConsole.DAO
{
    internal class PaisDAO : IDAO<Pais>
    {
        public void Insert(Pais p)
        {
            try
            {
                string sql = @"INSERT INTO Pais(nome)
                                VALUES (@nome)";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@nome", p.Nome);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao inserir país: {ex.Message}");
            }
        }

        public void Update(Pais p)
        {
            try
            {
                string sql = @"UPDATE Pais SET nome = @nome
                                WHERE paisID = @paisID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@nome", p.Nome);
                    cmd.Parameters.AddWithValue("@paisID", p.PaisID);
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

        public void Delete(int paisID)
        {
            try
            {
                string sql = "DELETE FROM Pais WHERE paisID = @paisID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@paisID", paisID);
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

        public List<Pais> GetAll()
        {
            List<Pais> paises = new List<Pais>();
            try
            {
                string sql = "SELECT * FROM Pais ORDER BY nome";

                using (var conexao = Conexao.Conectar())
                using (var comando = new MySqlCommand(sql, conexao))
                using (MySqlDataReader dr = comando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Pais pais = new Pais
                        {
                            PaisID = Convert.ToInt32(dr["paisID"]),
                            Nome = dr["nome"].ToString()
                        };

                        paises.Add(pais);
                    }
                }
                return paises;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar os países: {ex.Message}");
            }
        }

        public Pais BuscarID(int id_pais)
        {
            try
            {
                string sql = "SELECT * FROM Pais WHERE paisID = @paisID";

                using (var conexao = Conexao.Conectar())
                using (var comando = new MySqlCommand(sql, conexao))
                {
                    comando.Parameters.AddWithValue("@paisID", id_pais);

                    using (MySqlDataReader dr = comando.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Pais pais = new Pais();
                            pais.PaisID = Convert.ToInt32(dr["paisID"]);
                            pais.Nome = dr["nome"].ToString();

                            return pais;
                        }
                        else
                        {
                            throw new Exception("Nenhum país encontrado com esse ID.");
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