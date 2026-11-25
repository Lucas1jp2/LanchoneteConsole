using System;
using System.Collections.Generic;
using System.Text;

namespace LanchoneteConsole.Mapeamento
{
    internal class Bairro
    {
        public int BairroID { get; set; }
        public string Nome { get; set; }
        private decimal _taxa;
        public decimal Taxa
        {
            get { return _taxa; }
            set
            {
                if (value > 0)
                    _taxa = value;
                else
                    throw new Exception("Erro ao setar a taxa...");
            }
        }
        public int fk_cidadeID { get; set; }
    }
}