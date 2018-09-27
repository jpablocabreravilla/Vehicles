namespace Vehicles.ViewModels
{
	using System.Linq;
	using System.Windows.Input;
	using GalaSoft.MvvmLight.Command;
	using Vehicles.Helpers;
	using Vehicles.Models;
	using Vehicles.Services;
	using Vehicles.Views;
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
		public ICommand EditVehicleCommand
		{
			get
			{
				return new RelayCommand(EditVehicle);
			}
		}

		private async  void EditVehicle()
		{
			MainViewModel.GetInstance().EditVehicle = new EditVehicleViewModel(this);
			await Application.Current.MainPage.Navigation.PushAsync(new EditVehiclePage());
		}

		public ICommand DeleteVehicleCommand
		{
			get
			{
				return new RelayCommand(DeleteVehicle);
			}
		}

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
			var deletedVehicle = vehiclesViewModel.MyVehicles.Where(p => p.VehicleId == this.VehicleId).FirstOrDefault();
			if (deletedVehicle != null)
			{
				vehiclesViewModel.MyVehicles.Remove(deletedVehicle);
			}
			vehiclesViewModel.RefreshLIst();
		}

		#endregion

	}

}
