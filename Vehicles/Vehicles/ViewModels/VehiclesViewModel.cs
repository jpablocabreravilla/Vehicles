﻿namespace Vehicles.ViewModels
{
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Linq;
	using System.Windows.Input;
	using GalaSoft.MvvmLight.Command;
	using Vehicles.Helpers;
	using Vehicles.Models;
	using Vehicles.Services;
	using Xamarin.Forms;

	public class VehiclesViewModel : BaseViewModel
    {

		#region Attributes
		private string filter;

		private ApiService apiService;

		private bool isRefreshing;

		private ObservableCollection<VehicleItemViewModel> vehicles;


		#endregion

		#region Properties
		public string Filter
		{
			get { return this.filter; }
			set
			{
				this.filter = value;
				this.RefreshLIst();
			}
		}

		public List<Vehicle> MyVehicles { get; set; }

		public ObservableCollection<VehicleItemViewModel> Vehicles
		{
			get { return this.vehicles; }
			set { this.SetValue(ref this.vehicles, value); }
		}

		public bool IsRefreshing
		{
			get { return this.isRefreshing; }
			set { this.SetValue(ref this.isRefreshing, value); }
		}
		#endregion

		#region Constructor
		public VehiclesViewModel()
		{
			instance = this;
			this.apiService = new ApiService();
			this.LoadVehicles();
		}
		#endregion

		#region Singleton
		private static VehiclesViewModel instance;

		public static VehiclesViewModel GetInstance()
		{
			if (instance == null)
			{
				return new VehiclesViewModel();
			}

			return instance;
		}
		#endregion

		#region Metods
		public void RefreshLIst()
		{
			if (string.IsNullOrWhiteSpace(this.Filter))
			{
				var myListVehicleItemViewModel = this.MyVehicles.Select(p => new VehicleItemViewModel
				{
					VehicleId = p.VehicleId,
					Brand = p.Brand,
					Type = p.Type,
					Owner = p.Owner,
					Model = p.Model,
					Mileage = p.Mileage,
					Price = p.Price,
					Specifications = p.Specifications,
					ImagePath = p.ImagePath,
					IsNegotiable = p.IsNegotiable,
					ImageArray = p.ImageArray,
				});
				this.Vehicles = new ObservableCollection<VehicleItemViewModel>(
					myListVehicleItemViewModel.OrderBy(p => p.Brand));
			}
			else
			{
				var myListVehicleItemViewModel = this.MyVehicles.Select(p => new VehicleItemViewModel
				{
					VehicleId = p.VehicleId,
					Brand = p.Brand,
					Type = p.Type,
					Owner = p.Owner,
					Model = p.Model,
					Mileage = p.Mileage,
					Price = p.Price,
					Specifications = p.Specifications,
					ImagePath = p.ImagePath,
					IsNegotiable = p.IsNegotiable,
					ImageArray = p.ImageArray,
				}).Where(p => p.Brand.ToLower().Contains(this.Filter.ToLower())).ToList();

				this.Vehicles = new ObservableCollection<VehicleItemViewModel>(
					myListVehicleItemViewModel.OrderBy(p => p.Brand));

			}
		}

		#endregion

		#region Comands
		public ICommand SearchCommand
		{
			get
			{
				return new RelayCommand(RefreshLIst);
			}
		}

		public ICommand RefreshCommand
		{
			get
			{
				return new RelayCommand(LoadVehicles);
			}
		}
		
		private async void LoadVehicles()
		{
			this.IsRefreshing = true;

			var connection = await this.apiService.CheckConnection();
			if (!connection.IsSuccess)
			{
				this.IsRefreshing = false;
				await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
				return;
			}

			var url = Application.Current.Resources["UrlAPI"].ToString();
			var prefix = Application.Current.Resources["UrlPrefix"].ToString();
			var controller = Application.Current.Resources["UrlVehiclesController"].ToString();

			var response = await this.apiService.GetList<Vehicle>(url, prefix, controller);
			if (!response.IsSuccess)
			{
				this.IsRefreshing = false;
				await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
				return;
			}
			this.MyVehicles = (List<Vehicle>)response.Result;
			this.RefreshLIst();
			this.IsRefreshing = false;
		}

		#endregion

	}
}
