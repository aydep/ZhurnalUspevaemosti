using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ZhurnalUspevaemosti
{
    class DB
    {
        MySqlConnection connection = new MySqlConnection("Server=ayder2s4.beget.tech;Port=3306;Database=ayder2s4_zhurnal;User Id=ayder2s4_zhurnal;Password=Onm5b-1ju; database=ayder2s4_zhurnal");

        public void openConnection()
        {
            if(connection.State == System.Data.ConnectionState.Closed)
                connection.Open();

        }

        public void closeConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }

        public MySqlConnection getConnection()
        {
            return connection;
        }

    }
}
