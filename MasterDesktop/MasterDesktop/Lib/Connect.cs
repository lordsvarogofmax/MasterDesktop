using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using FirebirdSql.Data.FirebirdClient;
using MasterDesktop.Lib.Data;

namespace MasterDesktop.Lib
{
    public class Connect
    {
        public string Charset;
        public string UserID;
        public string Password;
        public string Database;
        public string ServerType;
        private FbConnectionStringBuilder conn;
        public FbConnection connection;

        public Connect()
        {
            Charset = ConfigurationManager.AppSettings["Charset"];
            UserID = ConfigurationManager.AppSettings["UserID"];
            Password = ConfigurationManager.AppSettings["Password"];
            Database = ConfigurationManager.AppSettings["Database"];
            ServerType = ConfigurationManager.AppSettings["ServerType"];


            conn = new FbConnectionStringBuilder
            {
                Charset = Charset,
                UserID = UserID,
                Password = Password,
                Database = Database,
                ServerType = 0
            };

            try
            {
                connection = new FbConnection(conn.ToString());
                connection.Open();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка подлючения к ДБ {ex.ToString()}");
            }
        } 
    }
}
