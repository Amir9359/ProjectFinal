using System.Threading.Tasks;
using Admin.Endpoint.ViewModels.Account;
using Application.BasketService;
using Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.Endpoint.Pages.Account
{
    [Authorize]
    public class RegisterModel : PageModel
    {
 
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RegisterModel(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        [BindProperty]
        public RegisterViewModel Model { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var Operator = new User()
            {
                FullName = Model.FullName,
                Email = Model.Email,
                PhoneNumber = Model.PhoneNumber,
                UserName = Model.Email
            };

            var userRes = _userManager.CreateAsync(Operator, Model.Password);
            if (userRes.Result.Succeeded)
            {
                IdentityResult addRoleUserRes = new IdentityResult();

                var role = _roleManager.FindByNameAsync("Admin");
                if (role.Result == null)
                {
                    var result = _roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });
                    if (result.Result.Succeeded)
                    {
                        addRoleUserRes = _userManager.AddToRoleAsync(Operator, "Admin").Result;
                        if (addRoleUserRes.Succeeded)
                        {
                            return RedirectToPage("/Index");
                        }
                    }
                }
                
                addRoleUserRes = _userManager.AddToRoleAsync(Operator, role?.Result?.Name ).Result;
   
                return RedirectToPage("/Index");
             


            }
          

            return Page();

        }
    }
}
