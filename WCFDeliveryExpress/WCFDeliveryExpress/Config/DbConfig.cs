using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryExpress.Config
{
    public static class DbConfig
    {
        private static SqlConnection con = new SqlConnection();
        //        private static string conString = @"DIVINEBRAD\SQLEXPRESS;Initial Catalog=DeliveryExpress;Integrated Security=True";
        private static string conString = @"Data Source=(localdb)\MSSQLLOCALDB;Initial Catalog=DeliveryExpress;Integrated Security=True";
        public static SqlConnection connection 
        {
            get
            {
                con = new SqlConnection(conString);
                return con;
            }
         
        }

    }
}
