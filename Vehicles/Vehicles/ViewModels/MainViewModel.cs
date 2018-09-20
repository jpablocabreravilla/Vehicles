using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Vehicles.Views;
using Xamarin.Forms;

namespace Vehicles.ViewModels
{
	public class MainViewModel
	{
		public VehiclesViewModel Vehicles { get; set; }

		public AddVehicleViewModel AddVehicle { get; set; }

		public MainViewModel()
		{
			this.Vehicles = new VehiclesViewModel();
		}

		public ICommand AddVehicleCommand
		{
			get
			{
				return new RelayCommand(GoToAddVehicle);
			}
		}

		private async void GoToAddVehicle()
		{
			this.AddVehicle = new AddVehicleViewModel();
			await Application.Current.MainPage.Navigation.PushAsync(new AddVehiclePage());
		}

	}
}
