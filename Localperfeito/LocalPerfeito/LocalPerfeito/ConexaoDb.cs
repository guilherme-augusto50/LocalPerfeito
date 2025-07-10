using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace LocalPerfeito
{
    class ConexaoDb
    {
        private string connectionString = "Server=localhost;Database=localperfeito;Uid=root;Pwd=;";

        public MySqlConnection Conectar()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                return connection;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao conectar ao banco de dados: " + ex.Message);
                return null;
            }
        }
    }
}
