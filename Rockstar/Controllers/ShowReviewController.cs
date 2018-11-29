using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Logic;
using Rockstar.ViewModels.Review;
using Models;
using Newtonsoft.Json;

namespace Rockstar.Controllers

{
    public class ShowReviewController : Controller
    {
		ShowReviewLogic ShowReviewLogic = new ShowReviewLogic();
        CompanyLogic CL = new CompanyLogic();
        

		public IActionResult SearchResults()
		{
			


			return View();
		}

		[HttpPost]
		public IActionResult SearchResults(SearchReviewViewModel viewModel)
        {
			if (viewModel.Rating > 0 && viewModel.Rating < 6)
			{
				viewModel.SearchResults = ShowReviewLogic.SearchReviewsByRating(viewModel.Rating, viewModel.SearchWord);
			}
			else
			{
				viewModel.SearchResults = ShowReviewLogic.SearchReviews(viewModel.SearchWord);
			}

			
			
			

			return View(viewModel);
		}
        [HttpPost]
        public JsonResult AutoComplete(string Prefix)
        {
            List<Company> allseries = CL.GetAllCompanies().Where(x => x.Name.Contains(Prefix)).Select(x => new Company
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
            return Json(allseries);


        }

        public JsonResult AutoCompleteLocation(string Prefix)
        {
            List<Company> allseries = CL.GetAllCompanies().Where(x => x.Location.Contains(Prefix)).Select(x => new Company
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
            return Json(allseries);


        }



    }


}
