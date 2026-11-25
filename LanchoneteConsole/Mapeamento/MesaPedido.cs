using System;
using System.Collections.Generic;
using System.Text;

namespace LanchoneteConsole.Mapeamento
{
    internal class MesaPedido
    {
        public int MesaPedidoID { get; set; }
        public int fk_pedidoID { get; set; }
        public int fk_mesaID { get; set; }
    }
}