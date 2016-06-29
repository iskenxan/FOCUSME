using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using MahApps.Metro.Controls;

namespace FocusMe.View
{
    /// <summary>
    /// Interaction logic for Greeting.xaml
    /// </summary>
    public partial class Greeting : Window
    {
        public Greeting()
        {
            InitializeComponent();
            loadedStoryBoard.Completed+=Storyboard_Completed;
            
        }

        void Storyboard_Completed(object sender, EventArgs e)
        {
            //Once the window has been loaded the first textbox animates
            DoubleAnimation textAnimation = (DoubleAnimation)TryFindResource("fadeInAnimation");
            textAnimation.Completed += textAnimation_Completed;
            welcomeTextBox.BeginAnimation(TextBox.OpacityProperty, textAnimation);
        }

        void textAnimation_Completed(object sender, EventArgs e)
        {
            //second textbox Animates
            DoubleAnimation textAnimation = (DoubleAnimation)TryFindResource("fadeInAnimation");
            textAnimation.Completed += textAnimation2_Completed;
            secondTextBox.BeginAnimation(TextBox.OpacityProperty, textAnimation);
        }

        private void textAnimation2_Completed(object sender, EventArgs e)
        {
            //Window fades out
            DoubleAnimation fadeOut = TryFindResource("fadeOutAnimation") as DoubleAnimation;
            fadeOut.Completed += fadeOut_Completed;
            mainGrid.BeginAnimation(Grid.OpacityProperty, fadeOut);
        }

        void fadeOut_Completed(object sender, EventArgs e)
        {
            //new main window opens once the greeting window is closed
            var mainWindow = new Configuration();
            mainWindow.Show();
            this.Close();
        }

        private void mainGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

    }
}
