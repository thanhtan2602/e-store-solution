using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Store.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.ApiService.Services.Interfaces
{
    public interface IAuthenService
    {
        Task<IActionResult> ConfirmEmail(string token, string email);
        Task<IdentityResult> SignUpAsync(UserDTO user);
        Task<string> SignInAsync(SignInDTO signInDTO);
    }
}
