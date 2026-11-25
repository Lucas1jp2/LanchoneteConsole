using System;
using System.Collections.Generic;
using System.Text;

namespace LanchoneteConsole.Mapeamento
{
    internal class Pagamento
    {
        public int PagamentoID { get; set; }
        public DateOnly? DataPagamento { get; set; }
        public DateOnly DataVencimento { get; set; }
        public string FormaPagamento { get; set; }

        private int _qtdParcelas;
        public int QtdParcelas
        {
            get { return _qtdParcelas; }
            set
            {
                if (value > 0)
                    _qtdParcelas = value;
                else
                    throw new Exception("Erro ao setar a quantidade de parcelas...");
            }
        }

        public decimal? ValorDesconto { get; set; }
        public int? PorcentagemDesconto { get; set; }
        public decimal? TotalDesconto { get; set; }
        public decimal? ValorPendente { get; set; }
        public decimal? TotalPago { get; set; }

        private decimal _total;
        public decimal Total
        {
            get { return _total; }
            set
            {
                if (value > 0)
                    _total = value;
                else
                    throw new Exception("Erro ao setar o total...");
            }
        }

        public string Status { get; set; }
    }
}