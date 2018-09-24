using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicles.Helpers
{
	using Xamarin.Forms;
	using Interfaces;
	using Resources;
	public static class Languages
	{
		static Languages()
		{
			var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
			Resource.Culture = ci;
			DependencyService.Get<ILocalize>().SetLocale(ci);
		}

		public static string Accept
		{
			get { return Resource.Accept; }
		}

		public static string Error
		{
			get { return Resource.Error; }
		}

		public static string NoInternet
		{
			get { return Resource.NoInternet; }
		}

		public static string Vehicles
		{
			get { return Resource.Vehicles; }
		}

		public static string TurnOnInternet
		{
			get { return Resource.TurnOnInternet; }
		}
		
		public static string AddVehicle
		{
			get { return Resource.AddVehicle; }
		}

		public static string Brand
		{
			get { return Resource.Brand; }
		}
		public static string BrandPlaceholder
		{
			get { return Resource.BrandPlaceholder; }
		}

		public static string Type
		{
			get { return Resource.Type; }
		}
		public static string TypePlaceholder
		{
			get { return Resource.TypePlaceholder; }
		}

		public static string Owner
		{
			get { return Resource.Owner; }
		}
		public static string OwnerPlaceholder
		{
			get { return Resource.OwnerPlaceholder; }
		}

		public static string Model
		{
			get { return Resource.Model; }
		}
		public static string ModelPlaceholder
		{
			get { return Resource.ModelPlaceholder; }
		}

		public static string Mileage
		{
			get { return Resource.Mileage; }
		}
		public static string MileagePlaceholder
		{
			get { return Resource.MileagePlaceholder; }
		}

		public static string Price
		{
			get { return Resource.Price; }
		}
		public static string PricePlaceholder
		{
			get { return Resource.PricePlaceholder; }
		}

		public static string Specifications
		{
			get { return Resource.Specifications; }
		}
		public static string SpecificationsPlaceholder
		{
			get { return Resource.SpecificationsPlaceholder; }
		}

		public static string IsNegotiable
		{
			get { return Resource.IsNegotiable; }
		}
		

		public static string Save
		{
			get { return Resource.Save; }
		}

		public static string ChangeImage
		{
			get { return Resource.Save; }
		}

		public static string BrandError
		{
			get { return Resource.BrandError; }
		}

		public static string TypeError
		{
			get { return Resource.TypeError; }
		}

		public static string OwnerError
		{
			get { return Resource.OwnerError; }
		}

		public static string MileageError
		{
			get { return Resource.MileageError; }
		}

		public static string PriceError
		{
			get { return Resource.PriceError; }
		}

		public static string SpecificationsError
		{
			get { return Resource.SpecificationsError; }
		}

		public static string ModelError
		{
			get { return Resource.ModelError; }
		}

		public static string NegativeNumbers
		{
			get { return Resource.NegativeNumbers; }
		}

		public static string ImageSource
		{
			get { return Resource.ImageSource; }
		}

		public static string FromGallery
		{
			get { return Resource.FromGallery; }
		}

		public static string NewPicture
		{
			get { return Resource.NewPicture; }
		}

		public static string Cancel
		{
			get { return Resource.Cancel; }
		}

		public static string Edit
		{
			get { return Resource.Edit; }
		}
		public static string Delete
		{
			get { return Resource.Delete; }
		}
		public static string DeleteConfirmation
		{
			get { return Resource.DeleteConfirmation; }
		}
		public static string Yes
		{
			get { return Resource.Yes; }
		}
		public static string No
		{
			get { return Resource.No; }
		}
		public  static string Confirm
		{
			get { return Resource.Confirm; }
		}

		public static string EditVehicle
		{
			get { return Resource.EditVehicle; }
		}

		public static string Search
		{
			get { return Resource.Search; }
		}
	}
}
