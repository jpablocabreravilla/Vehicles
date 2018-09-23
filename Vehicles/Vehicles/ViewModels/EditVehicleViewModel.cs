using GalaSoft.MvvmLight.Command;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Vehicles.Helpers;
using Vehicles.Models;
using Vehicles.Services;
using Xamarin.Forms;

namespace Vehicles.ViewModels
{
	public class EditVehicleViewModel : BaseViewModel
	{
		#region Atributes
		private Vehicle vehicle;

		private MediaFile file;

		private ImageSource imageSource;

		private ApiService apiService;

		private bool isRunning;

		private bool isEnabled;
		#endregion

		#region Properties
		public Vehicle Vehicle
		{
			get { return this.vehicle; }
			set { this.SetValue(ref this.vehicle, value); }
		}

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

		public ImageSource ImageSource
		{
			get { return this.imageSource; }
			set { this.SetValue(ref this.imageSource, value); }
		}
		#endregion

		#region Constructors
		public EditVehicleViewModel(Vehicle vehicle)
		{
			this.vehicle = vehicle;
			this.apiService = new ApiService();
			this.IsEnabled = true;
			this.ImageSource = vehicle.ImageFullPath;
		}

		#endregion

		#region Commands

		public ICommand DeleteCommand
		{
			get
			{
				return new RelayCommand(Delete);
			}
		}

		private async void Delete()
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

			this.IsRunning = true;
			this.IsEnabled = false;

			var connection = await this.apiService.CheckConnection();
			if (!connection.IsSuccess)
			{
				this.IsRunning = false;
				this.IsEnabled = true;
				await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
				return;
			}

			var url = Application.Current.Resources["UrlAPI"].ToString();
			var prefix = Application.Current.Resources["UrlPrefix"].ToString();
			var controller = Application.Current.Resources["UrlVehiclesController"].ToString();
			var response = await this.apiService.Delete(url, prefix, controller, this.Vehicle.VehicleId);
			if (!response.IsSuccess)
			{
				this.IsRunning = false;
				this.IsEnabled = true;
				await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
				return;
			}

			var vehiclesViewModel = VehiclesViewModel.GetInstance();
			var deletedVehicle = vehiclesViewModel.MyVehicles.Where(p => p.VehicleId == this.Vehicle.VehicleId).FirstOrDefault();
			if (deletedVehicle != null)
			{
				vehiclesViewModel.MyVehicles.Remove(deletedVehicle);
			}
			vehiclesViewModel.RefreshLIst();

			this.IsRunning = false;
			this.IsEnabled = true;
			await Application.Current.MainPage.Navigation.PopAsync();

		}

		public ICommand ChangeImageCommand
		{
			get
			{
				return new RelayCommand(ChangeImage);
			}
		}

		private async void ChangeImage()
		{
			await CrossMedia.Current.Initialize();
			var source = await Application.Current.MainPage.DisplayActionSheet(
			Languages.ImageSource,
			Languages.Cancel,
			null,
			Languages.FromGallery,
			Languages.NewPicture);
			if (source == Languages.Cancel)
			{
				this.file = null;
				return;
			}
			if (source == Languages.NewPicture)
			{
				this.file = await CrossMedia.Current.TakePhotoAsync(
				new StoreCameraMediaOptions
				{
					Directory = "Sample",
					Name = "test.jpg",
					PhotoSize = PhotoSize.Small,
				}
				);
			}
			else
			{
				this.file = await CrossMedia.Current.PickPhotoAsync();
			}
			if (this.file != null)
			{
				this.ImageSource = ImageSource.FromStream(() =>
				{
					var stream = this.file.GetStream();
					return stream;
				});
			}
		}

		public ICommand SaveCommand
		{
			get
			{
				return new RelayCommand(Save);
			}
		}

