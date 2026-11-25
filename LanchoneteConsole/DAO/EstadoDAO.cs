using LanchoneteConsole.Database;
using LanchoneteConsole.Interface;
using LanchoneteConsole.Mapeamento;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanchoneteConsole.DAO
{
    internal class EstadoDAO : IDAO<Estado>
    {
        public void Insert(Estado e)
        {
            try
            {
                string sql = @"INSERT INTO Estado(nome, fk_paisID)
                                VALUES (@nome, @fk_paisID)";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@nome", e.Nome);
                    cmd.Parameters.AddWithValue("@fk_paisID", e.fk_paisID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao inserir estado: {ex.Message}");
            }
        }

        public void Update(Estado e)
        {
            try
            {
                string sql = @"UPDATE Estado SET nome = @nome, fk_paisID = @fk_paisID
                                WHERE estadoID = @estadoID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@nome", e.Nome);
                    cmd.Parameters.AddWithValue("@fk_paisID", e.fk_paisID);
                    cmd.Parameters.AddWithValue("@estadoID", e.EstadoID);
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

        public void Delete(int estadoID)
        {
            try
            {
                string sql = "DELETE FROM Estado WHERE estadoID = @estadoID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@estadoID", estadoID);
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

        public List<Estado> GetAll()
        {
            List<Estado> estados = new List<Estado>();
            try
            {
                string sql = "SELECT * FROM Estado ORDER BY nome";

                using (var conexao = Conexao.Conectar())
                using (var comando = new MySqlCommand(sql, conexao))
                using (MySqlDataReader dr = comando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Estado estado = new Estado
                        {
                            EstadoID = Convert.ToInt32(dr["estadoID"]),
                            Nome = dr["nome"].ToString(),
                            fk_paisID = Convert.ToInt32(dr["fk_paisID"])
                        };

                        estados.Add(estado);
                    }
                }
                return estados;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar os estados: {ex.Message}");
            }
        }

        public Estado BuscarID(int id_estado)
        {
            try
            {
                string sql = "SELECT * FROM Estado WHERE estadoID = @estadoID";

                using (var conexao = Conexao.Conectar())
                using (var comando = new MySqlCommand(sql, conexao))
                {
                    comando.Parameters.AddWithValue("@estadoID", id_estado);

                    using (MySqlDataReader dr = comando.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Estado estado = new Estado();
                            estado.EstadoID = Convert.ToInt32(dr["estadoID"]);
                            estado.Nome = dr["nome"].ToString();
                            estado.fk_paisID = Convert.ToInt32(dr["fk_paisID"]);

                            return estado;
                        }
                        else
                        {
                            throw new Exception("Nenhum estado encontrado com esse ID.");
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