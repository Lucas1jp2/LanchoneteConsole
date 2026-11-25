using System;
using System.Collections.Generic;
using System.Text;

namespace LanchoneteConsole.Mapeamento
{
    internal class PagamentoPedido
    {
        public int PagamentoPedidoID { get; set; }
        public int fk_pagamentoID { get; set; }
        public int fk_pedidoID { get; set; }
    }
}