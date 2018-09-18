using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicles.ViewModels
{
	public class MainViewModel
	{
		public VehiclesViewModel Vehicles { get; set; }

		public MainViewModel()
		{
			this.Vehicles = new VehiclesViewModel();
		}
	}
}
