using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace WpfAppTest
{
    public partial class MainWindow : Window
    {
        private int score = 0; // Track the score

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

            CheckForCollisions(); // Check for collisions after moving Pac-Man
        }

        private void CheckForCollisions()
        {
            double pacmanMargin = 5; // Adjust this value as needed for Pac-Man
            double pelletMargin = 2.5; // Adjust this value as needed for Pellets

            Rect pacmanRect = new Rect(Canvas.GetLeft(pacman) + pacmanMargin, Canvas.GetTop(pacman) + pacmanMargin, pacman.Width - 2 * pacmanMargin, pacman.Height - 2 * pacmanMargin);

            foreach (UIElement child in MyCanvas.Children)
            {
                if (child is Image && (child as Image).Tag?.ToString() == "Pellet" && child.Visibility == Visibility.Visible)
                {
                    Rect pelletRect = new Rect(Canvas.GetLeft(child) + pelletMargin, Canvas.GetTop(child) + pelletMargin, (child as Image).Width - 2 * pelletMargin, (child as Image).Height - 2 * pelletMargin);

                    if (pacmanRect.IntersectsWith(pelletRect))
                    {
                        child.Visibility = Visibility.Collapsed; // Hide the pellet
                        score++; // Increase the score
                        scoreText.Content = "Score: " + score; // Update the score label
                    }
                }
            }
        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            // Show the game canvas and hide the start button
            MyCanvas.Visibility = Visibility.Visible;
            StartButton.Visibility = Visibility.Collapsed;

            // Focus on the canvas to allow key events
            MyCanvas.Focus();
        }
    }
}
