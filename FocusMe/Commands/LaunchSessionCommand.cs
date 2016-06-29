using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FocusMe.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace FocusMe.Commands
{
    public class LaunchSessionCommand:ICommand
    {
        Window window;
        int sessionNumber, sessionLength, breakLength;
        TextBox sessionNumberText, sessionLengthText, breakLengthText;

        public LaunchSessionCommand(Window window)
        {
            this.window = window;
             sessionNumberText = (TextBox)window.FindName("sessionNumbTextBox");
             sessionLengthText = (TextBox)window.FindName("sessionLengthTextBox");
             breakLengthText = (TextBox)window.FindName("breakLengthTextBox");
        }

        public bool CanExecute(object parameter)
        {
            // activates the Launch Button based on the validation of the values from the textBoxes in Configuration.xaml
            try
            {
                sessionNumber = Convert.ToInt32(sessionNumberText.Text);
                sessionLength = Convert.ToInt32(sessionLengthText.Text);
                breakLength = Convert.ToInt32(breakLengthText.Text);
                if (sessionNumber > 0 && sessionLength > 0 && breakLength > 0)
                    return true;
            }
            catch (Exception)
            {

                return false;
            }
            return false;

        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested+=value;}
            remove { CommandManager.RequerySuggested += value; }
        }

        public void Execute(object parameter)
        {
            //Launches the session by creating an instance of the Status.xaml and StatusManager.cs
            //Assigns the StatusManager.cs as a DataContext for Status.xaml
            //Starts the main timer and closes current Configuration.xaml through the ConfigurationManager instance
            FocusMe.ViewModel.ConfigurationManager configManager=(FocusMe.ViewModel.ConfigurationManager)parameter;
            if(configManager!=null)
            {
                FocusMe.View.Status statusWindow = new View.Status();
                statusWindow.Show();
                statusWindow.WindowState = WindowState.Minimized;
                if (configManager.Status.PlayWaves == false)
                    statusWindow.PlayWavesButton.Visibility = Visibility.Hidden;
                StatusManager manager = new StatusManager(configManager.Status, statusWindow);
                statusWindow.DataContext = manager;
                manager.StartTimer();
                configManager.CloseWindow();
            }
        }
    }
}
