using System;
using System.Collections.Generic;
using System.Text;
using Vehicles.ViewModels;

namespace Vehicles.Infraestructure
{
	public class InstanceLocator
	{
		public MainViewModel Main { get; set; }

		public InstanceLocator()
		{
			this.Main = new MainViewModel();
		}
	}
}
