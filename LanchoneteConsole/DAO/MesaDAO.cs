using LanchoneteConsole.Database;
using LanchoneteConsole.Interface;
using LanchoneteConsole.Mapeamento;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanchoneteConsole.DAO
{
    internal class MesaDAO : IDAO<Mesa>
    {
        public void Insert(Mesa m)
        {
            try
            {
                string sql = @"INSERT INTO Mesa(localizacao)
                                VALUES (@localizacao)";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@localizacao", string.IsNullOrEmpty(m.Localizacao) ? DBNull.Value : (object)m.Localizacao);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao inserir mesa: {ex.Message}");
            }
        }

        public void Update(Mesa m)
        {
            try
            {
                string sql = @"UPDATE Mesa SET localizacao = @localizacao
                                WHERE mesaID = @mesaID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@localizacao", string.IsNullOrEmpty(m.Localizacao) ? DBNull.Value : (object)m.Localizacao);
                    cmd.Parameters.AddWithValue("@mesaID", m.MesaID);
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

        public void Delete(int mesaID)
        {
            try
            {
                string sql = "DELETE FROM Mesa WHERE mesaID = @mesaID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@mesaID", mesaID);
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

        public List<Mesa> GetAll()
        {
            List<Mesa> mesas = new List<Mesa>();
            try
            {
                string sql = "SELECT * FROM Mesa ORDER BY mesaID";

                using (var conexao = Conexao.Conectar())
                using (var comando = new MySqlCommand(sql, conexao))
                using (MySqlDataReader dr = comando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Mesa mesa = new Mesa
                        {
                            MesaID = Convert.ToInt32(dr["mesaID"]),
                            Localizacao = dr["localizacao"] == DBNull.Value ? null : dr["localizacao"].ToString()
                        };

                        mesas.Add(mesa);
                    }
                }
                return mesas;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar as mesas: {ex.Message}");
            }
        }

        public Mesa BuscarID(int id_mesa)
        {
            try
            {
                string sql = "SELECT * FROM Mesa WHERE mesaID = @mesaID";

                using (var conexao = Conexao.Conectar())
                using (var comando = new MySqlCommand(sql, conexao))
                {
                    comando.Parameters.AddWithValue("@mesaID", id_mesa);

                    using (MySqlDataReader dr = comando.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Mesa mesa = new Mesa();
                            mesa.MesaID = Convert.ToInt32(dr["mesaID"]);
                            mesa.Localizacao = dr["localizacao"] == DBNull.Value ? null : dr["localizacao"].ToString();

                            return mesa;
                        }
                        else
                        {
                            throw new Exception("Nenhuma mesa encontrada com esse ID.");
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