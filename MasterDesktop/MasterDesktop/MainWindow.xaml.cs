using MasterDesktop.Lib;
using MasterDesktop.Lib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace MasterDesktop
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Master> masters;
        public MainWindow()
        {
            var connect = new Connect();
            masters = connect.GetMaster();


            InitializeComponent();

            MASTERS.BeginInit();
            MASTERS.ItemsSource = masters;
            MASTERS.EndInit();

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            MASTERS.BeginInit();
            MASTERS.ItemsSource = masters;
            MASTERS.EndInit();
        }
    }
}
