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
        public static FbConnectionStringBuilder conn = new FbConnectionStringBuilder();
        FbConnection connection;

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
                connection = new FbConnection(conn.ToString());
                connection.Open();
                connection.Close();

                GetMaster();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка подлючения к ДБ {ex.ToString()}");
            }
        } 

        public List<Master> GetMaster()
        {
            List<Master> masters = new List<Master>();
            try
            {

                connection.Open();

                FbTransaction transaction = connection.BeginTransaction();
                FbCommand command = new FbCommand("SELECT * FROM MASTER", connection, transaction);
                FbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Master master = new Master() { };

                    master.IDMASTER = reader.GetInt32(0);
                    master.SHOTNAMEMASTER = reader.GetString(1);
                    master.FULLNAMEMASTER = reader.GetString(2);
                    master.BIRTHDAY = reader.IsDBNull(3) ? (DateTime?) null : reader.GetDateTime(3);
                    master.BIRTHPLACE = reader.GetString(4);
                    master.ADRESSREGISTR = reader.GetString(5);
                    master.ADRESSFACT = reader.GetString(6);
                    master.DATARAB = reader.IsDBNull(7) ? (DateTime?) null : reader.GetDateTime(7);
                    master.DATAUVOLN = reader.IsDBNull(8) ? (DateTime?) null : reader.GetDateTime(8);
                    master.NOTES = reader.GetString(9);
                    master.PRIZNUVOLN = reader.GetInt32(10);
                    master.PRICHINAUV = reader.GetString(11);
                    master.TELEFON = reader.GetString(12);
                    master.FOTO = reader.GetString(13);
                    
                    masters.Add(master);
                }

                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                
            }

            return masters;
        }
    }
}
