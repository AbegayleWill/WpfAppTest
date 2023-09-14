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
        double moveAmount = 10.0; // Amount to move Pac-Man
        private double pacManX;
        private double pacManY;
        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            // Get current position
            pacManX = Canvas.GetLeft(pacman);
            pacManY = Canvas.GetTop(pacman);
        }

        private void UpdatePlayerPosition()
        {
            // Update Pac-Man's position on the Canvas
            Canvas.SetLeft(pacman, pacManX);
            Canvas.SetTop(pacman, pacManY);
        }

        private bool IsCollisionWithWall()
        {
            //List<Rectangle> walls = new List<Rectangle>();

            foreach (UIElement element in MyCanvas.Children)
            {
                if (element is Rectangle && element != pacman)
                {
                    Rectangle wall = (Rectangle)element;

                    // Get the boundaries of Pac-Man and the wall
                    Rect pacManBounds = new Rect(pacManX, pacManY, pacman.Width, pacman.Height);
                    Rect wallBounds = new Rect(Canvas.GetLeft(wall), Canvas.GetTop(wall), wall.Width, wall.Height);

                    if (pacManBounds.IntersectsWith(wallBounds))
                    {
                        // Collision detected with a wall
                        return true;
                    }
                }
            }

            // No collision with walls
            return false;

        }
        private void Canvas_KeyDown(object sender, KeyEventArgs e)
        {
            double newPacManX = pacManX;
            double newPacManY = pacManY;

            switch (e.Key)
            {
                case Key.Left:
                    newPacManX -= moveAmount;
                    break;
                case Key.Right:
                    newPacManX += moveAmount;
                    break;
                case Key.Up:
                    newPacManY -= moveAmount;
                    break;
                case Key.Down:
                    newPacManY += moveAmount;
                    break;
            }
            // Check if the new position collides with a wall before updating
            if (!IsCollisionWithWall())
            {
                //pacManX = newPacManX;
                //pacManY = newPacManY;
                Console.WriteLine("Moving");
            }

            UpdatePlayerPosition();
             Console.WriteLine("Not Moving");
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

