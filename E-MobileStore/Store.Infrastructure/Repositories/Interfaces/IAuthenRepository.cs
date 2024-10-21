using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Store.Domain.Entities;
using Store.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastructure.Repositories.Interfaces
{
	public interface IAuthenRepository
	{
		Task<IActionResult> ConfirmEmail(string token, string email);
		Task<IdentityResult> SignUpAsync(UserDTO user);
		Task<string> SignInAsync(SignInDTO signIn);
		Task<ApplicationUser> GetUserByUserName(string userName);
	}
}
