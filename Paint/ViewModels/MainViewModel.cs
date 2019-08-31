using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Paint
{
    public class MainViewModel : INotifyPropertyChanged
    {

        private int diameter = 1;
        public int Diameter
        {
            get
            {
                return diameter;
            }
            set
            {
                diameter = value;
                RaisePropertyChanged();
            }
        }

        private double opacity = 1.0;
        public double Opacity
        {
            get
            {
                return opacity;
            }
            set
            {
                opacity = value;
                RaisePropertyChanged();
            }
        }

        private double radius = 0.0;
        public double Radius
        {
            get
            {
                return radius;
            }
            set
            {
                radius = value;
                RaisePropertyChanged();
            }
        }

       



        private Color foregroundColor = Colors.Black;
        private Color backgroundColor = Colors.White;

        public Color ForegroundColor
        {
            get
            {
                return foregroundColor;
            }
            set
            {
                foregroundColor = value;
                RaisePropertyChanged();
            }
        }


        public Color BackgroundColor
        {
            get
            {
                return backgroundColor;
            }
            set
            {
                backgroundColor = value;
                RaisePropertyChanged();
            }
        }

        


        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
