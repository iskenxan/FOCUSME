using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Threading;
using System.ComponentModel;
using System.Windows.Media;
using System.Threading;
using System.Windows.Media.Imaging;
using System.Collections.ObjectModel;

//Keeps track of the sesion once it started, uses timers for counting time to work and break, plays and pauses alpha waves,takes screens and monitors apps
namespace FocusMe.Model
{
    public class Status:ObservableObject
    {
        //This event is called everytime each session unit has ended and session time supposed to switch to the break time
        public  event EventHandler<SessionEventArgs> SessionStatusChanged;
        //This event is called when all of the session units have ended and it's time to show the results screen
        public event EventHandler SessionStopped;


        //returns the session unit that is currently in use by the timer
        public Session CurrentSession 
        { 
            get 
            {
                return currentSession;
            }
            set
            {
                currentSession = value;
                OnPropertyChanged("CurrentSession");
            }
        }
        //These properties are bound to check boxes in Configuration.xaml
        public bool PlayWaves { get { return playWaves; } set { playWaves = value; OnPropertyChanged("PlayWaves"); } }
        public bool TakeScreens { get { return takeScreens; } set { takeScreens = value; OnPropertyChanged("TakeScreens"); } }
        public bool MonitorApps { get { return monitorApps; } set { monitorApps = value; OnPropertyChanged("MonitorApps"); } }
        public int SessionLength 
        { 
            set
        {
            foreach (Session session in sessions)
            {
                session.SetSessionTime(value);
            }
                OnPropertyChanged("SessionLength");
        }
        }
        public int BreakLength
        {
            set
            {
                foreach (Session session in sessions)
                {
                    session.SetBreakTime(value);
                }
                    OnPropertyChanged("BreakLength");
            }
        }
        //The number of session units that will run during the session. Each session unit is stored in Stack called sessions
        public int SessionsNumber
        {
            get
            {
                return sessions.Count();
            }
            set
            {
                    if (sessions.Count < value)
                    {
                        while (sessions.Count < value)
                        {
                            sessions.Enqueue(new Session());
                        }
                    }
                    else
                    {
                        while (sessions.Count > value)
                        {
                            sessions.Dequeue();
                        }
                    }
                    OnPropertyChanged("SessionsNumber");
                }
              
            }
        //This list contains screen shots taken during the session.It's an Observable object for the purpose of Binding
        public ObservableCollection<BitmapSource> Screens { get { return screens; } }
        public ObservableCollection<BitmapSource> screens;

        private Session currentSession;
        private Queue<Session> sessions;
        private bool playWaves;
        private bool takeScreens;
        private bool monitorApps;
        //This timer runs during the whole session time
        private DispatcherTimer timer = new DispatcherTimer();
        private bool onBreak;
        private int screenTakeInterval;
        private int counter = 0;
        private ScreenCapture screenCapture;

        public Status()
        {
            sessions = new Queue<Session>();
            screens = new ObservableCollection<BitmapSource>();
            playWaves = true;
            monitorApps = true;
            takeScreens = true;
            screenCapture = new ScreenCapture();
        }


        public void StartTimer()
        {//starts the timer and sets some of settings based on the choice the user made when he launched the session
            double totalSeconds = 0;
            timer.Interval = TimeSpan.FromSeconds(1.00);
            timer.Tick += timer_Tick;
            //If user chose to take screens during the session
            if (TakeScreens)
            {
                //screen interval is calculated to take 5 pictures during the whole session at the same interval
                totalSeconds = sessions.Count * sessions.Peek().SessionLength.TotalSeconds;
                screenTakeInterval = (int)totalSeconds / 5;
            }
            CurrentSession = sessions.Dequeue();
            OnPropertyChanged("SessionsNumber");
            timer.Start();
        }


        void timer_Tick(object sender, EventArgs e)
        {
            if(TakeScreens)
            TakeScreen();
            if (!onBreak && CurrentSession.SessionLength.TotalSeconds<=0)
            {
                //when its break time
                if (sessions.Count <= 0)
                {
                    //if all of the session units are finished
                    timer.Stop();
                    SessionStopped(this, null);
                }
                else
                {//if current session unit is finished
                    onBreak = true;
                    SessionStatusChanged(null, new SessionEventArgs { OnBreak = onBreak });
                }
            }
            if(onBreak&&CurrentSession.BreakLength.TotalSeconds<=0)
            {
                //when break time is over
                onBreak = false;
                    CurrentSession = sessions.Dequeue();
                SessionStatusChanged(null, new SessionEventArgs { OnBreak = onBreak });
                OnPropertyChanged("SessionsNumber");
            }
            if (onBreak)
                CurrentSession.BreakLength = CurrentSession.BreakLength.Subtract(TimeSpan.FromSeconds(1));
            else
                CurrentSession.SessionLength = CurrentSession.SessionLength.Subtract(TimeSpan.FromSeconds(1));
        }

        public void TakeScreen()
        {
            //takes time if the interval value is reached by the timer
            if(!onBreak)
                //This counter is used to calculate is interval value is reached
            counter++;
            if (counter % screenTakeInterval == 0)
            {
                screens.Add(screenCapture.CaptureScreen());
            }
        }
        

    }
}
