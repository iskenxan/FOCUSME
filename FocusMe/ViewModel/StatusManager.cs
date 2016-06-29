using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FocusMe.Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Threading;
using FocusMe.View;

namespace FocusMe.ViewModel
{
    public class StatusManager:ObservableObject
    {
        //This class is used as an ViewModel manager for Status.xaml

        public FocusMe.Model.Status Status { get { return status; } set { status = value; OnPropertyChanged("Status"); } }
        FocusMe.Model.Status status;
        public string SessionInfo { get { return sessionInfo; } set { sessionInfo = value; OnPropertyChanged("SessionInfo"); } }

        //This property is bound to the toggle button in Status.xaml which is used to play and stop the MediaPlayer with the alpha waves
        public bool IsPlayingAlpha { get { return isPlayingAlpha; } set { isPlayingAlpha = value; if (isPlayingAlpha)wavesPlayer.Play(); else wavesPlayer.Pause(); OnPropertyChanged("IsPlayingAlpha"); } }

        bool isPlayingAlpha;
        Window window;
        Label timerDisplay;
        string sessionInfo = "Session Time Left";
        MediaPlayer wavesPlayer;
        WMIEventListener processListener;
        public StatusManager(FocusMe.Model.Status status,Window window)
        {
            this.status = status;
            this.window = window;
            timerDisplay = window.FindName("TimerLabel") as Label;
            //when session status changes between the break and the session time
            status.SessionStatusChanged += status_SessionStatusChanged; 
            wavesPlayer = new MediaPlayer();
            wavesPlayer.Open(new Uri("Media/waves.mp3", UriKind.RelativeOrAbsolute));
            wavesPlayer.MediaEnded += wavesPlayer_MediaEnded;
            status.SessionStopped += status_SessionStopped;
            //listener used to listen to the windows  processes and display  notification window
            processListener = new WMIEventListener(window);
        }
        public void StartTimer()
        {
            //Starts the timer and calles method based on the user choise in Configuration.xaml
            Status.StartTimer();
            if (Status.PlayWaves)
                IsPlayingAlpha = true;
            if (Status.MonitorApps)
            processListener.StartWatcher();
        }

        public void PlayAlarm()
        {
            //Plays an alarm sound in different thread and freezes the main thread in order to pause the timer
            ThreadPool.QueueUserWorkItem(_ =>
            {
                MediaPlayer player = new MediaPlayer();
                player.Open(new Uri("Media/alarm.mp3", UriKind.RelativeOrAbsolute));
                player.Play();
            });
            Thread.Sleep(6000);
        }

        void status_SessionStatusChanged(object sender, SessionEventArgs e)
        {
            //if the status of the session is on break the Status.xaml is modified accordingly
            if(e.OnBreak)
            {
                IsPlayingAlpha = false;
                PlayAlarm();
                SessionInfo = "Break Time Left";
                Binding labelBinding = new Binding("BreakLength");
                labelBinding.Source = Status.CurrentSession;
                timerDisplay.SetBinding(Label.ContentProperty, labelBinding);

            }
            else
            {
                PlayAlarm();
                IsPlayingAlpha = true;
                SessionInfo = "Session Time Left";
                Binding labelBinding = new Binding("SessionLength");
                labelBinding.Source = Status.CurrentSession;
                timerDisplay.SetBinding(Label.ContentProperty, labelBinding);
            }

        }

        void wavesPlayer_MediaEnded(object sender, EventArgs e)
        {
            //if the alpha waves sound record ended during the session,the player starts it all over again
            wavesPlayer.Position = TimeSpan.Zero;
            IsPlayingAlpha = true;
        }

        public void CloseWindow()
        {
            window.Close();
        }

        void status_SessionStopped(object sender, EventArgs e)
        {
            //when the session is over it creates an instance of the Results.xaml to display the summary of the session with the screen shots
            IsPlayingAlpha = false;
            processListener.StopWatcher();
            Results resultsWindow = new Results();
            Resultsmanager rManager = new Resultsmanager(this.Status,resultsWindow);
            resultsWindow.DataContext = rManager;
            resultsWindow.Show();
            CloseWindow();
        }
    }
}
