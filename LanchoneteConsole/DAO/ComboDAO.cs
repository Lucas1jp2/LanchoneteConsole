using LanchoneteConsole.Database;
using LanchoneteConsole.Interface;
using LanchoneteConsole.Mapeamento;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanchoneteConsole.DAO
{
    internal class ComboDAO : IDAO<Combo>
    {
        public void Insert(Combo c)
        {
            try
            {
                List<Combo> combosExistentes = GetAll();
                foreach (Combo item in combosExistentes)
                {
                    if (item.Nome.Trim().ToUpper() == c.Nome.Trim().ToUpper())
                    {
                        throw new Exception("Já existe um combo cadastrado com esse nome.");
                    }
                }

                string sql = @"INSERT INTO Combo(nome, valor, dataInicio, dataFim)
                                VALUES (@nome, @valor, @dataInicio, @dataFim)";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@nome", c.Nome.Trim());
                    cmd.Parameters.AddWithValue("@valor", c.Valor);
                    cmd.Parameters.Add("@dataInicio", MySqlDbType.Date).Value = c.DataInicio.ToDateTime(TimeOnly.MinValue);
                    cmd.Parameters.Add("@dataFim", MySqlDbType.Date).Value = c.DataFim.ToDateTime(TimeOnly.MinValue);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao inserir combo: {ex.Message}");
            }
        }

        public void Update(Combo c)
        {
            try
            {
                List<Combo> combosExistentes = GetAll();
                foreach (Combo item in combosExistentes)
                {
                    if (item.ComboID != c.ComboID &&
                        item.Nome.Trim().ToUpper() == c.Nome.Trim().ToUpper())
                    {
                        throw new Exception("Já existe outro combo cadastrado com esse nome.");
                    }
                }

                string sql = @"UPDATE Combo SET nome = @nome, valor = @valor, dataInicio = @dataInicio, dataFim = @dataFim
                                WHERE comboID = @comboID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@nome", c.Nome.Trim());
                    cmd.Parameters.AddWithValue("@valor", c.Valor);
                    cmd.Parameters.Add("@dataInicio", MySqlDbType.Date).Value = c.DataInicio.ToDateTime(TimeOnly.MinValue);
                    cmd.Parameters.Add("@dataFim", MySqlDbType.Date).Value = c.DataFim.ToDateTime(TimeOnly.MinValue);
                    cmd.Parameters.AddWithValue("@comboID", c.ComboID);
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

        public void Delete(int comboID)
        {
            try
            {
                string sql = "DELETE FROM Combo WHERE comboID = @comboID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@comboID", comboID);
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

        public List<Combo> GetAll()
        {
            List<Combo> combos = new List<Combo>();
            try
            {
                string sql = "SELECT * FROM Combo ORDER BY nome";

                using (var conexao = Conexao.Conectar())
                using (var comando = new MySqlCommand(sql, conexao))
                using (MySqlDataReader dr = comando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Combo combo = new Combo
                        {
                            ComboID = Convert.ToInt32(dr["comboID"]),
                            Nome = dr["nome"].ToString(),
                            Valor = Convert.ToDecimal(dr["valor"]),
                            DataInicio = DateOnly.FromDateTime(Convert.ToDateTime(dr["dataInicio"])),
                            DataFim = DateOnly.FromDateTime(Convert.ToDateTime(dr["dataFim"]))
                        };

                        combos.Add(combo);
                    }
                }
                return combos;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar os combos: {ex.Message}");
            }
        }

        public Combo BuscarID(int id_combo)
        {
            try
            {
                string sql = "SELECT * FROM Combo WHERE comboID = @comboID";

                using (var conexao = Conexao.Conectar())
                using (var comando = new MySqlCommand(sql, conexao))
                {
                    comando.Parameters.AddWithValue("@comboID", id_combo);

                    using (MySqlDataReader dr = comando.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Combo combo = new Combo();
                            combo.ComboID = Convert.ToInt32(dr["comboID"]);
                            combo.Nome = dr["nome"].ToString();
                            combo.Valor = Convert.ToDecimal(dr["valor"]);
                            combo.DataInicio = DateOnly.FromDateTime(Convert.ToDateTime(dr["dataInicio"]));
                            combo.DataFim = DateOnly.FromDateTime(Convert.ToDateTime(dr["dataFim"]));

                            return combo;
                        }
                        else
                        {
                            throw new Exception("Nenhum combo encontrado com esse ID.");
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