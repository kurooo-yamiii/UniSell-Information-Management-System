using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebUniform.Interface;
using WebUniform.ViewModel;
using WebUniform.Models;

namespace WebUniform.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _accountRepository;
        public AccountController(IUserRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public IActionResult Login()
        {
            var loginview = new LoginViewModel();
            return View(loginview);
        }

        public ActionResult Logout()
        {
            HttpContext.Session.SetString("IsAuthenticated", "false");

            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (!ModelState.IsValid) return View(login);

            var user = await _accountRepository.GetUserByEmail(login.Username);

            if (user != null)
            {
                var isPasswordValid = await _accountRepository.VerifyPassword(login.Username, login.Password);
                if (isPasswordValid)
                {
                    
                    HttpContext.Session.SetString("IsAuthenticated", "true");
                    HttpContext.Session.SetString("UserId", user.Id.ToString());
                    return RedirectToAction("Index", "Home");

                }

                TempData["Error"] = "Wrong Credentials. Please, Try Again";
                return View(login);
            }

            TempData["Error"] = "Wrong Credentials. Please, Try Again";
            return View(login);
        }
        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }

        [HttpPost]

        public async Task<IActionResult> Register(RegisterViewModel registerVM)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = string.Join("; ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return View(registerVM);
            }
            var user = await _accountRepository.GetUserByEmail(registerVM.Username);

            if (user != null)
            {
                TempData["Error"] = "This Email Address is Already in Use";
                return View(registerVM);
            }

            if (registerVM.Password != registerVM.ConfirmPassword)
            {
                TempData["Error"] = "Password and Confirm Password Doesn't Match";
                return View(registerVM);
            }
            else
            {
                var newUser = new User
                {
                    Username = registerVM.Username,
                    Password = registerVM.Password,
                    Status = "Active",
                    Name = registerVM.Name,
                    Department = registerVM.Department,
                    Contact = registerVM.Contact
                };
                _accountRepository.Add(newUser);
                TempData["Success"] = "You have successfully registered!";
                return RedirectToAction("Index", "Slack");
            }

        }

        public async Task<IActionResult> Edit()
        {
            var curUser = HttpContext.Session.GetString("UserId");
            int.TryParse(curUser, out int userId);

            var user = await _accountRepository.GetUserById(userId);
            if (user == null) return View("Error");

            var userVM = new UserEditViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Password = user.Password,
                Contact = user.Contact,
                Department = user.Department,
            };

            return View(userVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserEditViewModel userVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to Edit User Information");
                return View("Edit", userVM);
            }

            var curUser = HttpContext.Session.GetString("UserId");
            int.TryParse(curUser, out int userId);
            var user = await _accountRepository.GetUserById(userId);

            if (user == null)
            {
                ModelState.AddModelError("", "Current User is not Exisitng");
                return View(userVM);
            }

            user.Name = userVM.Name;
            user.Password = userVM.Password;
            user.Contact = userVM.Contact;
            user.Department = userVM.Department;

            _accountRepository.Update(user);
            return RedirectToAction("Index", "Home");
        }
    }
}
