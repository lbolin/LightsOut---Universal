using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LightsOut___Universal
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private LightsOutGame game;

        public MainPage()
        {
            this.InitializeComponent();

            // Cache the page so it doesn't create a new page when navigated back
            this.NavigationCacheMode = NavigationCacheMode.Enabled;

            game = new LightsOutGame();
            CreateGrid();
            DrawGrid();            
        }              

        private void CreateGrid()
        {
            // Remove all previously-existing rectangles
            boardCanvas.Children.Clear();
            
            int rectSize = (int)boardCanvas.Width / game.GridSize;

            SolidColorBrush black = new SolidColorBrush(Windows.UI.Colors.Black);
            SolidColorBrush white = new SolidColorBrush(Windows.UI.Colors.White);

            // Turn entire grid on and create rectangles to represent it
            for (int r = 0; r < game.GridSize; r++)
            {
                for (int c = 0; c < game.GridSize; c++)
                {
                    Rectangle rect = new Rectangle();
                    rect.Fill = white;
                    rect.Width = rectSize + 1;
                    rect.Height = rect.Width + 1;
                    rect.Stroke = black;

                    // Store each row and col as a Point
                    rect.Tag = new Point(r, c);

                    // All rectangles have same callback
                    rect.Tapped += RectTapped;

                    int x = c * rectSize;
                    int y = r * rectSize;

                    Canvas.SetTop(rect, y);
                    Canvas.SetLeft(rect, x);

                    // Add the new rectangle to the canvas' children
                    boardCanvas.Children.Add(rect);
                }
            }
        }        

        private void DrawGrid()
        {
            int index = 0;

            SolidColorBrush black = new SolidColorBrush(Windows.UI.Colors.Black);
            SolidColorBrush white = new SolidColorBrush(Windows.UI.Colors.White);

            // Set colors of each rectangle based on grid values
            for (int r = 0; r < game.GridSize; r++)
                for (int c = 0; c < game.GridSize; c++)
                {
                    Rectangle rect = boardCanvas.Children[index] as Rectangle;
                    index++;

                    if (game.GetGridValue(r, c))
                    {
                        // On
                        rect.Fill = white;
                        rect.Stroke = black;
                    }
                    else
                    {
                        // Off
                        rect.Fill = black;
                        rect.Stroke = white;
                    }
                }
        }

        private async void RectTapped(object sender, TappedRoutedEventArgs e)
        {
            Rectangle rect = sender as Rectangle;
            var move = (Point)rect.Tag;
            game.Move((int)move.X, (int)move.Y);

            // Redraw the board
            DrawGrid();

            if (game.IsGameOver())
            {
                MessageDialog msgDialog = new MessageDialog("Congratulations!  You've won!", "Lights Out!");

                // Add an OK button
                msgDialog.Commands.Add(new UICommand("OK"));

                // Show the message box and wait aynchrously for a button press
                IUICommand command = await msgDialog.ShowAsync();

                // This executes *after* the OK button was pressed
                game.NewGame();
                DrawGrid();
            }
        }

        private void newGameButton_Click(object sender, RoutedEventArgs e)
        {
            game.NewGame();
            DrawGrid();
        }

        private void aboutGameButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AboutPage));
        }    
    }
}
