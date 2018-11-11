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
                await map.NavigateToBuilding25(addressStr, cityStr, stateStr, countryStr);
                
            }
        }
	}

    
    public class MapsClass
    {
        public async Task NavigateToBuilding25(string address, string city, string state, string country)
        {
            var placemark = new Placemark
            {
                CountryName = country,
                AdminArea = state, 
                Thoroughfare = address,
                Locality = city 
            };
            var options = new MapsLaunchOptions { Name = "Default location", MapDirectionsMode = MapDirectionsMode.Driving };

            await Maps.OpenAsync(placemark, options);
        }
    }
}
