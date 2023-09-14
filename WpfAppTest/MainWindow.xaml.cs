using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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

           
            double newLeft = currentLeft;
            double newTop = currentTop;


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
            e.Handled = true;

        }
        private void CharacterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedItem = (ComboBoxItem)CharacterComboBox.SelectedItem;
            string characterUrl = (string)selectedItem.Tag;

            if (!string.IsNullOrEmpty(characterUrl))
            {
                pacman.Source = new BitmapImage(new Uri(characterUrl));
            }
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
                // Check for collisions with maze blocks
                if (child is Rectangle && (child as Rectangle).Tag?.ToString() == "MazeBlock")
                {
                    Rect mazeBlockRect = new Rect(Canvas.GetLeft(child), Canvas.GetTop(child), (child as Rectangle).Width, (child as Rectangle).Height);
                    if (pacmanRect.IntersectsWith(mazeBlockRect))
                    {
                        // Show a simple popup when Pac-Man collides with a maze block
                        MessageBox.Show("Pac-Man collided with the maze!");
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
