namespace WebUI.Helper
{
	public class ClientInfoFunctions
	{
		/// <summary>
		/// Istifadecinin IP adresini alır.
		/// Gets the IP address of the user.
		/// </summary>
		/// <param name="context">HttpContext obyekti.</param>
		/// <returns>Istifadecinin IP adresi.</returns>
		public string GetClientIpAddress(HttpContext context)
		{
			string ip = context.Connection.RemoteIpAddress.ToString();
			return ip;
		}

		/// <summary>
		/// IP adresine göre coğrafi yer melumatlarini alır.
		/// Retrieves geographical location information based on the IP address.
		/// </summary>
		/// <param name="ipAddress">IP adresi.</param>
		/// <returns>Coğrafi yer melumatlarini JSON formatında.</returns>
		public async Task<string> GetLocationByIpAsync(string ipAddress)
		{
			using (var client = new HttpClient())
			{
				var response = await client.GetStringAsync($"http://ip-api.com/json/{ipAddress}");
				return response;
			}
		}

		/// <summary>
		/// Istifadecinin brauzer melumatlarini alır.
		/// Gets the browser information of the user.
		/// </summary>
		/// <param name="context">HttpContext obyekti.</param>
		/// <returns>Istifadecinin brauzer melumatlari.</returns>
		public string GetBrowserInfo(HttpContext context)
		{
			string browser = context.Request.Headers["User-Agent"].ToString();
			return browser;
		}

	}
}
