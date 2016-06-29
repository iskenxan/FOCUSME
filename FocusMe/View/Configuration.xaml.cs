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
using FocusMe.Model;
using MahApps.Metro.Controls;
using FocusMe.ViewModel;
namespace FocusMe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Configuration : MetroWindow
    {
        ConfigurationManager manager;
        public Configuration()
        {
            InitializeComponent();
            manager = new ConfigurationManager(this);
            DataContext = manager;
        }



    }
}
