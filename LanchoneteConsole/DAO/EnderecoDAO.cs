using LanchoneteConsole.Database;
using LanchoneteConsole.Interface;
using LanchoneteConsole.Mapeamento;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanchoneteConsole.DAO
{
    internal class EnderecoDAO : IDAO<Endereco>
    {
        public void Insert(Endereco e)
        {
            try
            {
                string sql = @"INSERT INTO Endereco(numero, observacao, fk_bairroID)
                                VALUES (@numero, @observacao, @fk_bairroID)";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@numero", e.Numero.HasValue ? (object)e.Numero.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@observacao", string.IsNullOrEmpty(e.Observacao) ? DBNull.Value : (object)e.Observacao);
                    cmd.Parameters.AddWithValue("@fk_bairroID", e.fk_bairroID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao inserir endereço: {ex.Message}");
            }
        }

        public void Update(Endereco e)
        {
            try
            {
                string sql = @"UPDATE Endereco SET numero = @numero, observacao = @observacao, fk_bairroID = @fk_bairroID
                                WHERE enderecoID = @enderecoID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@numero", e.Numero.HasValue ? (object)e.Numero.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@observacao", string.IsNullOrEmpty(e.Observacao) ? DBNull.Value : (object)e.Observacao);
                    cmd.Parameters.AddWithValue("@fk_bairroID", e.fk_bairroID);
                    cmd.Parameters.AddWithValue("@enderecoID", e.EnderecoID);
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

        public void Delete(int enderecoID)
        {
            try
            {
                string sql = "DELETE FROM Endereco WHERE enderecoID = @enderecoID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@enderecoID", enderecoID);
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

        public List<Endereco> GetAll()
        {
            List<Endereco> enderecos = new List<Endereco>();
            try
            {
                string sql = "SELECT * FROM Endereco ORDER BY enderecoID";

                using (var conexao = Conexao.Conectar())
                using (var comando = new MySqlCommand(sql, conexao))
                using (MySqlDataReader dr = comando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Endereco endereco = new Endereco
                        {
                            EnderecoID = Convert.ToInt32(dr["enderecoID"]),
                            Numero = dr["numero"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["numero"]),
                            Observacao = dr["observacao"] == DBNull.Value ? null : dr["observacao"].ToString(),
                            fk_bairroID = Convert.ToInt32(dr["fk_bairroID"])
                        };

                        enderecos.Add(endereco);
                    }
                }
                return enderecos;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar os endereços: {ex.Message}");
            }
        }

        public Endereco BuscarID(int id_endereco)
        {
            try
            {
                string sql = "SELECT * FROM Endereco WHERE enderecoID = @enderecoID";

                using (var conexao = Conexao.Conectar())
                using (var comando = new MySqlCommand(sql, conexao))
                {
                    comando.Parameters.AddWithValue("@enderecoID", id_endereco);

                    using (MySqlDataReader dr = comando.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Endereco endereco = new Endereco();
                            endereco.EnderecoID = Convert.ToInt32(dr["enderecoID"]);
                            endereco.Numero = dr["numero"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["numero"]);
                            endereco.Observacao = dr["observacao"] == DBNull.Value ? null : dr["observacao"].ToString();
                            endereco.fk_bairroID = Convert.ToInt32(dr["fk_bairroID"]);

                            return endereco;
                        }
                        else
                        {
                            throw new Exception("Nenhum endereço encontrado com esse ID.");
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