using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PaintApp
{
    public partial class MainWindow : Window
    {
        Point lastPoint;
        Line currentLine;
        bool isDrawing;
        double brushSize = 2;
        Brush brushColor = Brushes.Black;
        string drawingMode = "Free Draw";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnClearButtonClick(object sender, RoutedEventArgs e)
        {
            DrawingCanvas.Children.Clear();
        }

        private void OnBrushSizeChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BrushSizeComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                switch (selectedItem.Content.ToString())
                {
                    case "2":
                        brushSize = 2;
                        break;
                    case "4":
                        brushSize = 4;
                        break;
                    case "6":
                        brushSize = 6;
                        break;
                    case "8":
                        brushSize = 8;
                        break;
                    case "10":
                        brushSize = 10;
                        break;
                    default:
                        break;
                }
            }
        }

        private void OnBrushColorChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BrushColorComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                switch (selectedItem.Content.ToString())
                {
                    case "Red":
                        brushColor = Brushes.Red;
                        break;
                    case "Blue":
                        brushColor = Brushes.Blue;
                        break;
                    case "Yellow":
                        brushColor = Brushes.Yellow;
                        break;
                    case "Green":
                        brushColor = Brushes.Green;
                        break;
                    case "Black":
                        brushColor = Brushes.Black;
                        break;
                    default:
                        break;
                }
            }
        }

        private void OnDrawingModeChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DrawingModeComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                drawingMode = selectedItem.Content.ToString();
            }
        }

        private void OnCanvasMouseDown(object sender, MouseButtonEventArgs e)
        {
            isDrawing = true;
            if (e.ButtonState == MouseButtonState.Pressed)
                lastPoint = e.GetPosition(DrawingCanvas);

            if (drawingMode == "Straight Line")
            {
                currentLine = new Line
                {
                    Stroke = brushColor,
                    StrokeThickness = brushSize,
                    X1 = lastPoint.X,
                    Y1 = lastPoint.Y,
                    X2 = lastPoint.X, 
                    Y2 = lastPoint.Y
                };
                DrawingCanvas.Children.Add(currentLine);
            }
        }

        private void OnCanvasMouseMove(object sender, MouseEventArgs e)
        {
            if (!isDrawing) return;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (drawingMode == "Free Draw")
                {
                   
                    Line line = new Line
                    {
                        Stroke = brushColor,
                        StrokeThickness = brushSize,
                        X1 = lastPoint.X,
                        Y1 = lastPoint.Y,
                        X2 = e.GetPosition(DrawingCanvas).X,
                        Y2 = e.GetPosition(DrawingCanvas).Y
                    };

                    lastPoint = e.GetPosition(DrawingCanvas);

                    DrawingCanvas.Children.Add(line);
                }
                else if (drawingMode == "Straight Line")
                {
                    if (currentLine != null)
                    {
                        Point currentPoint = e.GetPosition(DrawingCanvas);
                        currentLine.X2 = currentPoint.X;
                        currentLine.Y2 = currentPoint.Y;
                    }
                }
            }
        }

        private void OnCanvasMouseUp(object sender, MouseButtonEventArgs e)
        {
            isDrawing = false;
            if (drawingMode == "Straight Line")
            {
                
                currentLine = null;              }
        }
    }
}
