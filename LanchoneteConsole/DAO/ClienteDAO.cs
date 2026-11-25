using LanchoneteConsole.Database;
using LanchoneteConsole.Interface;
using LanchoneteConsole.Mapeamento;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanchoneteConsole.DAO
{
    internal class ClienteDAO : IDAO<Cliente>
    {
        public void Insert(Cliente c)
        {
            try
            {
                string sql = @"INSERT INTO Cliente(dataNasc, nome, celular, fk_enderecoID)
                                VALUES (@dataNasc, @nome, @celular, @fk_enderecoID)";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    if (c.DataNasc.HasValue)
                        cmd.Parameters.Add("@dataNasc", MySqlDbType.Date).Value = c.DataNasc.Value.ToDateTime(TimeOnly.MinValue);
                    else
                        cmd.Parameters.Add("@dataNasc", MySqlDbType.Date).Value = DBNull.Value;

                    cmd.Parameters.AddWithValue("@nome", c.Nome);
                    cmd.Parameters.AddWithValue("@celular", c.Celular);
                    cmd.Parameters.AddWithValue("@fk_enderecoID", c.fk_enderecoID.HasValue ? (object)c.fk_enderecoID.Value : DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao inserir cliente: {ex.Message}");
            }
        }

        public void Update(Cliente c)
        {
            try
            {
                string sql = @"UPDATE Cliente SET dataNasc = @dataNasc, nome = @nome, celular = @celular, fk_enderecoID = @fk_enderecoID
                                WHERE clienteID = @clienteID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    if (c.DataNasc.HasValue)
                        cmd.Parameters.Add("@dataNasc", MySqlDbType.Date).Value = c.DataNasc.Value.ToDateTime(TimeOnly.MinValue);
                    else
                        cmd.Parameters.Add("@dataNasc", MySqlDbType.Date).Value = DBNull.Value;

                    cmd.Parameters.AddWithValue("@nome", c.Nome);
                    cmd.Parameters.AddWithValue("@celular", c.Celular);
                    cmd.Parameters.AddWithValue("@fk_enderecoID", c.fk_enderecoID.HasValue ? (object)c.fk_enderecoID.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@clienteID", c.ClienteID);
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

        public void Delete(int clienteID)
        {
            try
            {
                string sql = "DELETE FROM Cliente WHERE clienteID = @clienteID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@clienteID", clienteID);
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

        public List<Cliente> GetAll()
        {
            List<Cliente> clientes = new List<Cliente>();
            try
            {
                string sql = "SELECT * FROM Cliente ORDER BY nome";

                using (var conexao = Conexao.Conectar())
                using (var comando = new MySqlCommand(sql, conexao))
                using (MySqlDataReader dr = comando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Cliente cliente = new Cliente
                        {
                            ClienteID = Convert.ToInt32(dr["clienteID"]),
                            DataNasc = dr["dataNasc"] == DBNull.Value ? (DateOnly?)null : DateOnly.FromDateTime(Convert.ToDateTime(dr["dataNasc"])),
                            Nome = dr["nome"].ToString(),
                            Celular = dr["celular"].ToString(),
                            fk_enderecoID = dr["fk_enderecoID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["fk_enderecoID"])
                        };

                        clientes.Add(cliente);
                    }
                }
                return clientes;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar os clientes: {ex.Message}");
            }
        }

        public Cliente BuscarID(int id_cliente)
        {
            try
            {
                string sql = "SELECT * FROM Cliente WHERE clienteID = @clienteID";

                using (var conexao = Conexao.Conectar())
                using (var comando = new MySqlCommand(sql, conexao))
                {
                    comando.Parameters.AddWithValue("@clienteID", id_cliente);

                    using (MySqlDataReader dr = comando.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Cliente cliente = new Cliente();
                            cliente.ClienteID = Convert.ToInt32(dr["clienteID"]);
                            cliente.DataNasc = dr["dataNasc"] == DBNull.Value ? (DateOnly?)null : DateOnly.FromDateTime(Convert.ToDateTime(dr["dataNasc"]));
                            cliente.Nome = dr["nome"].ToString();
                            cliente.Celular = dr["celular"].ToString();
                            cliente.fk_enderecoID = dr["fk_enderecoID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["fk_enderecoID"]);

                            return cliente;
                        }
                        else
                        {
                            throw new Exception("Nenhum cliente encontrado com esse ID.");
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