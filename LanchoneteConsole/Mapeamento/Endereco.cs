using System;
using System.Collections.Generic;
using System.Text;

namespace LanchoneteConsole.Mapeamento
{
    internal class Endereco
    {
        public int EnderecoID { get; set; }
        public int? Numero { get; set; }
        public string Observacao { get; set; }
        public int fk_bairroID { get; set; }
    }
}