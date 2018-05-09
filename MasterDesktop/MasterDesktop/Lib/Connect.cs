using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using FirebirdSql.Data.FirebirdClient;
using MasterDesktop.Lib.Data;
using NLog;

namespace MasterDesktop.Lib
{
    public class Connect
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private FbConnectionStringBuilder conn;
        public FbConnection connection;
        public static bool isConnect = false;

        public Connect(JsonConfig config)
        {
            StartConnect(config);
        }

        public bool StartConnect(JsonConfig config)
        {
            conn = new FbConnectionStringBuilder
            {
                Charset = config.db.Charset,
                UserID = config.db.User,
                Password = config.db.Password,
                Database = config.db.Connect,
                ServerType = 0
            };

            isConnect =  _StartConnet(conn);
            return isConnect;
        }

        private bool _StartConnet(FbConnectionStringBuilder conn)
        {
            try
            {
                connection = new FbConnection(conn.ToString());
                connection.Open();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка подлючения к ДБ {ex.ToString()}");
                return false;
            }
        }
    }
}
