using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Vehicles.Models;
using Vehicles.Services;
using Xamarin.Forms;

namespace Vehicles.ViewModels
{
    public class VehiclesViewModel : BaseViewModel
    {
		private ApiServices apiService;

		private ObservableCollection<Vehicle> vehicles;

		public ObservableCollection<Vehicle> Vehicles
		{
			get { return this.vehicles; }
			set { this.SetValue(ref this.vehicles, value);  }
		}

		public VehiclesViewModel()
		{
			this.apiService = new ApiServices();
			this.LoadProducts();
		}

		private async void LoadProducts()
		{
			var response = await this.apiService.GetList<Vehicle>("https://pratice1-2018-iiapi.azurewebsites.net/ .", "/api", "/Vehicles");
			if (!response.IsSuccess)
			{
				await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
				return;
			}

			var list = (List<Vehicle>)response.Result;
			this.Vehicles = new ObservableCollection<Vehicle>(list);

		}



    }
}
