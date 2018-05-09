using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MasterDesktop.Lib.Data
{
    public class JsonConfig
    {
        //подключине к сервису Андроид
        public Server server { get; set; } = new Server();
        //подключение к ДБ
        public DB db { get; set; } = new DB();

        // статус на запись и чтение конфигов
        [IgnoreDataMember]
        public Status status { get; set; } = Status.Unknown;
        // статус подключинея к сервису
        [IgnoreDataMember]
        public bool status_Server { get; set; } = false;
        // статус подключения к базе
        [IgnoreDataMember]
        public bool status_DB { get; set; } = false;
    }

    public enum Status: int
    {
        Unknown = 0,
        Read = 1,
        Write = 2,
    }

    public class Server
    {
        public string Host { get; set; } = "194.67.207.41";
        public string Port { get; set; } = "45678";
        public string MD5 { get; set; } = "d55561f495b46e262733602ae825465d";
        public string Protocol { get; set; } = "http";
    }

    public class DB
    {
        public string Charset { get; set; } = "UNICODE_FSS";
        public string User { get; set; } = "SYSDBA";
        public string Password { get; set; } = "masterkey";
        public string Host { get; set; } = "localhost";
        public string Path { get; set; } = "C:/Zayavka 1.1/MYD.FDB";
        public string ServerType { get; set; } = "0";
        [IgnoreDataMember]
        public string Connect { get => $"{Host}:{Path}"; }
    }
}
