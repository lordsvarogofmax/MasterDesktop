using MasterDesktop.Lib.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using System.Configuration;
using NLog;
namespace MasterDesktop.Lib
{
    public static class Utility
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public static string HOST = "";
        public static string PORT = "";
        public static string PROTOCOL = "";
        public const string MD5 = "d55561f495b46e262733602ae825465d";
        public const string USERNAME = "ROOT";

        static Utility()
        {
            HOST = ConfigurationManager.AppSettings["Host"];
            PORT = ConfigurationManager.AppSettings["Port"];
            PROTOCOL = ConfigurationManager.AppSettings["Protocol"];
        }

        //http://127.0.0.1:5000/
        static string url() => $"{PROTOCOL}://{HOST}:{PORT}";

        private static string urlMasterUserAddList(string username = USERNAME) => $"{url()}/master/{username}/add/list/{MD5}";
        private static string urlMasterUserDellList(string username = USERNAME) => $"{url()}/master/{username}/dell/list/{MD5}";
        private static string urlMasterUserGetList(string username = USERNAME) => $"{url()}/master/{username}/get/list/{MD5}";
        private static string urlDeclarationUserAddList(string username = USERNAME) => $"{url()}/declaration/{username}/add/list/{MD5}";
        private static string urlDeclarationUserDellList(string username = USERNAME) => $"{url()}/declaration/{username}/dell/list/{MD5}";
        private static string urlDeclarationUserGetList(string username = USERNAME) => $"{url()}/declaration/{username}/get/list/{MD5}";

        public static string RequestPOST(string url, string json = null)
        {
            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "POST";
                httpRequest.ContentType = "application/json";
                using (var requestStream = httpRequest.GetRequestStream())
                using (var writer = new StreamWriter(requestStream))
                {
                    if (json != null)
                    {
                        writer.Write(json);
                    }
                }
                using (var httpResponse = httpRequest.GetResponse())
                using (var responseStream = httpResponse.GetResponseStream())
                using (var reader = new StreamReader(responseStream))
                {
                    string response = reader.ReadToEnd();
                    return response;
                }
            }
            catch (Exception ex)
            {
                //Ошибка запроса
                logger.Error(ex.ToString());
            }

            return null;
        }

        public static List<Master> GetMaster()
        {
            string json = null;
            List<Master> masters = new List<Master>();
            var page = RequestPOST(urlMasterUserGetList(), json);
            try
            {
                masters = JsonConvert.DeserializeObject<List<Master>>(page);
                return masters;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return new List<Master>();
            }
        }

        public static List<Static> SetMaster(List<Master> masters)
        {
            List<Static> statics = new List<Static>();
            try
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(masters);
                var page = RequestPOST(urlMasterUserAddList(), json);
                statics = JsonConvert.DeserializeObject<List<Static>>(page);
                return statics;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return new List<Static>();
            }
        }

        public static List<Static> SetMaster(Master master) => SetMaster(new List<Master>() { master });

        public static List<Static> DellMaster(List<Master> masters)
        {
            List<Static> statics = new List<Static>();

            try
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(masters);
                var page = RequestPOST(urlMasterUserDellList(), json);
                statics = JsonConvert.DeserializeObject<List<Static>>(page);
                return statics;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return new List<Static>();
            }
        }

        public static List<Static> DellMaster(Master master) => DellMaster(new List<Master>() { master });

        public static List<Declaration> GetDeclaration()
        {
            string json = null;
            List<Declaration> valueList = new List<Declaration>();
            var page = RequestPOST(urlDeclarationUserGetList(), json);
            try
            {
                valueList = JsonConvert.DeserializeObject<List<Declaration>>(page);
                return valueList;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return new List<Declaration>();
            }
        }

        public static List<Static> SetDeclaration(List<Declaration> declarations)
        {
            List<Static> statics = new List<Static>();
            
            try
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(declarations);
                var page = RequestPOST(urlDeclarationUserAddList(), json);
                statics = JsonConvert.DeserializeObject<List<Static>>(page);
                return statics;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return new List<Static>();
            }
        }

        public static List<Static> SetDeclaration(Declaration declaration) => SetDeclaration(new List<Declaration>() { declaration });

        public static List<Static> DellDeclaration(List<Declaration> declarations)
        {
            List<Static> statics = new List<Static>();
            
            try
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(declarations);
                var page = RequestPOST(urlDeclarationUserDellList(), json);
                statics = JsonConvert.DeserializeObject<List<Static>>(page);
                return statics;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return new List<Static>();
            }
        }

        public static List<Static> DellDeclaration(Declaration declaration) => DellDeclaration(new List<Declaration>() { declaration });
    }
}
