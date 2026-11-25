using System;
using System.Collections.Generic;
using System.Text;

namespace LanchoneteConsole.Mapeamento
{
    internal class Funcionario
    {
        public int FuncionarioID { get; set; }
        public DateOnly? DataNasc { get; set; }
        public string Cargo { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }
        public string Cpf { get; set; }
        private decimal _salario;
        public decimal Salario
        {
            get { return _salario; }
            set
            {
                if (value > 0)
                    _salario = value;
                else
                    throw new Exception("Erro ao setar o salário...");
            }
        }
        public int? fk_enderecoID { get; set; }
    }
}