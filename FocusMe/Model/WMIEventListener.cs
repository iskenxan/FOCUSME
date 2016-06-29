using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Windows;
using FocusMe.View;
using System.Windows.Threading;
using FocusMe.Model;
using FocusMe.Commands;
using FocusMe.Model.WindAPI;

namespace FocusMe.Model
{
    class WMIEventListener:ObservableObject
    {
        //This class is used to listen to Window processes running and create a popup reminder for user
        ManagementEventWatcher watcher;
        ManagementScope scope;
        string computerName = "localhost";
        string wmiQuery;
        string popupMessage = "";
        string processTitle="";
        Window window;
        CloseWindowCommand closeWindow;
        //This properties are bound to the popup window
        public string PopupMessage { get { return popupMessage; } set { popupMessage = value; OnPropertyChanged("PopupMessage"); } }
        public CloseWindowCommand CloseWindow { get { return closeWindow; } }
        public string ProcessTitle { get { return processTitle; } set { processTitle = value; OnPropertyChanged("ProcessTitle"); } }
        public WMIEventListener(Window window)
        {
            try
            {
                //lanches the listener and specifies its process scope
                scope = new ManagementScope(String.Format("\\\\{0}\\root\\CIMV2", computerName), null);
                scope.Connect();
                wmiQuery = "Select * From __InstanceCreationEvent Within 1 " +
                    "Where TargetInstance ISA 'Win32_Process' ";
                watcher = new ManagementEventWatcher(scope, new EventQuery(wmiQuery));
                //this event fires every time new window process is lanched
                watcher.EventArrived += watcher_EventArrived;
                this.window = window;
                closeWindow = new CloseWindowCommand();
                
            }
            catch (Exception)
            {

                MessageBox.Show("An error monitoring processes");
            }
 
        }

        void watcher_EventArrived(object sender, EventArrivedEventArgs e)
        {
            //gets the title of the process and creates a popup notification message
            //uses the dispatcher to run the method in the UI thread
            window.Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(() =>
            {
                processTitle = (string)((ManagementBaseObject)e.NewEvent.Properties["TargetInstance"].Value)["Name"];
                var previousPopup = User32.FindWindow(null, ProcessTitle);
                if(previousPopup==IntPtr.Zero)
                {

                    popupMessage = "You launched " + processTitle + ". Do you really need it?";
                    Popup popup = new Popup();
                    popup.DataContext = this;
                    popup.Show();
                }
            }));
        }

        public void StartWatcher()
        {
            //starts the listener
            watcher.Start();
        }

        public void StopWatcher()
        {
            //stops the listener
            watcher.Stop();
        }

    }
}
