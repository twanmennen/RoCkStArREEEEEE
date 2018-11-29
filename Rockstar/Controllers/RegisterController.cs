using System;
using System.Linq;
using System.Security.Claims;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Models;
using Rockstar.ViewModels.Account;
using Rockstar.ViewModels.Register;

namespace Rockstar.Controllers
{
    public class RegisterController : Controller
    {
        private RegisterLogic registerLogic = new RegisterLogic();

        public IActionResult RegisterUser()
        {
            int roleId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "RoleId")?.Value);

            RegisterUserViewModel viewModel = new RegisterUserViewModel(roleId);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult RegisterUser(RegisterUserViewModel viewModel)
        {
            if(/*viewModel.State == valid*/ viewModel.EMail != null && viewModel.Name != null && viewModel.PassWord != null )
            {
                string eMailLoggedIn = Convert.ToString(User.Claims.Where(claim => claim.Type == ClaimTypes.Email).Select(claim => claim.Value).SingleOrDefault());

                registerLogic.RegisterUser(viewModel.Name, viewModel.EMail, viewModel.PassWord, viewModel.Role, eMailLoggedIn);
                return RedirectToAction("Index", "Home");
            }

            return View("RegisterUser", viewModel);
        }
    }
}