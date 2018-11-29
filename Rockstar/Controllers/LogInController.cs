using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Logic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Models;
using Rockstar.ViewModels.Account;
using Rockstar.ViewModels.Register;

namespace Rockstar.Controllers
{
    public class LogInController : Controller
    {
        private LogInLogic _logInLogic = new LogInLogic();
        private RegisterLogic _registerLogic = new RegisterLogic();

        public IActionResult Index()
        {
            return View("Login");
        }

        [HttpPost]
        public IActionResult Index(LogInViewModel viewModel)
        {
            if(viewModel.EMail != null && viewModel.PassWord != null)
            {
                bool[] logInArray = _logInLogic.LoginCheck(viewModel.EMail, viewModel.PassWord);
                if (logInArray[0])
                {
                    Account account = _logInLogic.GetAccountByEMail(viewModel.EMail);
                    LogUserIn(account);
                    if (logInArray[1])
                    {
                        return RedirectToAction("FirstTimeLogIn");
                    }

                    return RedirectToAction("Index", "Home");
                }
            }
            
            return View("Login", viewModel);
        }

        private void LogUserIn(Account account)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("Id", account.Id.ToString()),
                new Claim(ClaimTypes.Name, account.Name),
                new Claim(ClaimTypes.Email, account.Email),
                new Claim("RoleId", account.RoleId.ToString()),
            };

            var claimsIdentity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity)).Wait();
        }

        public IActionResult FirstTimeLogIn()
        {
            int roleId = Convert.ToInt32(User.Claims.Where(claim => claim.Type == "RoleId").Select(claim => claim.Value).SingleOrDefault());
            FirstTimeLogInViewModel viewModel = new FirstTimeLogInViewModel(roleId);
            return View("FirstTimeLogIn", viewModel);
        }

        [HttpPost]
        public IActionResult FirstTimeLogIn(FirstTimeLogInViewModel viewModel)
        {
            int userId = Convert.ToInt32(User.Claims.Where(claim => claim.Type == "Id").Select(claim => claim.Value).SingleOrDefault());
            Account dataForUser = new Account { Id = userId, Phone = viewModel.Phone, Location = viewModel.Location, Gender = viewModel.Gender, RoleId = viewModel.RoleId };
            _registerLogic.FirstTimeLogIn(dataForUser);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult a()
        {
            return View();
        }
    }
}