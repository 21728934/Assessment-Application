using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mazibuko_Application
{
    class DB_Class
    {
        public static SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Code Art\Documents\LulamaDB.mdf;Integrated Security=True;Connect Timeout=30");
        public static SqlCommand cmd = new SqlCommand();
    }
}
