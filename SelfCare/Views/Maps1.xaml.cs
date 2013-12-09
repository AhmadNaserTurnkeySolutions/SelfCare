using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace SelfCare
{
    public partial class Maps1 : PhoneApplicationPage
    {
        public Maps1()
        {
            InitializeComponent();
            street.Visibility = Visibility.Visible;
            st.IsChecked = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (h.IsChecked == true)
            {
                hybrid.Visibility = Visibility.Visible;
                satellite.Visibility = Visibility.Collapsed;
                street.Visibility = Visibility.Collapsed;
                physical.Visibility = Visibility.Collapsed;
                wateroverlay.Visibility = Visibility.Collapsed;
            }
            else if (st.IsChecked == true)
            {
                hybrid.Visibility = Visibility.Collapsed;
                satellite.Visibility = Visibility.Collapsed;
                street.Visibility = Visibility.Visible;
                physical.Visibility = Visibility.Collapsed;
                wateroverlay.Visibility = Visibility.Collapsed;
            }
            else if (sl.IsChecked == true)
            {
                hybrid.Visibility = Visibility.Collapsed;
                satellite.Visibility = Visibility.Visible;
                street.Visibility = Visibility.Collapsed;
                physical.Visibility = Visibility.Collapsed;
                wateroverlay.Visibility = Visibility.Collapsed;
            }
            else if (p.IsChecked == true)
            {
                hybrid.Visibility = Visibility.Collapsed;
                satellite.Visibility = Visibility.Collapsed;
                street.Visibility = Visibility.Collapsed;
                physical.Visibility = Visibility.Visible;
                wateroverlay.Visibility = Visibility.Collapsed;

            }
            else
            {
                hybrid.Visibility = Visibility.Collapsed;
                satellite.Visibility = Visibility.Collapsed;
                street.Visibility = Visibility.Collapsed;
                physical.Visibility = Visibility.Collapsed;
                wateroverlay.Visibility = Visibility.Visible;
            }
        }

        private void ButtonZoomIn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            googlemap.ZoomLevel++;
        }

        private void ButtonZoomOut_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            googlemap.ZoomLevel--;
        }
    }

    public enum GoogleTileTypes
    {
        Hybrid,
        Physical,
        Street,
        Satellite,
        WaterOverlay
    }

    public class GoogleTile : Microsoft.Phone.Controls.Maps.TileSource
    {
        private int _server;
        private char _mapmode;
        private GoogleTileTypes _tiletypes;

        public GoogleTileTypes TileTypes
        {
            get { return _tiletypes; }
            set
            {
                _tiletypes = value;
                MapMode = MapModeConverter(value);
            }
        }

        public char MapMode
        {
            get { return _mapmode; }
            set { _mapmode = value; }
        }

        public int Server
        {
            get { return _server; }
            set { _server = value; }
        }

        public GoogleTile()
        {
            UriFormat = @"http://mt{0}.google.com/vt/lyrs={1}&z={2}&x={3}&y={4}";
            Server = 0;
        }

        public override Uri GetUri(int x, int y, int zoomLevel)
        {
            if (zoomLevel > 0)
            {
                var Url = string.Format(UriFormat, Server, MapMode, zoomLevel, x, y);
                return new Uri(Url);
            }
            return null;
        }

        private char MapModeConverter(GoogleTileTypes tiletype)
        {
            switch (tiletype)
            {
                case GoogleTileTypes.Hybrid:
                    {
                        return 'y';
                    }
                case GoogleTileTypes.Physical:
                    {
                        return 't';
                    }
                case GoogleTileTypes.Satellite:
                    {
                        return 's';
                    }
                case GoogleTileTypes.Street:
                    {
                        return 'm';
                    }
                case GoogleTileTypes.WaterOverlay:
                    {
                        return 'r';
                    }
            }
            return ' ';
        }
    }
}