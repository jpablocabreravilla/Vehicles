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

	public class AddVehicleViewModel : BaseViewModel
    {
		#region Attributes
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

			if (string.IsNullOrEmpty(this.Mileage))
			{
				await Application.Current.MainPage.DisplayAlert(
					Languages.Error,
					Languages.MileageError,
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

			var price = decimal.Parse(this.Price);
			if ( price < 0 )
			{
				await Application.Current.MainPage.DisplayAlert(
					Languages.Error,
					Languages.PriceError,
					Languages.Accept);
					return;
			}

		}

		#endregion

	}
}
