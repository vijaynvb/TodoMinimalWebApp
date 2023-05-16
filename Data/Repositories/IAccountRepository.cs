using Microsoft.AspNetCore.Identity;
using TodoMinimalWebApp.ViewModels;
using TodoMinimalWebApp.Models;

namespace TodoMinimalWebApp.Data.Repositories
{
    public interface IAccountRepository
    {
        Task<bool> SignUpUserAsync(RegisterUserViewModel user);
        Task<string> SignInUserAsync(LoginUserViewModel loginUserViewModel);
    }
}
