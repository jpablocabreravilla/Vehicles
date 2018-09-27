using Vehicles.Helpers;

namespace Vehicles.Models
{
	public class Vehicle
	{	
		public int VehicleId { get; set; }

		public string Brand { get; set; }

		public string Type { get; set; }

		public string Owner { get; set; }

		public int Model { get; set; }

		public int Mileage { get; set; }

		public decimal Price { get; set; }

		public string Specifications { get; set; }

		public string ImagePath { get; set; }

		public bool IsNegotiable { get; set; }

		public byte[] ImageArray { get; set; }
		
		public string ImageFullPath
		{
			get
			{
				if (string.IsNullOrEmpty(this.ImagePath))
				{
					return "NoVehicle";
				}

				return $"https://pratice1-2018-iiapi.azurewebsites.net/{this.ImagePath.Substring(1)}";
			}
		}

		public override string ToString()
		{
			return this.Brand;
		}

		public string StringIsNegotiable
		{
			get
			{
				if (this.IsNegotiable)
				{
					return Languages.Yes;
				}
				else
				{
					return Languages.No;
				}
			}
		}

	}
}