using LanchoneteConsole.Database;
using LanchoneteConsole.Interface;
using LanchoneteConsole.Mapeamento;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanchoneteConsole.DAO
{
    internal class PagamentoDAO : IDAO<Pagamento>
    {
        public void Insert(Pagamento p)
        {
            try
            {
                string sql = @"INSERT INTO Pagamento(dataPagamento, dataVencimento, formaPagamento, qtdParcelas, valorDesconto, porcentagemDesconto, totalDesconto, valorPendente, totalPago, total, status)
                                VALUES (@dataPagamento, @dataVencimento, @formaPagamento, @qtdParcelas, @valorDesconto, @porcentagemDesconto, @totalDesconto, @valorPendente, @totalPago, @total, @status)";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    if (p.DataPagamento.HasValue)
                        cmd.Parameters.Add("@dataPagamento", MySqlDbType.Date).Value = p.DataPagamento.Value.ToDateTime(TimeOnly.MinValue);
                    else
                        cmd.Parameters.Add("@dataPagamento", MySqlDbType.Date).Value = DBNull.Value;

                    cmd.Parameters.Add("@dataVencimento", MySqlDbType.Date).Value = p.DataVencimento.ToDateTime(TimeOnly.MinValue);
                    cmd.Parameters.AddWithValue("@formaPagamento", string.IsNullOrEmpty(p.FormaPagamento) ? DBNull.Value : (object)p.FormaPagamento);
                    cmd.Parameters.AddWithValue("@qtdParcelas", p.QtdParcelas);
                    cmd.Parameters.AddWithValue("@valorDesconto", p.ValorDesconto.HasValue ? (object)p.ValorDesconto.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@porcentagemDesconto", p.PorcentagemDesconto.HasValue ? (object)p.PorcentagemDesconto.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@totalDesconto", p.TotalDesconto.HasValue ? (object)p.TotalDesconto.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@valorPendente", p.ValorPendente.HasValue ? (object)p.ValorPendente.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@totalPago", p.TotalPago.HasValue ? (object)p.TotalPago.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@total", p.Total);
                    cmd.Parameters.AddWithValue("@status", string.IsNullOrEmpty(p.Status) ? DBNull.Value : (object)p.Status);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao inserir pagamento: {ex.Message}");
            }
        }

        public void Update(Pagamento p)
        {
            try
            {
                string sql = @"UPDATE Pagamento SET dataPagamento = @dataPagamento, dataVencimento = @dataVencimento, formaPagamento = @formaPagamento, qtdParcelas = @qtdParcelas, valorDesconto = @valorDesconto, porcentagemDesconto = @porcentagemDesconto, totalDesconto = @totalDesconto, valorPendente = @valorPendente, totalPago = @totalPago, total = @total, status = @status
                                WHERE pagamentoID = @pagamentoID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    if (p.DataPagamento.HasValue)
                        cmd.Parameters.Add("@dataPagamento", MySqlDbType.Date).Value = p.DataPagamento.Value.ToDateTime(TimeOnly.MinValue);
                    else
                        cmd.Parameters.Add("@dataPagamento", MySqlDbType.Date).Value = DBNull.Value;

                    cmd.Parameters.Add("@dataVencimento", MySqlDbType.Date).Value = p.DataVencimento.ToDateTime(TimeOnly.MinValue);
                    cmd.Parameters.AddWithValue("@formaPagamento", string.IsNullOrEmpty(p.FormaPagamento) ? DBNull.Value : (object)p.FormaPagamento);
                    cmd.Parameters.AddWithValue("@qtdParcelas", p.QtdParcelas);
                    cmd.Parameters.AddWithValue("@valorDesconto", p.ValorDesconto.HasValue ? (object)p.ValorDesconto.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@porcentagemDesconto", p.PorcentagemDesconto.HasValue ? (object)p.PorcentagemDesconto.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@totalDesconto", p.TotalDesconto.HasValue ? (object)p.TotalDesconto.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@valorPendente", p.ValorPendente.HasValue ? (object)p.ValorPendente.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@totalPago", p.TotalPago.HasValue ? (object)p.TotalPago.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@total", p.Total);
                    cmd.Parameters.AddWithValue("@status", string.IsNullOrEmpty(p.Status) ? DBNull.Value : (object)p.Status);
                    cmd.Parameters.AddWithValue("@pagamentoID", p.PagamentoID);
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

        public void Delete(int pagamentoID)
        {
            try
            {
                string sql = "DELETE FROM Pagamento WHERE pagamentoID = @pagamentoID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@pagamentoID", pagamentoID);
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

        public List<Pagamento> GetAll()
        {
            List<Pagamento> pagamentos = new List<Pagamento>();
            try
            {
                string sql = "SELECT * FROM Pagamento ORDER BY pagamentoID";

                using (var conexao = Conexao.Conectar())
                using (var comando = new MySqlCommand(sql, conexao))
                using (MySqlDataReader dr = comando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Pagamento pagamento = new Pagamento
                        {
                            PagamentoID = Convert.ToInt32(dr["pagamentoID"]),
                            DataPagamento = dr["dataPagamento"] == DBNull.Value ? (DateOnly?)null : DateOnly.FromDateTime(Convert.ToDateTime(dr["dataPagamento"])),
                            DataVencimento = DateOnly.FromDateTime(Convert.ToDateTime(dr["dataVencimento"])),
                            FormaPagamento = dr["formaPagamento"] == DBNull.Value ? null : dr["formaPagamento"].ToString(),
                            QtdParcelas = Convert.ToInt32(dr["qtdParcelas"]),
                            ValorDesconto = dr["valorDesconto"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["valorDesconto"]),
                            PorcentagemDesconto = dr["porcentagemDesconto"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["porcentagemDesconto"]),
                            TotalDesconto = dr["totalDesconto"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["totalDesconto"]),
                            ValorPendente = dr["valorPendente"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["valorPendente"]),
                            TotalPago = dr["totalPago"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["totalPago"]),
                            Total = Convert.ToDecimal(dr["total"]),
                            Status = dr["status"] == DBNull.Value ? null : dr["status"].ToString()
                        };

                        pagamentos.Add(pagamento);
                    }
                }
                return pagamentos;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar os pagamentos: {ex.Message}");
            }
        }

        public Pagamento BuscarID(int id_pagamento)
        {
            try
            {
                string sql = "SELECT * FROM Pagamento WHERE pagamentoID = @pagamentoID";

                using (var conexao = Conexao.Conectar())
                using (var comando = new MySqlCommand(sql, conexao))
                {
                    comando.Parameters.AddWithValue("@pagamentoID", id_pagamento);

                    using (MySqlDataReader dr = comando.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Pagamento pagamento = new Pagamento();
                            pagamento.PagamentoID = Convert.ToInt32(dr["pagamentoID"]);
                            pagamento.DataPagamento = dr["dataPagamento"] == DBNull.Value ? (DateOnly?)null : DateOnly.FromDateTime(Convert.ToDateTime(dr["dataPagamento"]));
                            pagamento.DataVencimento = DateOnly.FromDateTime(Convert.ToDateTime(dr["dataVencimento"]));
                            pagamento.FormaPagamento = dr["formaPagamento"] == DBNull.Value ? null : dr["formaPagamento"].ToString();
                            pagamento.QtdParcelas = Convert.ToInt32(dr["qtdParcelas"]);
                            pagamento.ValorDesconto = dr["valorDesconto"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["valorDesconto"]);
                            pagamento.PorcentagemDesconto = dr["porcentagemDesconto"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["porcentagemDesconto"]);
                            pagamento.TotalDesconto = dr["totalDesconto"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["totalDesconto"]);
                            pagamento.ValorPendente = dr["valorPendente"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["valorPendente"]);
                            pagamento.TotalPago = dr["totalPago"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["totalPago"]);
                            pagamento.Total = Convert.ToDecimal(dr["total"]);
                            pagamento.Status = dr["status"] == DBNull.Value ? null : dr["status"].ToString();

                            return pagamento;
                        }
                        else
                        {
                            throw new Exception("Nenhum pagamento encontrado com esse ID.");
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