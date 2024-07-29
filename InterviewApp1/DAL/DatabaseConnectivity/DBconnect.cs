using System;
using System.Data;
using System.Data.SqlClient;


namespace InterviewApp1.DAL.DatabaseConnectivity
{
    internal class DBconnect : IDisposable
    {
        private readonly string _connectionString;
        private SqlConnection _connection;

        public DBconnect()
        {
            _connectionString = string.Format(@"Data Source=MSI\SQLEXPRESS;" +
          "Initial Catalog=Interview01;" +
          "User id=sa;" +
          "Password=admin123; MultipleActiveResultSets=true ; Encrypt=False");
        }

        public SqlConnection GetOpenConnection()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection;
        }

        public SqlDataReader ReadTable(string readStr)
        {
            SqlConnection connection = GetOpenConnection();
            var command = new SqlCommand(readStr, connection);
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public bool AddEditDel(string AddEditDelStr)
        {
            SqlConnection connection = GetOpenConnection();
            var command = new SqlCommand(AddEditDelStr, connection);
            int affectedRows = command.ExecuteNonQuery();
            connection.Close(); // Close the connection after executing the query
            return affectedRows > 0;
        }

        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }
        }

    }
}
