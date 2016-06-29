using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FocusMe.Model;
using FocusMe.Commands;
using System.ComponentModel;
using System.Windows;

namespace FocusMe.ViewModel
{
    class ConfigurationManager:ObservableObject
    {
        //This class serves as a ViewModel manager for Configuration.xaml
        public Status Status { get{return status;} set{status=value;OnPropertyChanged("Status");} }
        public ConfigurationManager Manager { get { return this; } }
        //This command is bound to the Launch button in Configuration.xaml
        public LaunchSessionCommand LaunchCommand { get { return launchCommand; } }
        Status status;
        LaunchSessionCommand launchCommand;
        Window window;
        public ConfigurationManager(Window window)
        {
            this.window = window;
            launchCommand = new LaunchSessionCommand(window);
            status = new Status();
        }

        public void CloseWindow()
        {
            this.window.Close();
        }

    }
}
