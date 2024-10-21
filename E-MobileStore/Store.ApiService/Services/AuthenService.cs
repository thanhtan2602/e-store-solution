using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Store.ApiService.Services.Interfaces;
using Store.Domain.Entities;
using Store.Infrastructure.DTOs;
using Store.Infrastructure.Repositories.Interfaces;


namespace Store.ApiService.Services
{
	public class AuthenService : IAuthenService
	{
		private readonly IAuthenRepository _authenRepository;

		public AuthenService(IAuthenRepository authenRepository)
		{
			_authenRepository = authenRepository;
		}

		public async Task<IActionResult> ConfirmEmail(string token, string email)
		{
			return await _authenRepository.ConfirmEmail(token, email);
		}

		public async Task<ApplicationUser> GetUserByUserName(string userName)
		{
			return await _authenRepository.GetUserByUserName(userName);
		}

		public async Task<string> SignInAsync(SignInDTO signInDTO)
		{
			return await _authenRepository.SignInAsync(signInDTO);
		}

		public async Task<IdentityResult> SignUpAsync(UserDTO user)
		{
			return await _authenRepository.SignUpAsync(user);
		}
	}
}
