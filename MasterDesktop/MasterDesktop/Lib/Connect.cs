using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using FirebirdSql.Data.FirebirdClient;

namespace MasterDesktop.Lib
{
    public class Connect
    {
        public string Charset;
        public string UserID;
        public string Password;
        public string Database;
        public string ServerType;
        public static FbConnectionStringBuilder conn = new FbConnectionStringBuilder();

        public Connect()
        {
            Charset = ConfigurationManager.AppSettings["Charset"];
            UserID = ConfigurationManager.AppSettings["UserID"];
            Password = ConfigurationManager.AppSettings["Password"];
            Database = ConfigurationManager.AppSettings["Database"];
            ServerType = ConfigurationManager.AppSettings["ServerType"];


            conn.Charset = Charset;
            conn.UserID = UserID;
            conn.Password = Password;
            conn.Database = Database;
            conn.ServerType = 0;


            try
            {
                FbConnection connection = new FbConnection(conn.ToString());


                connection.Open();

                FbTransaction transaction = connection.BeginTransaction();

                FbCommand command = new FbCommand("SELECT * FROM MASTER", connection, transaction);

                FbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i += 1)
                        Console.Out.WriteLine(reader.GetString(i).ToString());
                }

                reader.Close();
                connection.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка подлючения к ДБ {ex.ToString()}");
            }
        } 
    }
}
