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
using System.Diagnostics;

using System.Windows.Threading;

namespace Connect4WPF
{
    /// <summary>
    /// Interaction logic for mainGame.xaml
    /// </summary>
    public partial class mainGame : Window
    {

        List<Ellipse> yellowPieces = new List<Ellipse>();
        List<Ellipse> redPieces = new List<Ellipse>();

        List<Ellipse> columnSelectors = new List<Ellipse>();

        Ellipse[,] gameBoardPieces = new Ellipse[6, 7];

        Ellipse[] winningPieces = new Ellipse[4];

        Ellipse clickedPiece;


        SolidColorBrush yellowSolidBrush = new SolidColorBrush(Color.FromArgb(255, 255, 255, 0));
        SolidColorBrush redSolidBrush = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
        SolidColorBrush transparentYellowSolidBrush = new SolidColorBrush(Color.FromArgb(50, 255, 255, 0));
        SolidColorBrush transparentRedSolidBrush = new SolidColorBrush(Color.FromArgb(50, 255, 0, 0));

        RadialGradientBrush yellowGradientBrush = new RadialGradientBrush();
        GradientStop yellowGradientStop = new GradientStop();
        GradientStop darkYellowGradientStop = new GradientStop();


        DispatcherTimer pieceFallingTimer = new DispatcherTimer();
        DispatcherTimer flashColour = new DispatcherTimer();
        DispatcherTimer resetGameFallingTimer = new DispatcherTimer();

        int[,] gameBoard = new int[6, 7];

        Point[,] piecePoints = {  {new Point(142, 294), new Point(187, 294), new Point(231, 294), new Point(277, 294), new Point(323, 294), new Point(367, 294), new Point(412, 294)},
                                  {new Point(142, 254), new Point(187, 254), new Point(231, 254), new Point(277, 254), new Point(323, 254), new Point(367, 254), new Point(412, 254)},
                                  {new Point(142, 214), new Point(187, 214), new Point(231, 214), new Point(277, 214), new Point(323, 214), new Point(367, 214), new Point(412, 214)},
                                  {new Point(142, 174), new Point(187, 174), new Point(231, 174), new Point(277, 174), new Point(323, 174), new Point(367, 174), new Point(412, 174)},
                                  {new Point(142, 134), new Point(187, 134), new Point(231, 134), new Point(277, 134), new Point(323, 134), new Point(367, 134), new Point(412, 134)},
                                  {new Point(142, 94), new Point(187, 94), new Point(231, 94), new Point(277, 94), new Point(323, 94), new Point(367, 94), new Point(412, 94)} };


        Point placePoint;

        mainMenu mainMenu;

        int column, scoreTotal;
        bool holdingPiece, isYellowTower, isWinnerYellow, greenFlash = true, isPieceOverColumn = false, isYellowPlaying = true, isPieceFalling = false;
        double sliderValue = 0;



        public mainGame(mainMenu mm)
        {
            InitializeComponent();

            mainMenu = mm;


            yellowGradientBrush.GradientOrigin = new Point(0.5, 0.5);
            yellowGradientBrush.Center = new Point(0.5, 0.5);

            yellowGradientStop.Color = Colors.Yellow;
            yellowGradientStop.Offset = 0.0;
            yellowGradientBrush.GradientStops.Add(yellowGradientStop);

            darkYellowGradientStop.Color = (Color)ColorConverter.ConvertFromString("#FF777C14");
            yellowGradientStop.Offset = 0.59;
            yellowGradientBrush.GradientStops.Add(darkYellowGradientStop);


            Canvas.SetZIndex(gameBoard_Image, 5);

            gameCanvas.Focus();

            columnSelectors.Add(Column1Selector);
            columnSelectors.Add(Column2Selector);
            columnSelectors.Add(Column3Selector);
            columnSelectors.Add(Column4Selector);
            columnSelectors.Add(Column5Selector);
            columnSelectors.Add(Column6Selector);
            columnSelectors.Add(Column7Selector);

            int columnCount = 0;
            foreach(Ellipse columnSelector in columnSelectors)
            {
                columnSelector.Name = "columnSelector" + columnCount.ToString();
                columnCount++;
                columnSelector.Fill = new SolidColorBrush(Color.FromArgb(50, 0, 0, 0));
            }

            pieceFallingTimer.Interval = TimeSpan.FromMilliseconds(10);
            pieceFallingTimer.Tick += fallDownwardsTick;

            flashColour.Interval = TimeSpan.FromMilliseconds(500);
            flashColour.Tick += flashColourTick;

            resetGameFallingTimer.Interval = TimeSpan.FromMilliseconds(10);
            resetGameFallingTimer.Tick += resetFallTick;

            addEllipseButtons();
        }

