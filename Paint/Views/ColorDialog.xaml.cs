using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Paint
{
    /// <summary>
    /// Interaction logic for ColorDialog.xaml
    /// </summary>
    public partial class ColorDialog : Window
    {
        public ColorDialog()
        {
            InitializeComponent();

        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((INotifyPropertyChanged)DataContext).PropertyChanged += (o, args) => Update();
             Update();
        }

        private void Update()
        {
            colorCircle.Margin = new Thickness(X, Y, 0, 0);

            triangles.Margin = new Thickness(0, H, 0, 0);
            
        }

  

        private double X
        {
            get
            {
                return ((ColorViewModel)DataContext).Saturation * gradient.ActualWidth / 100.0;
            }
            set
            {
                if (value >= 0.0)
                {
                    ((ColorViewModel)DataContext).Saturation = 100.0 * value / gradient.ActualWidth;
                }
                    
            }
        }
        private double Y
        {
            get
            {
                return gradient.ActualHeight * (1.0 - ((ColorViewModel)DataContext).Brightness / 100.0 ) ;
            }
            set
            {
                if (value >= 0.0)
                {
                    ((ColorViewModel)DataContext).Brightness = 100.0 - 100.0 * (value / gradient.ActualHeight);
                }            
            }
        }


        private double H
        {
            get
            {
                return (1.0 - ((ColorViewModel)DataContext).Hue / 360.0) * rainbow.ActualHeight; 
            }
            set
            {
                ((ColorViewModel)DataContext).Hue = 360.0 * (1.0 - value / rainbow.ActualHeight);
            }
        }


        
        
        



        private void Pick_Color(object sender, MouseButtonEventArgs e)
        {
            X = e.GetPosition(gradient).X;
            Y = e.GetPosition(gradient).Y;
            

            colorCircle.Margin = new Thickness(X, Y, 0, 0);
        }



        private void Gradient_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {

                X = e.GetPosition(gradient).X;
                Y = e.GetPosition(gradient).Y;                

                colorCircle.Margin = new Thickness(X, Y, 0, 0);
            }
        }

        private void Rainbow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            H = e.GetPosition(rainbow).Y;

            triangles.Margin = new Thickness(0, H, 0, 0);
                      
        }

        private void Rainbow_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                H = e.GetPosition(rainbow).Y;

                triangles.Margin = new Thickness(0, H, 0, 0);
                               
            }
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

       
    }
}
