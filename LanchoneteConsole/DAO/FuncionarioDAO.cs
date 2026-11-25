using LanchoneteConsole.Database;
using LanchoneteConsole.Interface;
using LanchoneteConsole.Mapeamento;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanchoneteConsole.DAO
{
    internal class FuncionarioDAO : IDAO<Funcionario>
    {
        public void Insert(Funcionario f)
        {
            try
            {
                string sql = @"INSERT INTO Funcionario(dataNasc, cargo, nome, email, celular, cpf, salario, fk_enderecoID)
                                VALUES (@dataNasc, @cargo, @nome, @email, @celular, @cpf, @salario, @fk_enderecoID)";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    if (f.DataNasc.HasValue)
                        cmd.Parameters.Add("@dataNasc", MySqlDbType.Date).Value = f.DataNasc.Value.ToDateTime(TimeOnly.MinValue);
                    else
                        cmd.Parameters.Add("@dataNasc", MySqlDbType.Date).Value = DBNull.Value;

                    cmd.Parameters.AddWithValue("@cargo", string.IsNullOrEmpty(f.Cargo) ? DBNull.Value : (object)f.Cargo);
                    cmd.Parameters.AddWithValue("@nome", f.Nome);
                    cmd.Parameters.AddWithValue("@email", f.Email);
                    cmd.Parameters.AddWithValue("@celular", f.Celular);
                    cmd.Parameters.AddWithValue("@cpf", f.Cpf);
                    cmd.Parameters.AddWithValue("@salario", f.Salario);
                    cmd.Parameters.AddWithValue("@fk_enderecoID", f.fk_enderecoID.HasValue ? (object)f.fk_enderecoID.Value : DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao inserir funcionário: {ex.Message}");
            }
        }

        public void Update(Funcionario f)
        {
            try
            {
                string sql = @"UPDATE Funcionario SET dataNasc = @dataNasc, cargo = @cargo, nome = @nome, email = @email, celular = @celular, cpf = @cpf, salario = @salario, fk_enderecoID = @fk_enderecoID
                                WHERE funcionarioID = @funcionarioID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    if (f.DataNasc.HasValue)
                        cmd.Parameters.Add("@dataNasc", MySqlDbType.Date).Value = f.DataNasc.Value.ToDateTime(TimeOnly.MinValue);
                    else
                        cmd.Parameters.Add("@dataNasc", MySqlDbType.Date).Value = DBNull.Value;

                    cmd.Parameters.AddWithValue("@cargo", string.IsNullOrEmpty(f.Cargo) ? DBNull.Value : (object)f.Cargo);
                    cmd.Parameters.AddWithValue("@nome", f.Nome);
                    cmd.Parameters.AddWithValue("@email", f.Email);
                    cmd.Parameters.AddWithValue("@celular", f.Celular);
                    cmd.Parameters.AddWithValue("@cpf", f.Cpf);
                    cmd.Parameters.AddWithValue("@salario", f.Salario);
                    cmd.Parameters.AddWithValue("@fk_enderecoID", f.fk_enderecoID.HasValue ? (object)f.fk_enderecoID.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@funcionarioID", f.FuncionarioID);
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

        public void Delete(int funcionarioID)
        {
            try
            {
                string sql = "DELETE FROM Funcionario WHERE funcionarioID = @funcionarioID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@funcionarioID", funcionarioID);
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

        public List<Funcionario> GetAll()
        {
            List<Funcionario> funcionarios = new List<Funcionario>();
            try
            {
                string sql = "SELECT * FROM Funcionario ORDER BY nome";

                using (var conexao = Conexao.Conectar())
                using (var comando = new MySqlCommand(sql, conexao))
                using (MySqlDataReader dr = comando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Funcionario funcionario = new Funcionario
                        {
                            FuncionarioID = Convert.ToInt32(dr["funcionarioID"]),
                            DataNasc = dr["dataNasc"] == DBNull.Value ? (DateOnly?)null : DateOnly.FromDateTime(Convert.ToDateTime(dr["dataNasc"])),
                            Cargo = dr["cargo"] == DBNull.Value ? null : dr["cargo"].ToString(),
                            Nome = dr["nome"].ToString(),
                            Email = dr["email"].ToString(),
                            Celular = dr["celular"].ToString(),
                            Cpf = dr["cpf"].ToString(),
                            Salario = Convert.ToDecimal(dr["salario"]),
                            fk_enderecoID = dr["fk_enderecoID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["fk_enderecoID"])
                        };

                        funcionarios.Add(funcionario);
                    }
                }
                return funcionarios;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar os funcionários: {ex.Message}");
            }
        }

        public Funcionario BuscarID(int id_funcionario)
        {
            try
            {
                string sql = "SELECT * FROM Funcionario WHERE funcionarioID = @funcionarioID";

                using (var conexao = Conexao.Conectar())
                using (var comando = new MySqlCommand(sql, conexao))
                {
                    comando.Parameters.AddWithValue("@funcionarioID", id_funcionario);

                    using (MySqlDataReader dr = comando.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Funcionario funcionario = new Funcionario();
                            funcionario.FuncionarioID = Convert.ToInt32(dr["funcionarioID"]);
                            funcionario.DataNasc = dr["dataNasc"] == DBNull.Value ? (DateOnly?)null : DateOnly.FromDateTime(Convert.ToDateTime(dr["dataNasc"]));
                            funcionario.Cargo = dr["cargo"] == DBNull.Value ? null : dr["cargo"].ToString();
                            funcionario.Nome = dr["nome"].ToString();
                            funcionario.Email = dr["email"].ToString();
                            funcionario.Celular = dr["celular"].ToString();
                            funcionario.Cpf = dr["cpf"].ToString();
                            funcionario.Salario = Convert.ToDecimal(dr["salario"]);
                            funcionario.fk_enderecoID = dr["fk_enderecoID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["fk_enderecoID"]);

                            return funcionario;
                        }
                        else
                        {
                            throw new Exception("Nenhum funcionário encontrado com esse ID.");
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