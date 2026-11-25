using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace LanchoneteConsole.Database
{
    internal class Conexao
    {
        private const string strConexao =
            "server = localhost; uid = root; pwd = root; database = Lanchonete; port = 3306";
        public static MySqlConnection Conectar()
        {
            MySqlConnection conexao = new MySqlConnection(strConexao);
            try
            {
                conexao.Open();
                Console.WriteLine("Conectado...");
                return conexao;
            }
            catch (Exception ex)
            {
                throw new Exception("ERRO ao conectar ...");
            }
        }
    }
}
