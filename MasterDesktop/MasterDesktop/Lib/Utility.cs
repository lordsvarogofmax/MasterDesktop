﻿using MasterDesktop.Lib.Data;
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
        public static JsonConfig config;
        public static string HOST = "";
        public static string PORT = "";
        public static string PROTOCOL = "";
        public static string MD5;
        public const string USERNAME = "ROOT";
        public static string PathJsonConfig;

        static Utility()
        {
            PathJsonConfig = $"{Environment.CurrentDirectory}\\JsonConfig.json";
        }

        private static bool setJsonConfig()
        {
            if (System.IO.File.Exists(PathJsonConfig))
            {
                try
                {
                    string json = System.IO.File.ReadAllText(PathJsonConfig, Encoding.UTF8);
                    config = JsonConvert.DeserializeObject<JsonConfig>(json);
                    return true;
                }
                catch(Exception ex)
                {
                    config = new JsonConfig();
                    logger.Error(ex.ToString());
                    return false;
                }

            }
            else
            {
                try
                {
                    config = new JsonConfig();
                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(config);
                    System.IO.File.WriteAllText(PathJsonConfig, json, Encoding.UTF8);
                    return false;
                }
                catch(Exception ex)
                {
                    logger.Error(ex.ToString());
                    return false;
                }
            }
        }
        public static bool LoadSetting()
        {
            bool res = setJsonConfig();
            HOST = config.server.Host;
            PORT = config.server.Port;
            PROTOCOL = config.server.Protocol;
            MD5 = config.server.MD5;
            return res;
        }

        public static bool ConnetcService()
        {
            return false;
        }

        public static bool WrireSetting()
        {
            try
            {
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(config);
                System.IO.File.WriteAllText(PathJsonConfig, json, Encoding.UTF8);
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return false;
            }
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
