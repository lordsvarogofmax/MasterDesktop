using NLog;
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
using System.Windows.Shapes;

namespace MasterDesktop.Lib.Forms
{
    /// <summary>
    /// Логика взаимодействия для FormSettings.xaml
    /// </summary>
    public partial class FormSettings : Window
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            //Application.Current.Shutdown();
        }


        public FormSettings()
        {
            InitializeComponent();
        }
    }
}
