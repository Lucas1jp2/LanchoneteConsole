using System;
using System.Collections.Generic;
using System.Text;

namespace LanchoneteConsole.Mapeamento
{
    internal class Cidade
    {
        public int CidadeID { get; set; }
        public string Nome { get; set; }
        public int fk_estadoID { get; set; }
    }
}