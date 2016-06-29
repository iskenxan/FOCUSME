using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FocusMe.View;
using FocusMe.Model;
using FocusMe.ViewModel;

namespace FocusMe.Commands
{
    class RestartSessionCommand:ICommand
    {
        //This command is used to restart in Restart.xaml to restart the session once it's over
        //Creates an instane of Configuration.xaml and ConfigurationManager
        //Assigns ConfigurationManager to Configuration.xaml as a DataContext property
        //Closes current Results.xaml window
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            Window window = parameter as Window;
            Configuration configWindow = new Configuration();
            ConfigurationManager configManager = new ConfigurationManager(configWindow);
            configWindow.DataContext = configManager;
            configWindow.Show();
            window.Close();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }
    }
}
