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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Canvas_KeyDown(object sender, KeyEventArgs e)
        {
            double moveAmount = 10.0; // Amount to move Pac-Man



            // Get current position
            double currentLeft = Canvas.GetLeft(pacman);
            double currentTop = Canvas.GetTop(pacman);



            switch (e.Key)
            {
                case Key.Up:
                    Canvas.SetTop(pacman, currentTop - moveAmount);
                    break;
                case Key.Down:
                    Canvas.SetTop(pacman, currentTop + moveAmount);
                    break;
                case Key.Left:
                    Canvas.SetLeft(pacman, currentLeft - moveAmount);
                    break;
                case Key.Right:
                    Canvas.SetLeft(pacman, currentLeft + moveAmount);
                    break;
            }
        }



        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            // Show the game canvas and hide the start button
            MyCanvas.Visibility = Visibility.Visible;
            StartButton.Visibility = Visibility.Collapsed;////

            // Focus on the canvas to allow key events
            MyCanvas.Focus();
        }


    }
}

