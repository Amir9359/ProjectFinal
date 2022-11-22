using Admin.Endpoint.Controllers;
using Admin.Endpoint.ViewModels.Account;
using Application.BasketService;
using Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.Endpoint.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public LoginModel(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [BindProperty]
        public  LoginViewModel Model { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = _userManager.FindByNameAsync(Model.Email).Result;
            if (user == null)
            {
                ModelState.AddModelError("", "کاربر یافت نشد !");
                return Page();
            }

            _signInManager.SignOutAsync();
            var roleRes = _userManager.IsInRoleAsync(user, "Admin").Result;

            if (roleRes)
            { 
                var res = _signInManager.PasswordSignInAsync(user, Model.Password, Model.IsPersistent, true).Result;

                if (res.Succeeded)
                {
                    return LocalRedirect(Model.ReturnUrl ?? "/");
                }
                
                if (res.RequiresTwoFactor)
                {
                    //
                }
            }
            else
            {
                ModelState.AddModelError("Model.Email", "کاربر ادمین نیستید .");
                return Page();
            }


            return Page();
        }
    }
}
