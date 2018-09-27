namespace Vehicles.Infraestructure
{
	using Vehicles.ViewModels;

	public class InstanceLocator
	{
		public MainViewModel Main { get; set; }

		public InstanceLocator()
		{
			this.Main = new MainViewModel();
		}
	}
}
