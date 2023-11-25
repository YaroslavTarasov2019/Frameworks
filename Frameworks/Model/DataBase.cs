using MySql.Data.MySqlClient;
using System.Data;

namespace Frameworks.Model
{
    public class DataBase
    {
        MySqlConnection connection = new MySqlConnection("server=localhost;user=root;password=ASDFGHJKL12345;database=fooddelivery");
        public void openConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed) connection.Open();
        }
        public void closeConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open) connection.Close();
        }
        public MySqlConnection GetConnection()
        {
            return connection;
        }
        public bool IsConnectionOpen()
        {
            if (connection != null)
            {
                return connection.State == ConnectionState.Open;
            }
            return false;
        }
    }
}
