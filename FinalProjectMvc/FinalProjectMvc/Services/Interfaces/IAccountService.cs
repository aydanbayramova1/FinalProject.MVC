using FinalProjectMvc.ViewModels.UI;
using FinalProjectMvc.ViewModels.UI.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace FinalProjectMvc.Services.Interfaces
{
    public interface IAccountService
    {
        Task<IdentityResult> RegisterAsync(RegisterVM request);
        Task<SignInResult> LoginAsync(LoginVM request);
        Task ConfirmEmailAsync(string userId, string token);
        Task<bool> ForgotPasswordAsync(ForgotPasswordVM model);
        Task<IdentityResult> ResetPasswordAsync(ResetPasswordVM model);
        Task LogoutAsync();
    }
}
