using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FocusMe.Model
{
    public class SessionEventArgs:EventArgs
    {
        //This class is used as an arguments object for SessionStatusChanged event in Status.cs
        public bool OnBreak { get; set; }
    }
}
