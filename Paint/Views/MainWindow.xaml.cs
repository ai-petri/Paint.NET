using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Paint
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



        private Point position;

        private Polyline polyline;
        private Ellipse ellipse;
        private Rectangle rectangle;
        private Polygon polygon;

        private Shape selectedShape;



        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            position = e.GetPosition(canvas);

            if (PencilButton.IsChecked == true) // рисование линии
            {
                polyline = new Polyline()
                {
                    Stroke = new SolidColorBrush(((MainViewModel)DataContext).ForegroundColor),
                    StrokeThickness = ((MainViewModel)DataContext).Diameter,
                    Opacity = ((MainViewModel)DataContext).Opacity,
                    StrokeStartLineCap = PenLineCap.Round,
                    StrokeEndLineCap = PenLineCap.Round                   
                };

                polyline.MouseDown += (o, args) =>
                {
                    if (ArrowButton.IsChecked == true)
                    {
                        canvas.Children.Remove((Polyline)o);
                        canvas.Children.Add((Polyline)o);
                        selectedShape = o as Shape;
                    }
                };

                polyline.Points.Add(position);
                polyline.Points.Add(position);
                canvas.Children.Add(polyline);
            }
            
            if (EllipseButton.IsChecked == true) // рисование эллипса
            {
                ellipse = new Ellipse()
                {
                    Stroke = new SolidColorBrush(((MainViewModel)DataContext).ForegroundColor),
                    StrokeThickness = ((MainViewModel)DataContext).Diameter,
                    Fill = new SolidColorBrush(((MainViewModel)DataContext).BackgroundColor),
                    Margin = new Thickness(position.X, position.Y, 0, 0),
                    Opacity = ((MainViewModel)DataContext).Opacity,
                    Width = 1,
                    Height = 1
                };

                ellipse.MouseDown += (o, args) =>
                {
                    if (ArrowButton.IsChecked == true)
                    {
                        canvas.Children.Remove((Ellipse)o);
                        canvas.Children.Add((Ellipse)o);
                        selectedShape = o as Shape;
                    }
                                        
                };

                canvas.Children.Add(ellipse);
            }

            if (RectangleButton.IsChecked == true) // рисование прямоугольника
            {
                rectangle = new Rectangle()
                {
                    Stroke = new SolidColorBrush(((MainViewModel)DataContext).ForegroundColor),
                    StrokeThickness = ((MainViewModel)DataContext).Diameter,
                    Fill = new SolidColorBrush(((MainViewModel)DataContext).BackgroundColor),
                    Margin = new Thickness(position.X, position.Y, 0, 0),
                    Opacity = ((MainViewModel)DataContext).Opacity,
                    RadiusX = ((MainViewModel)DataContext).Radius,
                    RadiusY = ((MainViewModel)DataContext).Radius,
                    Width = 1,
                    Height = 1
                };

                rectangle.MouseDown += (o, args) =>
                {
                    if (ArrowButton.IsChecked == true)
                    {
                        canvas.Children.Remove((Rectangle)o);
                        canvas.Children.Add((Rectangle)o);
                        selectedShape = o as Shape;
                    }

                };

                canvas.Children.Add(rectangle);
            }

            if (TriangleButton.IsChecked == true) // рисование треугольника
            {
                Point[] points = { position, position, position };
                polygon = new Polygon()
                {
                    Stroke = new SolidColorBrush(((MainViewModel)DataContext).ForegroundColor),
                    StrokeThickness = ((MainViewModel)DataContext).Diameter,
                    Fill = new SolidColorBrush(((MainViewModel)DataContext).BackgroundColor),
                    Opacity = ((MainViewModel)DataContext).Opacity,                   
                    Points = new PointCollection(points)                    
                };

                polygon.MouseDown += (o, args) =>
                {
                    if (ArrowButton.IsChecked == true)
                    {
                        canvas.Children.Remove((Polygon)o);
                        canvas.Children.Add((Polygon)o);
                        selectedShape = o as Shape;
                    }

                };

                canvas.Children.Add(polygon);
            }
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {

                if (ArrowButton.IsChecked == true && selectedShape != null)
                {
                    double dx = e.GetPosition(canvas).X - position.X;
                    double dy = e.GetPosition(canvas).Y - position.Y;
                    selectedShape.Margin = new Thickness(selectedShape.Margin.Left + dx, selectedShape.Margin.Top + dy, 0, 0);
                    position = e.GetPosition(canvas);
                }
                

                if (PencilButton.IsChecked == true && polyline != null)
                {
                    position = e.GetPosition(canvas);
                    polyline.Points.Add(position);
                }
                
                if (EllipseButton.IsChecked == true && ellipse != null)
                {

                    ellipse.Margin = new Thickness (Math.Min(position.X, e.GetPosition(canvas).X), Math.Min(position.Y, e.GetPosition(canvas).Y), 0, 0);

                    ellipse.Width = Math.Abs(e.GetPosition(canvas).X - position.X);
                    ellipse.Height = Math.Abs(e.GetPosition(canvas).Y - position.Y);
                }

                if (RectangleButton.IsChecked == true && rectangle != null)
                {
                    rectangle.Margin = new Thickness(Math.Min(position.X, e.GetPosition(canvas).X), Math.Min(position.Y, e.GetPosition(canvas).Y), 0, 0);

                    rectangle.Width = Math.Abs(e.GetPosition(canvas).X - position.X);
                    rectangle.Height = Math.Abs(e.GetPosition(canvas).Y - position.Y);
                }

                if (TriangleButton.IsChecked == true && polygon != null)
                {
                   
                    polygon.Points[0] = new Point(position.X, e.GetPosition(canvas).Y);
                    polygon.Points[1] = new Point((position.X + e.GetPosition(canvas).X)/2, position.Y);
                    polygon.Points[2] = e.GetPosition(canvas);
                }
            }
        }

        private void ColorBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ColorViewModel colorViewModel = new ColorViewModel(((MainViewModel)DataContext).ForegroundColor);
            ColorDialog dialog = new ColorDialog()
            {
                DataContext = colorViewModel,
                Owner = this
            };

            dialog.ShowDialog();

            if (dialog.DialogResult == true)
            {
                ((MainViewModel)DataContext).ForegroundColor = colorViewModel.SelectedColor;
            }
           
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            polyline = null;
            ellipse  = null;
            rectangle  = null;
            polygon = null;

            selectedShape = null;
        }

        private void BackgroundColorBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ColorViewModel colorViewModel = new ColorViewModel(((MainViewModel)DataContext).BackgroundColor);
            ColorDialog dialog = new ColorDialog()
            {
                DataContext = colorViewModel,
                Owner = this
            };

            dialog.ShowDialog();

            if (dialog.DialogResult == true)
            {
                ((MainViewModel)DataContext).BackgroundColor = colorViewModel.SelectedColor;
            }
        }

        private void ColorReset_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ((MainViewModel)DataContext).BackgroundColor = Colors.White;
            ((MainViewModel)DataContext).ForegroundColor = Colors.Black;
        }

        private void ReverseColor_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Color temp = ((MainViewModel)DataContext).BackgroundColor;
            ((MainViewModel)DataContext).BackgroundColor = ((MainViewModel)DataContext).ForegroundColor;
            ((MainViewModel)DataContext).ForegroundColor = temp;
        }

        private void ArrowButton_Checked(object sender, RoutedEventArgs e)
        {
            canvas.Cursor = Cursors.Arrow;
        }
       
        private void ArrowButton_Unchecked(object sender, RoutedEventArgs e)
        {
            canvas.Cursor = Cursors.Cross;
        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {

            SaveFileDialog dialog = new SaveFileDialog
            {
                FileName = "image.jpg",
                Filter = "JPEG ( *.jpg )|*.jpg|BMP ( *.bmp )|*.bmp|TIFF ( *.tif )|*.tif"
            };
           
            if(dialog.ShowDialog(this) == true)
            {
                RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap((int)canvas.ActualWidth, (int)canvas.ActualHeight, 96, 96, PixelFormats.Default);
                renderTargetBitmap.Render(canvas);

                BitmapEncoder encoder = 
                    dialog.FilterIndex == 0 ? (BitmapEncoder) new JpegBitmapEncoder { QualityLevel = 100 } :
                    dialog.FilterIndex == 1 ? (BitmapEncoder) new BmpBitmapEncoder () :
                                              (BitmapEncoder) new TiffBitmapEncoder { Compression = TiffCompressOption.None }; 
                
                encoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));
                using (FileStream fs = File.Create(dialog.FileName))
                {
                    encoder.Save(fs);
                }
            }
        }
    }
}
