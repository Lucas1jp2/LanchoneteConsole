using System;
using System.Collections.Generic;
using System.Text;

namespace LanchoneteConsole.Mapeamento
{
    internal class Pedido
    {
        public int PedidoID { get; set; }
        public DateOnly DataPedido { get; set; }
        public bool Preferencial { get; set; }
        public string Status { get; set; }
        public string Observacao { get; set; }
        public string Tipo { get; set; }
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
        public decimal? ValorDesconto { get; set; }
        public int? PorcentagemDesconto { get; set; }
        public decimal? TotalDesconto { get; set; }
        public int fk_funcionarioID { get; set; }
        public int? fk_clienteID { get; set; }
    }
}