using System.Collections.Generic;
using System.Text;

namespace Vehicles.Services
{
	using Newtonsoft.Json;
	using System;
	using System.Net.Http;
	using System.Threading.Tasks;
	using Vehicles.Models;

	public class ApiServices
    {

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


    }


}
