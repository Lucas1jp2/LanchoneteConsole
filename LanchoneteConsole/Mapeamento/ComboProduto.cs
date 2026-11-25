using System;
using System.Collections.Generic;
using System.Text;

namespace LanchoneteConsole.Mapeamento
{
    internal class ComboProduto
    {
        public int ComboProdutoID { get; set; }
        public int fk_comboID { get; set; }
        public int fk_produtoID { get; set; }
    }
}