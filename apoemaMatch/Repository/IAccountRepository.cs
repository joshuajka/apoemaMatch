﻿using apoemaMatch.Data.ViewModels;
using apoemaMatch.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;


namespace apoemaMatch.Repository
{
    public interface IAccountRepository
    {
        Task<ApplicationUser> GetUserByEmailAsync(string email);

        //Task<IdentityResult> CreateUserAsync(SignUpUserModel userModel);

        //Task<SignInResult> PasswordSignInAsync(SignInModel signInModel);

        //Task SignOutAsync();

        Task<IdentityResult> ChangePasswordAsync(MudarSenhaViewModel model);

        Task<IdentityResult> ConfirmEmailAsync(string uid, string token);

        Task GenerateEmailConfirmationTokenAsync(ApplicationUser user);

        Task GenerateForgotPasswordTokenAsync(ApplicationUser user);

        Task<IdentityResult> ResetPasswordAsync(ResetPasswordModel model);
    }
}
