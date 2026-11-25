using System;
using System.Collections.Generic;
using System.Text;

namespace LanchoneteConsole.Mapeamento
{
    internal class Entrega
    {
        public int EntregaID { get; set; }

        private decimal _taxaEntrega;
        public decimal TaxaEntrega
        {
            get { return _taxaEntrega; }
            set
            {
                if (value > 0)
                    _taxaEntrega = value;
                else
                    throw new Exception("Erro ao setar a taxa de entrega...");
            }
        }

        public string Entregador { get; set; }
        public string Observacao { get; set; }
        public int fk_enderecoID { get; set; }
        public int fk_pedidoID { get; set; }
    }
}