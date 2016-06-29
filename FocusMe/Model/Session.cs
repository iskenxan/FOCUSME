using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusMe.Model
{
    //This class is used as a Node for each session unit
    //It has properties for session length and break length and methods to set the value of those properties in minutes
    public class Session:ObservableObject
    {
        public TimeSpan SessionLength {
            get
            {
                return sessionLength;
            }
            set
            {
                sessionLength = value;
                OnPropertyChanged("SessionLength");
            }

        }

       public TimeSpan BreakLength {
            get
            {
                return breakLength;
            }
            set
            {
                breakLength = value;
                OnPropertyChanged("BreakLength");
            }

        }


        private TimeSpan sessionLength;
        private TimeSpan breakLength;

        public Session()
        {

        }
        public void SetSessionTime(int sessionLength)
        {
            this.sessionLength = TimeSpan.FromMinutes(sessionLength);
        }
        public void SetBreakTime(int breakLength)
        {
            this.breakLength = TimeSpan.FromMinutes(breakLength);
        }
    }
}
