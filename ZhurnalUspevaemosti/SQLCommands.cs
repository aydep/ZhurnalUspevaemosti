using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace ZhurnalUspevaemosti
{
    class SQLCommands
    {
        MySqlDataAdapter adapter = new MySqlDataAdapter();
        DB db = new DB();

        public void insertCmd(string command)
        {
            MySqlCommand outcmd = new MySqlCommand(command, db.getConnection());
            db.openConnection();
            outcmd.ExecuteNonQuery();
            db.closeConnection();
        }

        public void selectCmd(DataTable table, string command)
        {
            MySqlCommand outcmd = new MySqlCommand(command, db.getConnection());
            adapter.SelectCommand = outcmd;
            adapter.Fill(table);
        }
    }
}