        private void Btn_Menu_Click(object sender, RoutedEventArgs e)
        {
            pieceReleaseSlider.Value = 7;
            pieceReleaseSlider.Value = 0;
            mainMenu.Top = this.Top;
            mainMenu.Left = this.Left;
            mainMenu.Show();
            this.Hide();
        }
        private void resetFallTick(object sender, EventArgs e)
        {
            int piecesMoved = 0;
            for (int x = 0; x < sliderValue; x++)
            {
                for (int y = 0; y < 6; y++)
                {
                    if (gameBoardPieces[y, x] != null)
                    {
                        if (Canvas.GetTop(gameBoardPieces[y, x]) <= 420)
                        {
                            Canvas.SetTop(gameBoardPieces[y, x], Canvas.GetTop(gameBoardPieces[y,x]) + 5);
                            piecesMoved++;
                        }
                        else
                        {
                            gameCanvas.Children.Remove(gameBoardPieces[y, x]);
                            gameBoardPieces[y, x] = null;
                        }
                    }
                }
            }
            if (piecesMoved == 0)
            {
                foreach(Ellipse redPiece in redPieces)
                {
                    gameCanvas.Children.Remove(redPiece);
                }
                foreach(Ellipse yellowPiece in yellowPieces)
                {
                    gameCanvas.Children.Remove(yellowPiece);
                }
                isYellowPlaying = true;
                gameBoard = new int[6, 7];
                yellowPieces.Clear();
                redPieces.Clear();
                addEllipseButtons();
                resetGameFallingTimer.Stop();
                
            }
        }

