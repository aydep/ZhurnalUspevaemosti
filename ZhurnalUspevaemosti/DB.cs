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
        MySqlConnection Connection = new MySqlConnection("Server=ayder2s4.beget.tech;Port=3306;Database=ayder2s4_zhurnal;User Id=ayder2s4_zhurnal;Password=Onm5b-1ju; database=ayder2s4_zhurnal");

        public void openConnection()
        {
            if(Connection.State == System.Data.ConnectionState.Closed)
                Connection.Open();

        }

        public void closeConnection()
        {
            if (Connection.State == System.Data.ConnectionState.Open)
                Connection.Close();
        }

        public MySqlConnection getConnection()
        {
            return Connection;
        }

    }
}
