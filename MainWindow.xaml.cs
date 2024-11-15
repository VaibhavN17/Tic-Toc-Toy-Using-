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

namespace Mobile_Application
{
    public partial class MainWindow : Window
    {
        private char currentPlayer = 'X';
        private Button[,] buttons = new Button[3, 3];

        public MainWindow()
        {
            InitializeComponent();
            InitializeGameBoard();
        }

        private void InitializeGameBoard()
        {
            int index = 0;
            foreach (UIElement element in GameGrid.Children)
            {
                if (element is Button button)
                {
                    int row = index / 3;
                    int col = index % 3;
                    buttons[row, col] = button;
                    button.Content = string.Empty;
                    button.IsEnabled = true;
                    index++;
                }
            }
        }

        private void Cell_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            clickedButton.Content = currentPlayer;
            clickedButton.IsEnabled = false;

            if (CheckWinner())
            {
                StatusText.Text = $"Player {currentPlayer} wins!";
                DisableBoard();
                return;
            }

            if (IsDraw())
            {
                StatusText.Text = "It's a draw!";
                return;
            }

            // Switch player
            currentPlayer = currentPlayer == 'X' ? 'O' : 'X';
            StatusText.Text = $"Player {currentPlayer}'s turn";
        }

        private bool CheckWinner()
        {
            for (int i = 0; i < 3; i++)
            {
                // Check rows and columns
                if (buttons[i, 0].Content.ToString() == currentPlayer.ToString() &&
                    buttons[i, 1].Content.ToString() == currentPlayer.ToString() &&
                    buttons[i, 2].Content.ToString() == currentPlayer.ToString())
                    return true;

                if (buttons[0, i].Content.ToString() == currentPlayer.ToString() &&
                    buttons[1, i].Content.ToString() == currentPlayer.ToString() &&
                    buttons[2, i].Content.ToString() == currentPlayer.ToString())
                    return true;
            }

            // Check diagonals
            if (buttons[0, 0].Content.ToString() == currentPlayer.ToString() &&
                buttons[1, 1].Content.ToString() == currentPlayer.ToString() &&
                buttons[2, 2].Content.ToString() == currentPlayer.ToString())
                return true;

            if (buttons[0, 2].Content.ToString() == currentPlayer.ToString() &&
                buttons[1, 1].Content.ToString() == currentPlayer.ToString() &&
                buttons[2, 0].Content.ToString() == currentPlayer.ToString())
                return true;

            return false;
        }

        private bool IsDraw()
        {
            foreach (Button button in buttons)
            {
                if (string.IsNullOrEmpty(button.Content.ToString()))
                    return false;
            }
            return true;
        }

        private void DisableBoard()
        {
            foreach (Button button in buttons)
            {
                button.IsEnabled = false;
            }
        }

        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            currentPlayer = 'X';
            StatusText.Text = "Player X's turn";
            InitializeGameBoard();
        }
    }
}
