using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FocusMe.Model;
using System.Windows.Media.Imaging;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using FocusMe.Commands;

namespace FocusMe.ViewModel
{
    //This class servers as ViewModel manager for Results.xaml
    class Resultsmanager:ObservableObject
    {
        public Status Status { get{ return status;}}
        //This command is bound to the button in Results.xaml
        public RestartSessionCommand RestartCommand { get { return restartCommand; }}
        Window window;
        Status status;
        RestartSessionCommand restartCommand;
        public Resultsmanager(Status status,Window window)
        {
            this.window = window;
            this.status = status;
            restartCommand = new RestartSessionCommand();
            //if the user chose not to take screenshots then the Results.xaml will be modified accordingly
            if (status.TakeScreens == false)
                SwitchResultsWindow();
        }
        private void SwitchResultsWindow()
        {
            //Modifies Results.xaml to hide the FlipView control which shows the screen shots as a slide show
            //Applies other changes to Results.xaml
            TextBlock textBlock2 = window.FindName("textBlock2") as TextBlock;
            textBlock2.Text = "Your session has ended";
            FlipView flipView = window.FindName("flipView") as FlipView;
            flipView.Visibility = Visibility.Hidden;
            window.Width = 350;
            window.Height = 200;
        }
    }
}
