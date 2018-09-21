﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Vehicles.Models;
using Vehicles.Services;
using Xamarin.Forms;
using GalaSoft.MvvmLight.Command;
using Vehicles.Helpers;

namespace Vehicles.ViewModels
{
    public class VehiclesViewModel : BaseViewModel
    {
		private ApiService apiService;

		private bool isRefreshing;

		private ObservableCollection<Vehicle> vehicles;

		public ObservableCollection<Vehicle> Vehicles
		{
			get { return this.vehicles; }
			set { this.SetValue(ref this.vehicles, value);  }
		}

		public bool IsRefreshing
		{
			get { return this.isRefreshing; }
			set { this.SetValue(ref this.isRefreshing, value); }
		}

		public VehiclesViewModel()
		{
			this.apiService = new ApiService();
			this.LoadVehicles();
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

			var response = await this.apiService.GetList<Vehicle>(url,prefix,controller);
			if (!response.IsSuccess)
			{
				this.IsRefreshing = false;
				await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
				return;
			}
				var list = (List<Vehicle>)response.Result;
				this.Vehicles = new ObservableCollection<Vehicle>(list);
				this.IsRefreshing = false;

		}


		public ICommand RefreshCommand
		{
			get
			{
				return new RelayCommand(LoadVehicles);
			}
		}


	}
}
