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
    public class Adapter
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static Connect connect;

        public Adapter()
        {
            connect = new Connect();
        }

        public List<Master> GetMaster()
        {
            FbConnection connection = connect.connection;
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
                    master.BIRTHDAY = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3);
                    master.BIRTHPLACE = reader.GetString(4);
                    master.ADRESSREGISTR = reader.GetString(5);
                    master.ADRESSFACT = reader.GetString(6);
                    master.DATARAB = reader.IsDBNull(7) ? (DateTime?)null : reader.GetDateTime(7);
                    master.DATAUVOLN = reader.IsDBNull(8) ? (DateTime?)null : reader.GetDateTime(8);
                    master.NOTES = reader.GetString(9);
                    master.PRIZNUVOLN = reader.IsDBNull(10) ? (int?)null : reader.GetInt32(10);
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
                logger.Error(ex.ToString());
                connection.Close();
            }

            return masters;
        }

        public List<Sostzakaz> GetSostzakaz()
        {
            FbConnection connection = connect.connection;
            List<Sostzakaz> sostzakaz = new List<Sostzakaz>();

            try
            {
                connection.Open();

                FbTransaction transaction = connection.BeginTransaction();
                FbCommand command = new FbCommand("SELECT * FROM SOSTZAKAZ", connection, transaction);
                FbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Sostzakaz zakaz = new Sostzakaz() { };

                    zakaz.SOSTZAKAZID = reader.GetInt32(0);
                    zakaz.SOSTZAKAZ = reader.GetString(1);
                    zakaz.COLOR = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2);

                    sostzakaz.Add(zakaz);
                }

                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                connection.Close();
            }

            return sostzakaz;
        }

        private List<Declaration> GetDeclarationSELECT(string SELECT)
        {
            FbConnection connection = connect.connection;
            List<Declaration> declarations = new List<Declaration>();

            try
            {
                connection.Open();

                FbTransaction transaction = connection.BeginTransaction();
                FbCommand command = new FbCommand(SELECT, connection, transaction);
                FbDataReader reader = command.ExecuteReader();


                while (reader.Read())
                {
                    Declaration decl = new Declaration() { };

                    decl.DECLID = reader.GetInt32(0);
                    decl.DECLN = reader.IsDBNull(1) ? (int?)null : reader.GetInt32(1);
                    decl.DECLDATA = reader.IsDBNull(2) ? (DateTime?)null : reader.GetDateTime(2);
                    decl.KVN = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3);
                    decl.TELEFON = reader.GetString(4);
                    decl.FIO = reader.GetString(5);
                    decl.DECLARATION = reader.GetString(6);
                    decl.DECLTIME = reader.IsDBNull(7) ? (DateTime?)null : reader.GetDateTime(7);
                    decl.PODCOD = reader.IsDBNull(8) ? (int?)null : reader.GetInt32(8);
                    decl.MASTERID = reader.IsDBNull(9) ? (int?)null : reader.GetInt32(9);
                    decl.DATAVIPOLN = reader.IsDBNull(10) ? (DateTime?)null : reader.GetDateTime(10);
                    decl.SOSTZAKAZID = reader.IsDBNull(11) ? (int?)null : reader.GetInt32(11);
                    decl.NOTES = reader.GetString(12);
                    decl.ZAPLANIR = reader.IsDBNull(13) ? (DateTime?)null : reader.GetDateTime(13);
                    decl.USERNAME = reader.GetString(14);

                    declarations.Add(decl);
                }

                reader.Close();
                connection.Close();

            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                connection.Close();
            }

            return declarations;
        }

        public List<Declaration> GetDeclarationAll()
        {
            string SELECT = "SELECT * FROM DECLARATION";
            List<Declaration> decl = GetDeclarationSELECT(SELECT);

            return decl;
        }

        public List<Declaration> GetDeclarationDate(string year, string month, string day)
        {
            string SELECT = $"SELECT * FROM DECLARATION WHERE DECLDATA = '{year}-{month}-{day}'";
            List<Declaration> decl = GetDeclarationSELECT(SELECT);

            return decl;
        }

        public Street GetAdressPODID(string ID)
        {
            FbConnection connection = connect.connection;
            Street street = new Street();
            string SELECT = $" SELECT  S.streetname, P.domn, P.doml FROM POD P LEFT JOIN STREET S ON P.podidstreet = S.streetid WHERE PODID = {ID}";

            try
            {
                connection.Open();

                FbTransaction transaction = connection.BeginTransaction();
                FbCommand command = new FbCommand(SELECT, connection, transaction);
                FbDataReader reader = command.ExecuteReader();



                if (reader.Read())
                {
                    street.STREETNAME = reader.GetString(0);
                    street.DOMN = reader.IsDBNull(1) ? (int?)null : reader.GetInt32(1);
                    street.DOML = reader.GetString(2);
                }

                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                connection.Close();
            }

            return street;
        }
    }
}
