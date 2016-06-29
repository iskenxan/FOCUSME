# FOCUSME
###Clean and usable application to focus on your work
##Basic Functionality:

* **Set the values for your session before launching it**

![Image of Configuration.xaml]
(https://raw.githubusercontent.com/iskenxan/FOCUSME/cabff64aa4e7109c31f0abff6e729b2568b50402/Config.gif)

* **Timer acts like a constant reminder, plus plays an alarm**

![Image of Status.xaml]
(https://raw.githubusercontent.com/iskenxan/FOCUSME/cabff64aa4e7109c31f0abff6e729b2568b50402/Status.gif)

* **Notification popups help you stay focused**

![Image of Notification.xaml]
(https://raw.githubusercontent.com/iskenxan/FOCUSME/cabff64aa4e7109c31f0abff6e729b2568b50402/Notification.png)

* **Final results window displays screen shots of your work**

![Image of Results.xaml]
(https://raw.githubusercontent.com/iskenxan/FOCUSME/cabff64aa4e7109c31f0abff6e729b2568b50402/Results.gif)

###About Project:
**The application is built using C# and WPF. Structured according to MVVM pattern.**

###Interface:
**For building the interface I used [MetroWindow](https://github.com/MahApps/MahApps.Metro) package in Nuget.**

###Code Snippet:

```
        void EventArrived(object sender, EventArrivedEventArgs e)
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
```

###Areas for improvement
- [ ] **Making notification system more robust and pleasant for user**
- [ ] **Fixing minor bugs**
- [ ] **Any other useful functionality**

####Anybody is welcome to contribute to this project. If something is not working properly use github's issue reporter on the right or shoot me an email at iskenxan11@gmail.com