        private void flashColourTick(object sender, EventArgs e)
        {
            foreach (Ellipse piece in winningPieces)
            {
                if (greenFlash)
                {
                    piece.Fill = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0));
                }
                else
                {
                    switch (isWinnerYellow)
                    {
                        case (true):
                            piece.Fill = yellowSolidBrush;
                            break;
                        case (false):
                            piece.Fill = redSolidBrush;
                            break;
                    }
                }
            }
            greenFlash = !greenFlash;
        }

      

        private void PieceReleaseSlider_LostMouseCapture(object sender, MouseEventArgs e)
        {
            pieceReleaseSlider.Value = 0;   
        }


        private void fallDownwardsTick(object sender, EventArgs e)
        {
            if(Canvas.GetTop(clickedPiece) < placePoint.Y - 2)
            {
                Canvas.SetTop(clickedPiece, Canvas.GetTop(clickedPiece) + 4);
                Canvas.SetLeft(clickedPiece, placePoint.X);
            }
            else
            {
                checkForWinner();
                clickedPiece = null;
                holdingPiece = false;
                isPieceFalling = false;
                pieceFallingTimer.Stop();
            }
        }

        private void piece_MouseMove(object sender, MouseEventArgs e)
        {

            if (holdingPiece && !isPieceFalling)
            {
                Point mousePos = e.GetPosition(gameCanvas);

                foreach(Ellipse columnSelector in columnSelectors)
                {
                    double radius = columnSelector.Height / 2;
                    Point columnSelectorCenter = new Point(Canvas.GetLeft(columnSelector) + (columnSelector.Width / 2), Canvas.GetTop(columnSelector) + (columnSelector.Height / 2));
                    double differenceX = Math.Abs(columnSelectorCenter.X - mousePos.X);
                    double differenceY = Math.Abs(columnSelectorCenter.Y - mousePos.Y);

                    if (((differenceX * differenceX) + (differenceY * differenceY)) <= radius * radius)
                    {
                        isPieceOverColumn = true;
                        column = int.Parse(columnSelector.Name.Substring(14, 1));
                        Canvas.SetLeft(clickedPiece, Canvas.GetLeft(columnSelector));
                        Canvas.SetTop(clickedPiece, Canvas.GetTop(columnSelector));
                        return;
                    }
                    isPieceOverColumn = false;
                }

                double left = mousePos.X - (clickedPiece.ActualWidth / 2);
                double top = mousePos.Y - (clickedPiece.ActualHeight / 2);

                Canvas.SetLeft(clickedPiece, left);
                Canvas.SetTop(clickedPiece, top);

            }
        }

        private void pieceClicked(object sender, MouseButtonEventArgs e)
        {
            if (holdingPiece == false)
            {
                holdingPiece = true;
                Ellipse selectedPiece = sender as Ellipse;
                isYellowTower = selectedPiece.Name.Contains("yellow");
                switch (isYellowTower)
                {
                    case (true):
                        clickedPiece = yellowPieces[0];
                        yellowPieces.RemoveAt(0);
                        break;
                    case (false):
                        clickedPiece = redPieces[0];
                        redPieces.RemoveAt(0);
                        break;
                }
                
                clickedPiece.CaptureMouse();
                clickedPiece.Height = 36;
            }
            else
            {
                if (isPieceOverColumn)
                {
                    isPieceOverColumn = false;
                    isPieceFalling = true;
                    clickedPiece.ReleaseMouseCapture();
                    Canvas.SetZIndex(clickedPiece, 0);
                    clickedPiece.MouseLeftButtonDown -= pieceClicked;

                    for(int y = 0; y <= 5; y++)
                    {
                        if(gameBoard[y, column] == 0)
                        {
                            switch (isYellowPlaying)
                            {
                                case (true):
                                    foreach(Ellipse yellowPiece in yellowPieces)
                                    {
                                        yellowPiece.Fill = transparentYellowSolidBrush;
                                        yellowPiece.MouseLeftButtonDown -= pieceClicked;
                                        yellowPiece.MouseMove -= piece_MouseMove;
                                    }
                                    foreach (Ellipse redPiece in redPieces)
                                    {
                                        redPiece.Fill = redSolidBrush;
                                        redPiece.MouseLeftButtonDown += pieceClicked;
                                        redPiece.MouseMove += piece_MouseMove;
                                    }
                                    gameBoardPieces[y, column] = clickedPiece;
                                    gameBoard[y, column] = 1;
                                    break;

                                case (false):
                                    foreach (Ellipse yellowPiece in yellowPieces)
                                    {
                                        yellowPiece.Fill = yellowSolidBrush;
                                        yellowPiece.MouseLeftButtonDown += pieceClicked;
                                        yellowPiece.MouseMove += piece_MouseMove;
                                    }
                                    foreach (Ellipse redPiece in redPieces)
                                    {
                                        redPiece.Fill = transparentRedSolidBrush;
                                        redPiece.MouseLeftButtonDown -= pieceClicked;
                                        redPiece.MouseMove -= piece_MouseMove;
                                    }
                                    gameBoard[y, column] = 10;
                                    gameBoardPieces[y, column] = clickedPiece;
                                    break;
                            }
                            isYellowPlaying = !isYellowPlaying;
                            placePoint = piecePoints[y, column];

                            if(gameBoard[5, column] != 0)
                            {
                                columnSelectors[column].Fill = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                                columnSelectors.Remove(columnSelectors[column]);
                            }
                            break;
                        }
                    }
                    
                    pieceFallingTimer.Start();
                }
            }
        }

        private void addEllipseButtons()
        {
            int yellowPiece = 1, redPiece = 1;
            for(int piece = 0; piece < 42; piece++)
            {
                Ellipse newPiece = new Ellipse();

               Canvas.SetZIndex(newPiece, 10);

                
                newPiece.Width = 36;
                newPiece.Height = 17;


                if (piece < 21)
                {
                    //Adds new yellow piece to yellow piece tower
                    newPiece.Name = "yellowPiece" + yellowPiece.ToString();
                    newPiece.Fill = yellowGradientBrush;
                    newPiece.MouseLeftButtonDown += pieceClicked;
                    newPiece.MouseMove += piece_MouseMove;

                    Canvas.SetTop(newPiece, 300 - (yellowPiece * 7));
                    Canvas.SetLeft(newPiece, 500);

                    gameCanvas.Children.Add(newPiece);

                    yellowPieces.Add(newPiece);
                    yellowPiece++;
                }
                else
                {
                    //Adds new red piece to red piece tower
                    newPiece.Name = "redPiece" + redPiece.ToString();
                    newPiece.Fill = transparentRedSolidBrush;
                    newPiece.Fill = redSolidBrush;

                    Canvas.SetTop(newPiece, 150 + (redPiece * 7));
                    Canvas.SetLeft(newPiece, 50);

                    gameCanvas.Children.Add(newPiece);

                    redPieces.Add(newPiece);
                    redPiece++;
                }
            }
        }

        private void checkForWinner()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int y = 0; y < 4; y++)
                    {
                        scoreTotal = 0;
                        for (int x = 0; x < 4; x++)
                        {
                            winningPieces[x] = gameBoardPieces[y + i, x + j];
                            scoreTotal += gameBoard[y + i, x + j];
                            switch (scoreTotal)
                            {
                                case (4):
                                    Debug.WriteLine("YELLOW WINS");
                                    isWinnerYellow = true;
                                    disableTowers();
                                    return;
                                case (40):
                                    Debug.WriteLine("RED WINS");
                                    isWinnerYellow = false;
                                    disableTowers();
                                    return;
                            }
                        }
                    }

                    for (int x = 0; x < 4; x++)
                    {
                        scoreTotal = 0;
                        for (int y = 0; y < 4; y++)
                        {
                            winningPieces[y] = gameBoardPieces[y + i, x + j];
                            scoreTotal += gameBoard[y + i, x + j];
                            switch (scoreTotal)
                            {
                                case (4):
                                    Debug.WriteLine("YELLOW WINS");
                                    isWinnerYellow = true;
                                    disableTowers();
                                    return;
                                case (40):
                                    Debug.WriteLine("RED WINS");
                                    isWinnerYellow = false ;
                                    disableTowers();
                                    return;
                            }
                        }
                    }

                    int yPos = 0;
                    scoreTotal = 0;
                    for(int x = 0; x < 4; x++)
                    {
                        winningPieces[x] = gameBoardPieces[yPos + i, x + j];
                        scoreTotal += gameBoard[yPos + i, x + j];
                        switch (scoreTotal)
                        {
                            case (4):
                                Debug.WriteLine("YELLOW WINS YPOS");
                                isWinnerYellow = true;
                                disableTowers();
                                return;
                            case (40):
                                Debug.WriteLine("RED WINS YPOS");
                                isWinnerYellow = false;
                                disableTowers();
                                return;
                        }
                        yPos++;
                    }


                    int xPos = 0;
                    scoreTotal = 0;
                    for (int y = 3; y >= 0; y--)
                    {
                        winningPieces[y] = gameBoardPieces[y + i, xPos + j];
                        scoreTotal += gameBoard[y + i, xPos + j];
                        switch (scoreTotal)
                        {
                            case (4):
                                Debug.WriteLine("YELLOW WINS XPOS");
                                isWinnerYellow = true;
                                disableTowers();
                                return;
                            case (40):
                                Debug.WriteLine("RED WINS XPOS");
                                isWinnerYellow = false;
                                disableTowers();
                                return;
                        }
                        xPos++;
                    }
                }
            }

            if(redPieces.Count == 0 && yellowPieces.Count == 0)
            {
                Debug.WriteLine("DRAW");
            }
        }

        private void disableTowers()
        {
            foreach (Ellipse yellowPiece in yellowPieces)
            {
                yellowPiece.Fill = transparentYellowSolidBrush;
                yellowPiece.MouseLeftButtonDown -= pieceClicked;
                yellowPiece.MouseMove -= piece_MouseMove;
            }
            foreach (Ellipse redPiece in redPieces)
            {
                redPiece.Fill = transparentRedSolidBrush;
                redPiece.MouseLeftButtonDown -= pieceClicked;
                redPiece.MouseMove -= piece_MouseMove;
            }
            flashColour.Start();
        }

        private void PieceReleaseSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (pieceReleaseSlider.Value > sliderValue)
            {
                sliderValue = Math.Round(pieceReleaseSlider.Value);
            }
            resetGameFallingTimer.Start();
        }

     
    }


}
          