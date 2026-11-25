using System;
using System.Collections.Generic;
using System.Text;

namespace LanchoneteConsole.Mapeamento
{
    internal class Produto
    {
        public int ProdutoID { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        private decimal _valor;
        public decimal Valor
        {
            get { return _valor; }
            set
            {
                if (value > 0)
                    _valor = value;
                else
                    throw new Exception("Erro ao setar o valor...");
            }
        }
    }
}