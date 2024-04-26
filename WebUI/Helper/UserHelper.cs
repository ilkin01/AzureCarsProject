using System.Security.Claims;

namespace WebUI.Helper
{
	public static class UserHelper
	{
		public static (string, string, string) GetUserInfo(ClaimsIdentity identity)
		{
			string username = identity.FindFirst(ClaimTypes.Name)?.Value;
			string userId = identity.FindFirst("Id")?.Value;
			string phoneNumber = identity.FindFirst("PhoneNumber")?.Value;

			return (username, userId, phoneNumber);
		}
	}
}
