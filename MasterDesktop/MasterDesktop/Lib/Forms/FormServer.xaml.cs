using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MasterDesktop.Lib.Data;

namespace MasterDesktop.Lib.Forms
{
    /// <summary>
    /// Логика взаимодействия для FormServer.xaml
    /// </summary>
    public partial class FormServer : Window
    {
        public Match match;
        private static Server new_server = new Server() { Host = "", MD5 = "", Port = "", Protocol = "http" };
        private static bool isIP = false;
        private static bool isPort = false;
        private static bool isMD5 = false;
        private static bool StartInit = true;
        public FormServer()
        {
            StartInit = true;
            InitializeComponent();
            StartInit = false;

            textBox_IP_Servis.Text = Utility.config.server.Host;
            textBox_Port_Servis.Text = Utility.config.server.Port;
            textBox_MD5_Servis.Text = Utility.config.server.MD5;
            var pro = Utility.config.server.Protocol.Trim().ToLower();
            combobox_Protocol_Server.SelectedIndex = pro == "http" ? 0 : pro == "https" ? 1 : 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Отмена
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Сохранить
            IsSetingServer(IP: textBox_IP_Servis.Text, Port: textBox_Port_Servis.Text, MD5: textBox_MD5_Servis.Text);
            if (isIP && isPort && isMD5)
            {
                Utility.SetSettingServer(new_server);
                if (Utility.WrireSetting())
                {
                    label_Info_Server.Content = "Настройки сохранены";
                    label_Info_Server.Foreground = new SolidColorBrush(Colors.Green);
                }
                else
                {
                    label_Info_Server.Content = "Не удалось сохранить настройки";
                    label_Info_Server.Foreground = new SolidColorBrush(Colors.Red);
                }
                
            }
            else
            {
                label_Info_Server.Content = "Не правильно установлены настройки";
                label_Info_Server.Foreground = new SolidColorBrush(Colors.Red);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //Сбросить 
            new_server = new Server();
            textBox_IP_Servis.Text = new_server.Host;
            textBox_Port_Servis.Text = new_server.Port;
            textBox_MD5_Servis.Text = new_server.MD5;
            combobox_Protocol_Server.SelectedIndex = 0;
            IsSetingServer(IP: textBox_IP_Servis.Text, Port: textBox_Port_Servis.Text, MD5: textBox_MD5_Servis.Text);
            label_Info_Server.Content = "";
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //Проверить соединение
            bool isSS = IsSetingServer(IP: textBox_IP_Servis.Text, Port: textBox_Port_Servis.Text, MD5: textBox_MD5_Servis.Text);
            bool isConnect = Utility.isConnectServer(new_server);

            if (isSS && isConnect)
            {
                label_Info_Server.Foreground = new SolidColorBrush(Colors.Green);
                label_Info_Server.Content = "Соедине с сервером установленно";
            }
            else
            {
                label_Info_Server.Foreground = new SolidColorBrush(Colors.Red);
                label_Info_Server.Content = "Не удалось соединится с сервером";
            }
        }

        private bool IsSetingServer(string IP, string Port, string MD5)
        {
            new_server.Protocol = combobox_Protocol_Server?.SelectionBoxItem?.ToString()?.ToLower()?.Trim();

            if (Utility.isIpAdress(IP))
            {
                isIP = true;
                new_server.Host = IP.Trim();
                label_Ip_Server.Foreground = new SolidColorBrush(Colors.Black);
            }
            else
            {
                isIP = false;
                new_server.Host = "";
                label_Ip_Server.Foreground = new SolidColorBrush(Colors.Red);
            }

            if (Utility.isPost(Port))
            {
                isPort = true;
                new_server.Port = Port.Trim();
                label_Port_Server.Foreground = new SolidColorBrush(Colors.Black);
            }
            else
            {
                isPort = false;
                new_server.Port = "";
                label_Port_Server.Foreground = new SolidColorBrush(Colors.Red);
            }
            
            if (Utility.IsMD5(MD5))
            {
                isMD5 = true;
                new_server.MD5 = MD5.Trim();
                label_MD5_Server.Foreground = new SolidColorBrush(Colors.Black);
            }
            else
            {
                isMD5 = false;
                new_server.MD5 = "";
                label_MD5_Server.Foreground = new SolidColorBrush(Colors.Red);
            }

            if (isIP && isPort && isMD5)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void combobox_Protocol_Server_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.AddedItems.Count > 0)
            {
                if (StartInit)
                {
                    var pro = Utility.config.server.Protocol.Trim().ToLower();
                    combobox_Protocol_Server.SelectedIndex = pro == "http" ? 0 : pro == "https" ? 1 : 0;
                }
            }
        }
    }
}
