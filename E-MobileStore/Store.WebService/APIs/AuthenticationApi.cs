using Store.WebService.APIs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.WebService.APIs
{
	public class AuthenticationApi : IAuthenticationApi
	{
		public AuthenticationApi() { }
		private string baseUrl = "http://localhost:5163";
		public string SignIn()
		{
			return $"{baseUrl}/api/authen/signin";
		}
		public string SignUp()
		{
			return $"{baseUrl}/api/authen/signup";
		}

		public string GetUser(string userName)
		{
			return $"{baseUrl}/api/authen/FindUser?userName={userName}";

		}
	}
}
