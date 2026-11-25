using System;
using System.Collections.Generic;
using System.Text;

namespace LanchoneteConsole.Mapeamento
{
    internal class Cliente
    {
        public int ClienteID { get; set; }
        public DateOnly? DataNasc { get; set; }
        public string Nome { get; set; }
        public string Celular { get; set; }
        public int? fk_enderecoID { get; set; }
    }
}