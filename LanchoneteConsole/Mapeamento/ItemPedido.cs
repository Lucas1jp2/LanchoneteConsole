using System;
using System.Collections.Generic;
using System.Text;

namespace LanchoneteConsole.Mapeamento
{
    internal class ItemPedido
    {
        public int ItemPedidoID { get; set; }

        private int _quantidade;
        public int Quantidade
        {
            get { return _quantidade; }
            set
            {
                if (value > 0)
                    _quantidade = value;
                else
                    throw new Exception("Erro ao setar a quantidade...");
            }
        }

        private decimal _valorUnitario;
        public decimal ValorUnitario
        {
            get { return _valorUnitario; }
            set
            {
                if (value > 0)
                    _valorUnitario = value;
                else
                    throw new Exception("Erro ao setar o valor unitário...");
            }
        }

        public decimal? ValorDesconto { get; set; }
        public int? PorcentagemDesconto { get; set; }
        public decimal? TotalDesconto { get; set; }

        private decimal _totalItem;
        public decimal TotalItem
        {
            get { return _totalItem; }
            set
            {
                if (value > 0)
                    _totalItem = value;
                else
                    throw new Exception("Erro ao setar o total do item...");
            }
        }

        public string Observacao { get; set; }
        public int fk_produtoID { get; set; }
        public int fk_pedidoID { get; set; }
    }
}