namespace Vehicles.Services
{
	using System.Collections.Generic;
	using System.Net.Http;
	using System.Threading.Tasks;
	using Newtonsoft.Json;
	using Plugin.Connectivity;
	using System;
	using Vehicles.Helpers;
	using Vehicles.Models;
	using Xamarin.Forms;
	using System.Text;

	public class ApiService
    {
		
		public async Task<Response> CheckConnection()
		{
			if (!CrossConnectivity.Current.IsConnected)
			{
				return new Response
				{
					IsSuccess = false,
					Message = Languages.TurnOnInternet,
				};
			}

			//var urlGoogle = Application.Current.Resources["UrlGoogle"].ToString();  --ERROR INEXPLICABLE , PREGUNTAR A ZULU

			var isReachable = await CrossConnectivity.Current.IsRemoteReachable("google.com");
			if (!isReachable)
			{
				return new Response
				{
					IsSuccess = false,
					Message = Languages.NoInternet,
				};
			}
			return new Response
			{
				IsSuccess = true,
			};
		}

		public async Task<Response> GetList<T>(string urlBase, string Prefix, string Controller)
		{
			try
			{
				var client = new HttpClient();
				client.BaseAddress = new Uri(urlBase);
				var url = $"{Prefix}{Controller}";
				var response = await client.GetAsync(url);
				var answer = await response.Content.ReadAsStringAsync();
				if (!response.IsSuccessStatusCode)
				{
					return new Response
					{
						IsSuccess = false,
						Message = answer,
					};
				}
				var list = JsonConvert.DeserializeObject<List<T>>(answer); ;
				return new Response
				{
					IsSuccess = true,
					Result = list,
				};
			}
			catch (Exception ex)
			{
				return new Response
				{
					IsSuccess = false,
					Message = ex.Message,
				};
			}

		}

		public async Task<Response> Post<T>(string urlBase, string Prefix, string Controller, T model)
		{
			try
			{
				var request = JsonConvert.SerializeObject(model);
				var content = new StringContent(request, Encoding.UTF8, "application/json");
				var client = new HttpClient();
				client.BaseAddress = new Uri(urlBase);
				var url = $"{Prefix}{Controller}";
				var response = await client.PostAsync(url,content);
				var answer = await response.Content.ReadAsStringAsync();
				if (!response.IsSuccessStatusCode)
				{
					return new Response
					{
						IsSuccess = false,
						Message = answer,
					};
				}
				var obj = JsonConvert.DeserializeObject<T>(answer); ;
				return new Response
				{
					IsSuccess = true,
					Result = obj,
				};
			}
			catch (Exception ex)
			{
				return new Response
				{
					IsSuccess = false,
					Message = ex.Message,
				};
			}
		}

		public async Task<Response> Delete(string urlBase, string Prefix, string Controller, int id)
		{
			try
			{
				var client = new HttpClient();
				client.BaseAddress = new Uri(urlBase);
				var url = $"{Prefix}{Controller}/{id}";
				var response = await client.DeleteAsync(url);
				var answer = await response.Content.ReadAsStringAsync();
				if (!response.IsSuccessStatusCode)
				{
					return new Response
					{
						IsSuccess = false,
						Message = answer,
					};
				}
				return new Response
				{
					IsSuccess = true,
				};
			}
			catch (Exception ex)
			{
				return new Response
				{
					IsSuccess = false,
					Message = ex.Message,
				};
			}

		}

	}
}
