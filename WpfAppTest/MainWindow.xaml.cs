using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Media;

namespace WpfAppTest
{
    public partial class MainWindow : Window
    {
        private int score = 0; // Track the score
        private bool isGameEnded = false;
        private double pacmanOriginalLeft;
        private double pacmanOriginalTop;
        private int lives = 3; // Start with 3 lives
        MediaPlayer mediaPlayer = new MediaPlayer();


        private Random random = new Random();
        private DispatcherTimer enemyMoveTimer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;


            // Store Pac-Man's original position
            pacmanOriginalLeft = Canvas.GetLeft(pacman);
            pacmanOriginalTop = Canvas.GetTop(pacman);

            enemyMoveTimer.Interval = TimeSpan.FromMilliseconds(20); // Adjust the interval as needed
            enemyMoveTimer.Tick += EnemyMoveTimer_Tick;
            enemyMoveTimer.Start();

        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Open(new Uri("reggaemusic.mp3", UriKind.Relative));
            mediaPlayer.MediaEnded += (o, e) => mediaPlayer.Position = TimeSpan.Zero;
            mediaPlayer.Play();
        }

        private void EnemyMoveTimer_Tick(object sender, EventArgs e)
        {
            foreach (UIElement child in MyCanvas.Children)
            {
                if (child is Image && (child as Image).Tag?.ToString() == "Enemy")
                {
                    MoveEnemyRandomly(child as Image);
                }
            }
        }
        private void MoveEnemyRandomly(Image enemy)
        {
            double moveAmount = 10.0; // Amount to move the enemy
            double currentLeft = Canvas.GetLeft(enemy);
            double currentTop = Canvas.GetTop(enemy);
            double newLeft = currentLeft;
            double newTop = currentTop;

            int direction = random.Next(4); // 0: Up, 1: Down, 2: Left, 3: Right

            switch (direction)
            {
                case 0:
                    newTop -= moveAmount;
                    break;
                case 1:
                    newTop += moveAmount;
                    break;
                case 2:
                    newLeft -= moveAmount;
                    break;
                case 3:
                    newLeft += moveAmount;
                    break;
            }
            // Check for collisions with maze blocks
            Rect enemyRect = new Rect(newLeft, newTop, enemy.Width, enemy.Height);
            bool collisionDetected = false;

            foreach (UIElement child in MyCanvas.Children)
            {
                if (child is Rectangle && (child as Rectangle).Tag?.ToString() == "MazeBlock")
                {
                    Rect mazeBlockRect = new Rect(Canvas.GetLeft(child), Canvas.GetTop(child), (child as Rectangle).Width, (child as Rectangle).Height);
                    if (enemyRect.IntersectsWith(mazeBlockRect))
                    {
                        collisionDetected = true;
                        break;
                    }
                }
            }

            if (!collisionDetected)
            {
                Canvas.SetLeft(enemy, newLeft);
                Canvas.SetTop(enemy, newTop);
            }
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

        private bool AreAllPelletsCollected()
        {
            foreach (UIElement child in MyCanvas.Children)
            {
                if (child is Image && (child as Image).Tag?.ToString() == "Pellet" && child.Visibility == Visibility.Visible)
                {
                    return false; // At least one pellet is still visible
                }
            }
            return true; // All pellets are collected
        }

        private void CheckForCollisions()
        {
            double pacmanMargin = 5; // Adjust this value as needed for Pac-Man
            double pelletMargin = 2.5; // Adjust this value as needed for Pellets
            if (isGameEnded) return; // Exit the method if the game has ended

            Rect pacmanRect = new Rect(Canvas.GetLeft(pacman) + pacmanMargin, Canvas.GetTop(pacman) + pacmanMargin, pacman.Width - 2 * pacmanMargin, pacman.Height - 2 * pacmanMargin);

            // Check for collisions with pellets
            foreach (UIElement child in MyCanvas.Children)
            {
                if (child is Image && (child as Image).Tag?.ToString() == "Pellet" && child.Visibility == Visibility.Visible)
                {
                    Rect pelletRect = new Rect(Canvas.GetLeft(child) + pelletMargin, Canvas.GetTop(child) + pelletMargin, (child as Image).Width - 2 * pelletMargin, (child as Image).Height - 2 * pelletMargin);

                    if (pacmanRect.IntersectsWith(pelletRect))
                    {
                        child.Visibility = Visibility.Collapsed; // Hide the pellet
                        score++; // Increase the score
                        scoreDisplay.Text = "Score: " + score.ToString();
                    }
                }
            }

            // Check for collisions with enemies
            foreach (UIElement child in MyCanvas.Children)
            {
                if (child is Image && (child as Image).Tag?.ToString() == "Enemy")
                {
                    Rect enemyRect = new Rect(Canvas.GetLeft(child), Canvas.GetTop(child), (child as Image).Width, (child as Image).Height);
                    if (pacmanRect.IntersectsWith(enemyRect))
                    {
                        lives--; // Decrease the number of lives
                        livesCount.Text = lives.ToString(); // Update the displayed number of lives

                        // Update heart visibility based on lives left
                        switch (lives)
                        {
                            case 2:
                                heart3.Visibility = Visibility.Collapsed; // Hide the third heart
                                break;
                            case 1:
                                heart2.Visibility = Visibility.Collapsed; // Hide the second heart
                                break;
                            case 0:
                                heart1.Visibility = Visibility.Collapsed; // Hide the first heart
                                EndGame("Game Over! You were caught by an enemy.");
                                break;
                        }

                        if (lives > 0)
                        {
                            MessageBox.Show($"You were caught by an enemy! Lives remaining: {lives}");
                            Canvas.SetLeft(pacman, pacmanOriginalLeft); // Reset Pac-Man's position
                            Canvas.SetTop(pacman, pacmanOriginalTop);
                        }
                    }
                }
            }

            // Check for collisions with maze blocks
            foreach (UIElement child in MyCanvas.Children)
            {
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

            if (AreAllPelletsCollected())
            {
                EndGame("Congratulations! You collected all the pellets!");
            }
        }



        private void ResetGame()
        {
            // Reset the score
            score = 0;
            // Reset the number of lives (if you have this logic)
            lives = 3;
            livesCount.Text = lives.ToString(); // Update the displayed number of lives


            // Reset the visibility of all pellets
            foreach (UIElement child in MyCanvas.Children)
            {
                if (child is Image && (child as Image).Tag?.ToString() == "Pellet")
                {
                    child.Visibility = Visibility.Visible;
                }
            }

            // Reset Pac-Man's position to the original starting position
            Canvas.SetLeft(pacman, pacmanOriginalLeft);
            Canvas.SetTop(pacman, pacmanOriginalTop);

            // Reset the game ended flag
            isGameEnded = false;

            // Start the enemy move timer again
            enemyMoveTimer.Start();
            heart1.Text = "❤";
            heart2.Text = "❤";
            heart3.Text = "❤";

        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            // Show the game canvas and hide the start button
            MyCanvas.Visibility = Visibility.Visible;
            StartButton.Visibility = Visibility.Collapsed;

            // Focus on the canvas to allow key events
            MyCanvas.Focus();
        }
        private void EndGame(string message)
        {
            enemyMoveTimer.Stop(); // Stop the enemies from moving
            MessageBox.Show(message); // Display the message

            // Hide the game canvas and show the start button
            MyCanvas.Visibility = Visibility.Collapsed;
            StartButton.Visibility = Visibility.Visible;
            ResetGame(); // Reset the game state
        }



    }
}