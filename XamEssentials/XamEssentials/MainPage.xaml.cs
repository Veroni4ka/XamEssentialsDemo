using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace XamEssentials
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

        protected override void OnAppearing()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            base.OnAppearing();
        }

        private async Task MapButton_ClickedAsync(object sender, EventArgs e)
        {
            var map = new MapsClass();
            string addressStr = address.Text, cityStr = city.Text, stateStr = state.Text, countryStr = country.Text;
            var current = Connectivity.NetworkAccess;


            if (current != NetworkAccess.Internet)
            {
                await DisplayAlert("Error", "Internet Connection is not available", "OK");
                return;
            }
            else
            {
                await map.NavigateTo(addressStr, cityStr, stateStr, countryStr);

            }
        }

        private async void GeolocationInfo (object sender, EventArgs e)
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {
                    await DisplayAlert("Geolocation", "Latitude: " + location.Latitude + ", Longitude: " + location.Longitude + ", Altitude: " + location.Altitude, "Ok");
                    //Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                await DisplayAlert("Permission exception", "Please, check your permissions", "Ok");
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
        }

        private async void BatteryInfo(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BatteryInfo());
        }
    }


    public class MapsClass
    {
        public async Task NavigateTo(string address, string city, string state, string country)
        {
            var placemark = new Placemark
            {
                CountryName = country,
                AdminArea = state,
                Thoroughfare = address,
                Locality = city
            };
            var options = new MapLaunchOptions { Name = "Default location", NavigationMode = NavigationMode.Driving };

            await Map.OpenAsync(placemark, options);
        }
    }
}
