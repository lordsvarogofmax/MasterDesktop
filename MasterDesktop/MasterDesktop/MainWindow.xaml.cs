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
        public Adapter adapter;
        public MainWindow()
        {
            adapter = new Adapter();

            //var M = adapter.GetMaster();
            //var S = adapter.GetSostzakaz();
            //var D = adapter.GetDeclarationDate();


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

        private void CALENDARDECLARATION_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            Calendar cal = (Calendar)sender;
            List<Declaration> ListDeclarations = new List<Declaration>();
            string year = cal.SelectedDate.Value.Year.ToString();
            string month = cal.SelectedDate.Value.Month.ToString();
            string day = cal.SelectedDate.Value.Day.ToString();
            LISTZAKAZ.ItemsSource = null;
            STATUS.Content = null;
            STREETLabel.Content = null;

            try
            {
                //LISTZAKAZ.BeginInit();
                ListDeclarations = adapter.GetDeclarationDate(year, month, day);

                
                LISTZAKAZ.ItemsSource = ListDeclarations;
                //LISTZAKAZ.EndInit();

                //LNAME.BeginInit();
                LNAME.Content = $"{day}-{month}-{year}";
                //LNAME.BeginInit();

                

            }
            catch (Exception ex)
            {

            }
            


        }

        private void LISTZAKAZ_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Declaration decla = (Declaration) (e.AddedItems.Count != 0 ? e.AddedItems[0] : null );

            // ADRESS.DataContext = decla.
            if (decla != null)
            {
                STATUS.Content = adapter.GetSostzakaz().FirstOrDefault(f => f.SOSTZAKAZID == decla.SOSTZAKAZID);

                Street adress = adapter.GetAdressPODID(decla.PODCOD.ToString());

                STREETLabel.Content = $"{adress.STREETNAME}. дом {adress.DOMN} {adress.DOML}";
            }
            else
            {
                STATUS.Content = null;
                STREETLabel.Content = null;
            }

        }

    }
}
