

namespace Vehicles.ViewModels
{
	using System.Windows.Input;
	using GalaSoft.MvvmLight.Command;
	using Vehicles.Views;
	using Xamarin.Forms;

	public class MainViewModel
	{
		#region Properties
		public EditVehicleViewModel EditVehicle { get; set; }

		public VehiclesViewModel Vehicles { get; set; }

		public AddVehicleViewModel AddVehicle { get; set; }
		#endregion

		#region Constructor
		public MainViewModel()
		{
			instance = this;
			this.Vehicles = new VehiclesViewModel();
		}
		#endregion

		#region Singleton
		private static MainViewModel instance;

		public static MainViewModel GetInstance()
		{
			if (instance == null)
			{
				return new MainViewModel();
			}

			return instance;
		}
		#endregion

		#region Commands
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
		#endregion

	}
}
