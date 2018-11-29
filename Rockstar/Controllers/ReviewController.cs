using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rockstar.ViewModels.Review;

namespace Rockstar.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ReviewLogic _reviewLogic = new ReviewLogic();
        private readonly CompanyLogic _companyLogic = new CompanyLogic();
        private readonly AccountLogic _accountLogic = new AccountLogic();
        private readonly SendReviewLogic _sendReviewLogic = new SendReviewLogic();
        private readonly ShowReviewLogic _showReviewLogic = new ShowReviewLogic();

        public IActionResult Send()
        {
            var viewModel = new SendReviewViewModel();
            viewModel.Companies = _companyLogic.GetAllCompanies();
            viewModel.Accounts = _accountLogic.GetAllRockstarAccounts();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Send(SendReviewViewModel viewModel)
        {
            var userId = Convert.ToInt32(User.Claims.Where(claim => claim.Type == "Id").Select(claim => claim.Value).SingleOrDefault());
            _sendReviewLogic.SendReview(viewModel.Review, userId, viewModel.MailReason);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Invites()
        {
            InvitesReviewViewModel viewModel = new InvitesReviewViewModel();
            int userId = Convert.ToInt32(Convert.ToString(User.Claims.Where(claim => claim.Type == "Id").Select(claim => claim.Value).SingleOrDefault()));
            viewModel.Reviews = _showReviewLogic.GetInvitesOfUser(userId);
            viewModel.Categories = _reviewLogic.GetAllCategories();
            return View(viewModel);
        }

        //[Authorize]
        public IActionResult New(int id)
        {
            NewReviewViewModel viewModel = new NewReviewViewModel();
            var categories = _reviewLogic.GetAllCategories();
            viewModel.Categories = categories;
            viewModel.Review = _reviewLogic.GetNewReviewById(id);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult New(InvitesReviewViewModel model)
        {
            var review = model.NewReview;
            int userId = Convert.ToInt32(Convert.ToString(User.Claims.Where(claim => claim.Type == "Id").Select(claim => claim.Value).SingleOrDefault()));
            _reviewLogic.AddReview(review, userId);
            var categories = model.Categories;
            foreach (var category in categories)
            {
                _reviewLogic.AddRatingToReview(category, review.Id);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}