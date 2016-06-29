using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FocusMe.Commands
{
    public class CloseWindowCommand:ICommand
    {
        //This command is used to close any window passed as a parameter
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            Window window = parameter as Window;
            if(window!=null)
            {
                window.Close();
            }
        }
    }
}
