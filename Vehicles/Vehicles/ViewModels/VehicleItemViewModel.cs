namespace Vehicles.ViewModels
{
	using GalaSoft.MvvmLight.Command;
	using System;
	using System.Linq;
	using System.Windows.Input;
	using Vehicles.Helpers;
	using Vehicles.Models;
	using Vehicles.Services;
	using Xamarin.Forms;

	public class VehicleItemViewModel : Vehicle
    {
		#region Atributes

		private ApiService apiService;

		#endregion

		#region Constructors

		public VehicleItemViewModel()
		{
			this.apiService = new ApiService();
		}
		#endregion

		#region Commands

		public ICommand DeleteVehicleCommand
		{
			get
			{
				return new RelayCommand(DeleteVehicle);
			}
		}

		#endregion

		#region Metods

		
		private async void DeleteVehicle()
		{
			var answer = await Application.Current.MainPage.DisplayAlert(
				Languages.Confirm,
				Languages.DeleteConfirmation,
				Languages.Yes,
				Languages.No);

			if (!answer)
			{
				return;
			}

			var connection = await this.apiService.CheckConnection();
			if (!connection.IsSuccess)
			{
				await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
				return;
			}

			var url = Application.Current.Resources["UrlAPI"].ToString();
			var prefix = Application.Current.Resources["UrlPrefix"].ToString();
			var controller = Application.Current.Resources["UrlVehiclesController"].ToString();
			var response = await this.apiService.Delete(url, prefix, controller, this.VehicleId);
			if (!response.IsSuccess)
			{
				await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
				return;
			}

			var vehiclesViewModel = VehiclesViewModel.GetInstance();
			var deletedVehicle = vehiclesViewModel.Vehicles.Where(p => p.VehicleId == this.VehicleId).FirstOrDefault();
			if (deletedVehicle != null)
			{
				vehiclesViewModel.Vehicles.Remove(deletedVehicle);
			}
		}

		#endregion

	}

}
