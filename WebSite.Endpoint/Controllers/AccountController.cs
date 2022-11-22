using Application.BasketService;
using Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebSite.Endpoint.Models.ViewModels.User;
using WebSite.EndPoint.Models.ViewModels.User;
using WebSite.Endpoint.Utilities.Filters;

namespace WebSite.Endpoint.Controllers
{
    [ServiceFilter(typeof(SaveVisitorFilter))]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IBasketService _basketService;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IBasketService basketService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _basketService = basketService;
        }

        // GET


        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var User = new User()
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName,
                PhoneNumber = model.PhoneNumber
            };
            var res = _userManager.CreateAsync(User, model.Password).Result;
            if (res.Succeeded)
            {
                var newUser =  _userManager.FindByNameAsync(User.Email).Result;
                TransferBasketUser(newUser.Id);
                _signInManager.SignInAsync(newUser, true).Wait();
                return RedirectToAction("Profile");
            }

            foreach (var error in res.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }

            return View(model);
        }

        public IActionResult Profile()
        {
            return View();
        }
        public IActionResult Login(string returnUrl = "/")
        {
            return View(new LoginViewModel()
            {
                ReturnUrl = returnUrl
            });
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _userManager.FindByNameAsync(model.Email).Result;
            if (user == null)
            {
                ModelState.AddModelError("", "کاربر یافت نشد !");
                return View(model);
            }

            _signInManager.SignOutAsync();
            var res = _signInManager.PasswordSignInAsync(user, model.Password , model.IsPersistent , true).Result;
            if (res.Succeeded)
            {
                TransferBasketUser(user.Id);
                return LocalRedirect(model.ReturnUrl ?? "/");
            }

            if (res.RequiresTwoFactor)
            {
                //
            }
            return View(model);
        }

        public IActionResult LogOut()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        private void TransferBasketUser(string UserId)
        {
            string BasketCookieName = "BasketId";
            if (Request.Cookies.ContainsKey(BasketCookieName))
            {
                var anonymouseBasketId = Request.Cookies[BasketCookieName];
                _basketService.transferBasket(anonymouseBasketId, UserId);
                Response.Cookies.Delete(BasketCookieName);
            }
        }
    }
}