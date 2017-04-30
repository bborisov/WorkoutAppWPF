using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WorkoutsProject
{
    /// <summary>
    /// Interaction logic for CurrentWorkout.xaml
    /// </summary>
    public partial class CurrentWorkout : Page
    {
        private const int EXERCISE_LIMIT = 7;

        private int exerciseCounter;
        private int timeCounter;
        private DispatcherTimer dispatcherTimer;
        private Random random;

        public CurrentWorkout()
        {
            InitializeComponent();

            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Start();

            random = new Random();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            timeCounter--;

            if (timeCounter < 1)
            {
                if (exerciseCounter > EXERCISE_LIMIT - 1)
                {
                    dispatcherTimer.Stop();
                    this.NavigationService.Navigate(new FinalWindow());
                }

                timeCounter = 30;
                exerciseCounter++;
                changeExerciseImage();
            }

           setTimeToLabel();
        }

        private void changeExerciseImage()
        {
            string packUri = "pack://application:,,,/WorkoutsProject;component/Resources/pic" + random.Next(1, 14) + ".jpg";

            try
            {
                ImageBox.Source = new ImageSourceConverter().ConvertFromString(packUri) as ImageSource;
            }
            catch (FileFormatException fnfe)
            {
                Console.WriteLine("Exception caught: {0}", fnfe);
            }
        }

        private void setTimeToLabel()
        {
            if (timeCounter >= 10)
            {
                TimeLabel.Content = "00:" + timeCounter.ToString();
            }
            else
            {
                TimeLabel.Content = "00:0" + timeCounter.ToString();
            }
        }
    }
}
