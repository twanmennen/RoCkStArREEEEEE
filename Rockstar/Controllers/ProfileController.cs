using System;
using System.Linq;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Rockstar.ViewModels.Account;

namespace Rockstar.Controllers
{
    public class ProfileController : Controller
    {
        private AccountLogic _accountLogic = new AccountLogic();
        private CompanyLogic _companyLogic = new CompanyLogic();

        public IActionResult GetUserProfile()
        {
            UserProfileViewModel viewModel = new UserProfileViewModel();
            
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value);
            int roleId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "RoleId")?.Value);

            switch (roleId)
            {
                //admin
                case 1:
                    viewModel.Account = _accountLogic.GetAccountById(userId);
                    break;
                //agent
                case 2:
                    viewModel.Account = _accountLogic.GetAccountById(userId);
                    break;
                //rockstar
                case 3:
                    viewModel.Account = _accountLogic.GetAccountById(userId);
                    //viewModel.Reviews = _accountLogic.GetReviewsByUser(userId);
                    break;
            }

            return View("ProfileUser",viewModel);
        }

        public IActionResult GetCompanyProfile(int id)
        {
            CompanyProfileViewModel viewModel = new CompanyProfileViewModel();

            viewModel.Company = _companyLogic.GetCompanyById(id);
            //viewModel.Reviews = _companyLogic.GetReviewsByCompany(id);

            return View("ProfileCompany", viewModel);
        }
    }
}