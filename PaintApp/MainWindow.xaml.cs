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
            if (e.ButtonState == MouseButtonState.Pressed)
                lastPoint = e.GetPosition(DrawingCanvas);
        }

        private void OnCanvasMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (drawingMode == "Free Draw")
                {
                    var line = new Line
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
                    DrawingCanvas.Children.Clear();

                    var line = new Line
                    {
                        Stroke = brushColor,
                        StrokeThickness = brushSize,
                        X1 = lastPoint.X,
                        Y1 = lastPoint.Y,
                        X2 = e.GetPosition(DrawingCanvas).X,
                        Y2 = e.GetPosition(DrawingCanvas).Y
                    };

                    DrawingCanvas.Children.Add(line);
                }
            }
        }
    }
}
