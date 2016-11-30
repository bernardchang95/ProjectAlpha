using ProjectAlpha.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ProjectAlpha
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void PageLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var position = await LocationManager.GetPosition();

                RootObject myWeather =
                    await OpenWeatherMapProxy.GetWeather(
                        position.Coordinate.Latitude,
                        position.Coordinate.Longitude);

                string icon = String.Format("ms-appx:///Assets/Weather/{0}.png", myWeather.weather[0].icon);
                ResultImage.Source = new BitmapImage(new Uri(icon, UriKind.Absolute));

                TempTextBlock.Text = ((int)myWeather.main.temp).ToString() + "°C";
                LocationTextBlock.Text = myWeather.name;
                DescriptionTextBlock.Text = myWeather.weather[0].description;
            }
            catch
            { LocationTextBlock.Text = "Unable to get weather at this time."; }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var position = await LocationManager.GetPosition();

                RootObject myWeather =
                    await OpenWeatherMapProxy.GetWeather(
                        position.Coordinate.Latitude,
                        position.Coordinate.Longitude);

                string icon = String.Format("ms-appx:///Assets/Weather/{0}.png", myWeather.weather[0].icon);
                ResultImage.Source = new BitmapImage(new Uri(icon, UriKind.Absolute));

                TempTextBlock.Text = ((int)myWeather.main.temp).ToString() + "°C";
                LocationTextBlock.Text = myWeather.name;
                DescriptionTextBlock.Text = myWeather.weather[0].description;
            }
            catch
            { LocationTextBlock.Text = "Unable to get weather at this time."; }
        }
    }
}
