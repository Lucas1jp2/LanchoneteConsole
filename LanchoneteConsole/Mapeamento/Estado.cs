using System;
using System.Collections.Generic;
using System.Text;

namespace LanchoneteConsole.Mapeamento
{
    internal class Estado
    {
        public int EstadoID { get; set; }
        public string Nome { get; set; }
        public int fk_paisID { get; set; }
    }
}