		private async void Save()
		{	//PREGUNTAR SI TODOS LOS CAMPOS SIN OBLIGATORIOS PARA SABER SI VAN O NO ALGUNAS VALIDACIONES
			if (string.IsNullOrEmpty(this.Vehicle.Brand))
			{
				await Application.Current.MainPage.DisplayAlert(
					Languages.Error,
					Languages.BrandError,
					Languages.Accept);
				return;
			}
			
			if (string.IsNullOrEmpty(this.Vehicle.Type))
			{
				await Application.Current.MainPage.DisplayAlert(
					Languages.Error,
					Languages.TypeError,
					Languages.Accept);
				return;
			}
			
			if (string.IsNullOrEmpty(this.Vehicle.Owner))
			{
				await Application.Current.MainPage.DisplayAlert(
					Languages.Error,
					Languages.OwnerError,
					Languages.Accept);
				return;
			}

			if (this.Vehicle.Model < 0)
			{
				await Application.Current.MainPage.DisplayAlert(
					Languages.Error,
					Languages.NegativeNumbers,
					Languages.Accept);
				return;
			}
			var model = Convert.ToString(this.Vehicle.Model);
			if (string.IsNullOrEmpty(model))
			{
				await Application.Current.MainPage.DisplayAlert(
					Languages.Error,
					Languages.ModelError,
					Languages.Accept);
				return;
			}

			if (this.Vehicle.Mileage < 0)
			{
				await Application.Current.MainPage.DisplayAlert(
					Languages.Error,
					Languages.NegativeNumbers,
					Languages.Accept);
				return;
			}
			var mileage = Convert.ToString(this.Vehicle.Mileage);
			if (string.IsNullOrEmpty(mileage))
			{
				await Application.Current.MainPage.DisplayAlert(
					Languages.Error,
					Languages.MileageError,
					Languages.Accept);
				return;
			}

			if (this.Vehicle.Price < 0)
			{
				await Application.Current.MainPage.DisplayAlert(
					Languages.Error,
					Languages.NegativeNumbers,
					Languages.Accept);
				return;
			}
			var price = Convert.ToString(this.Vehicle.Price);
			if (string.IsNullOrEmpty(price))
			{
				await Application.Current.MainPage.DisplayAlert(
					Languages.Error,
					Languages.PriceError,
					Languages.Accept);
				return;
			}
			
			if (string.IsNullOrEmpty(this.Vehicle.Specifications))
			{
				await Application.Current.MainPage.DisplayAlert(Languages.Error,
					Languages.SpecificationsError,
					Languages.Accept);
				return;
			}
			//PREGUNTAR SI TODOS LOS CAMPOS SIN OBLIGATORIOS PARA SABER SI VAN O NO ALGUNAS VALIDACIONES

			this.IsRunning = true;
			this.IsEnabled = false;

			var connection = await this.apiService.CheckConnection();
			if (!connection.IsSuccess)
			{
				this.IsRunning = false;
				this.IsEnabled = true;
				await Application.Current.MainPage.DisplayAlert(
					Languages.Error,
					connection.Message,
					Languages.Accept);
				return;
			}

			byte[] imageArray = null;
			if (this.file != null)
			{
				imageArray = FilesHelper.ReadFully(this.file.GetStream());
				this.Vehicle.ImageArray = imageArray;
			}

			var url = Application.Current.Resources["UrlAPI"].ToString();
			var prefix = Application.Current.Resources["UrlPrefix"].ToString();
			var controller = Application.Current.Resources["UrlVehiclesController"].ToString();
			var response = await this.apiService.Put(url, prefix, controller, this.Vehicle, this.Vehicle.VehicleId);

			if (!response.IsSuccess)
			{
				this.IsRunning = false;
				this.IsEnabled = true;
				await Application.Current.MainPage.DisplayAlert(
					Languages.Error,
					response.Message,
					Languages.Accept);
				return;
			}

			var newVehicle = (Vehicle)response.Result;
			var vehiclesViewModel = VehiclesViewModel.GetInstance();
			var oldVehicle = vehiclesViewModel.MyVehicles.Where(p => p.VehicleId == this.Vehicle.VehicleId).FirstOrDefault();
			if (oldVehicle != null)
			{
				vehiclesViewModel.MyVehicles.Remove(oldVehicle);
			}

			vehiclesViewModel.MyVehicles.Add(newVehicle);
			vehiclesViewModel.RefreshLIst();

			this.IsRunning = false;
			this.IsEnabled = true;
			await Application.Current.MainPage.Navigation.PopAsync();
		}

		#endregion
	}
}
