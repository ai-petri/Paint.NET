using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Paint
{
    public class ColorViewModel : INotifyPropertyChanged
    {


        private byte r = 0;
        private byte g = 0;
        private byte b = 0;

        private double hue = 360;
        private double saturation = 0;
        private double brightness = 0;

        private Color brightColor = new Color();
        private Color selectedColor = new Color();


        public ColorViewModel(Color color)
        {
            SelectedColor = color;

            R = color.R;
            G = color.G;
            B = color.B;

            UpdateHSB();

            BrightColor = ColorFromHue(Hue);           
        }


        

        private bool RGBisUpdating;
        private bool HSBisUpdating;


        public byte R
        {
            get
            {
                return r;
            }
            set
            {
                r = value;
                RGBisUpdating = true;
                if(!HSBisUpdating) UpdateHSB();
                BrightColor = ColorFromHue(Hue);
                SelectedColor = Color.FromRgb(R, G, B);
                RGBisUpdating = false;
            }
        }

        public byte G
        {
            get
            {
                return g;
            }
            set
            {
                g = value;
                RGBisUpdating = true;
                if (!HSBisUpdating) UpdateHSB();
                BrightColor = ColorFromHue(Hue);
                SelectedColor = Color.FromRgb(R, G, B);
                RGBisUpdating = false;
            }
        }

        public byte B
        {
            get
            {
                return b;
            }
            set
            {
                b = value;
                RGBisUpdating = true;
                if (!HSBisUpdating) UpdateHSB();
                BrightColor = ColorFromHue(Hue);
                SelectedColor = Color.FromRgb(R, G, B);
                RGBisUpdating = false;
            }
        }


       

        public double Hue
        {
            get
            {
                return hue;
            }
            set
            {
                hue = value < 0.0 ? 0.0 : value > 360.0 ? 360.0 : value;
                HSBisUpdating = true;
                if (!RGBisUpdating) UpdateRGB();
                HSBisUpdating = false;
            }
        }

        public double Saturation
        {
            get
            {
                return saturation;
            }
            set
            {
                saturation = value < 0.0 ? 0.0 : value > 100.0 ? 100.0 : value;
                HSBisUpdating = true;
                if (!RGBisUpdating) UpdateRGB();
                HSBisUpdating = false;
            }
        }

        public double Brightness
        {
            get
            {
                return brightness;
            }
            set
            {
                brightness = value < 0.0 ? 0.0 : value > 100.0 ? 100.0 : value;
                HSBisUpdating = true;
                if (!RGBisUpdating) UpdateRGB();
                HSBisUpdating = false;
            }
        }


        public Color BrightColor
        {
            get
            {
                return brightColor;
            }
            set
            {
                brightColor = value;
                RaisePropertyChanged();
            }

        }

        public Color SelectedColor
        {
            get
            {
                return selectedColor;
            }
            set
            {
                selectedColor = value;
                RaisePropertyChanged();
            }

        }



        public void UpdateHSB()
        {

            hue = HueFromRGB(R, G, B);

            saturation = (Math.Max(Math.Max(R, G), B) - Math.Min(Math.Min(R, G), B)) * 100.0 / 255.0;

            brightness = Math.Max(Math.Max(R,G), B) * 100.0/255.0;

            RaisePropertyChanged("");
        }
        




        private void UpdateRGB()
        {

            BrightColor = ColorFromHue(hue);

            R = (byte)(Brightness/100.0 * (BrightColor.R * Saturation/100.0 + 255.0 * (1.0 - Saturation/100.0)));
            G = (byte)(Brightness/100.0 * (BrightColor.G * Saturation/100.0 + 255.0  * (1.0 - Saturation/100.0)));
            B = (byte)(Brightness/100.0 * (BrightColor.B * Saturation/100.0 + 255.0 * (1.0 - Saturation/100.0)));

            SelectedColor = Color.FromRgb(R, G, B);

            RaisePropertyChanged("");

        }




        private double HueFromRGB(byte R, byte G, byte B)
        {
            

            double red = R / 255.0;
            double green = G / 255.0;
            double blue = B / 255.0;

            if ((red >= green && green > blue)||(red >= green && green > blue)) // Orange
            {
                return 60 * (green - blue) / (red - blue);
            }

            if (green > red && red >= blue) // Chartreuse
            {
                return 60 * (2 - (red - blue) / (green - blue));
            }

            if (green >= blue && blue > red) // Spring Green
            {
                return 60 * (2 + (blue - red) / (green - red));
            }

            if (blue > green && green > red) // Azure
            {
                return 60 * (4 - (green - red) / (blue - red));
            }

            if (blue > red && red >= green) // Violet
            {
                return 60 * (4 + (red - green) / (blue - green));
            }

            if (red >= blue && blue > green) // Rose
            {
                return 60 * (6 - (blue - green) / (red - green));
            }

            else return 360.0;
        }



        private Color ColorFromHue(double hue)
        {
            byte red = 0;
            byte green = 0;
            byte blue = 0;

            if (hue <= 60)
            {
                red = 255;
                green = (byte)(255.0 * hue / 60.0);
                blue = 0;
            }
            else if (hue <= 120)
            {
                red = (byte)(255.0 - 255.0 * (hue - 60.0) / 60.0);
                green = 255;
                blue = 0;
            }
            else if (hue <= 180)
            {
                red = 0;
                green = 255;
                blue = (byte)(255.0 * (hue - 120.0) / 60.0);
            }
            else if (hue <= 240)
            {
                red = 0;
                green = (byte)(255.0 - 255.0 * (hue - 180.0) / 60.0);
                blue = 255;
            }
            else if (hue <= 300)
            {
                red = (byte)(255.0 * (hue - 240.0) / 60.0); ;
                green = 0;
                blue = 255;
            }
            else if (hue <= 360)
            {
                red = 255;
                green = 0;
                blue = (byte)(255.0 - 255.0 * (hue - 300.0) / 60.0); ;
            }

            return Color.FromRgb(red, green, blue);
        }



        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
