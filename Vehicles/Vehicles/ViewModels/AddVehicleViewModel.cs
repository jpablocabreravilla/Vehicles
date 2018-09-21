namespace Vehicles.ViewModels
{
	using System;
	using System.Linq;
	using System.Windows.Input;
	using GalaSoft.MvvmLight.Command;
	using Vehicles.Helpers;
	using Vehicles.Models;
	using Vehicles.Services;
	using Plugin.Media;
	using Plugin.Media.Abstractions;
	using Xamarin.Forms;
	using Vehicles.Services;

	public class AddVehicleViewModel : BaseViewModel
    {
		#region Attributes
		private ApiService apiService;

		private bool isRunning;

		private bool isEnabled;
		#endregion


		#region Properties
		public string Brand { get; set; }

		public string Type { get; set; }

		public string Owner { get; set; }

		public string Model { get; set; }

		public string Mileage { get; set; }

		public string Price { get; set; }

		public string Specifications { get; set; }

		public bool IsRunning
		{
			get { return this.isRunning; }
			set { this.SetValue(ref this.isRunning, value); }
		}

		public bool IsEnabled
		{
			get { return this.isEnabled; }
			set { this.SetValue(ref this.isEnabled, value); }
		}

		#endregion


		#region Construcors

		public AddVehicleViewModel()
		{
			this.apiService = new ApiService();
			this.IsEnabled = true;
		}

		#endregion


		#region Commands

		public ICommand SaveCommand
		{
			get
			{
				return new RelayCommand(Save);
			}
		}

		private async void Save()
		{
			if (string.IsNullOrEmpty(this.Brand))
			{
				await Application.Current.MainPage.DisplayAlert(
					Languages.Error,
					Languages.BrandError,
					Languages.Accept);
					return;
			}


			if (string.IsNullOrEmpty(this.Type))
			{
				await Application.Current.MainPage.DisplayAlert(
					Languages.Error,
					Languages.TypeError, 
					Languages.Accept);
				return;
			}


			if (string.IsNullOrEmpty(this.Owner))
			{
				await Application.Current.MainPage.DisplayAlert(
					Languages.Error,
					Languages.OwnerError,
					Languages.Accept);
				return;
			}


			if (string.IsNullOrEmpty(this.Model))
			{
				await Application.Current.MainPage.DisplayAlert(
					Languages.Error,
					Languages.ModelError,
					Languages.Accept);
				return;
			}
			var model = int.Parse(this.Model);
			if (model < 0)
			{
				await Application.Current.MainPage.DisplayAlert(
					Languages.Error,
					Languages.NegativeNumbers,
					Languages.Accept);
				return;
			}


			if (string.IsNullOrEmpty(this.Mileage))
			{
				await Application.Current.MainPage.DisplayAlert(
					Languages.Error,
					Languages.MileageError,
					Languages.Accept);
				return;
			}
			var mileaje = int.Parse(this.Mileage);
			if (mileaje < 0)
			{
				await Application.Current.MainPage.DisplayAlert(
					Languages.Error,
					Languages.NegativeNumbers,
					Languages.Accept);
				return;
			}


			if (string.IsNullOrEmpty(this.Price))
			{
				await Application.Current.MainPage.DisplayAlert(
					Languages.Error,
					Languages.PriceError,
					Languages.Accept);
				return;
			}
			var price = decimal.Parse(this.Price);
			if (price < 0)
			{
				await Application.Current.MainPage.DisplayAlert(
					Languages.Error,
					Languages.NegativeNumbers,
					Languages.Accept);
				return;
			}


			if (string.IsNullOrEmpty(this.Specifications))
			{
				await Application.Current.MainPage.DisplayAlert(Languages.Error,
					Languages.SpecificationsError,
					Languages.Accept);
				return;
			}

			this.isRunning = true;
			this.IsEnabled = false;

			var connection = await this.apiService.CheckConnection();
			if (!connection.IsSuccess)
			{
				this.isRunning = false;
				this.IsEnabled = true;
				await Application.Current.MainPage.DisplayAlert(
					Languages.Error,
					connection.Message,
					Languages.Accept);
				return;
			}

			var vehicle = new Vehicle
			{
				Brand = this.Brand,
				Type = this.Type,
				Owner = this.Owner,
				Model = model,
				Mileage = mileaje,
				Price = price,
				Specifications = this.Specifications,
			};

			var url = Application.Current.Resources["UrlAPI"].ToString();
			var prefix = Application.Current.Resources["UrlPrefix"].ToString();
			var controller = Application.Current.Resources["UrlVehiclesController"].ToString();
			var response = await this.apiService.Post(url, prefix, controller, vehicle);

			if (!response.IsSuccess)
			{
				this.isRunning = false;
				this.IsEnabled = true;
				await Application.Current.MainPage.DisplayAlert(
					Languages.Error,
					response.Message,
					Languages.Accept);
				return;
			}

			this.isRunning = false;
			this.IsEnabled = true;
			await Application.Current.MainPage.Navigation.PopAsync();

		}

		#endregion

	}
}
