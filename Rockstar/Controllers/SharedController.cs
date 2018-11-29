using Logic;
using Microsoft.AspNetCore.Mvc;
using Rockstar.ViewModels.Shared;

namespace Rockstar.Controllers
{
    public class SharedController : Controller
    {
        private ShowReviewLogic _showReviewLogic = new ShowReviewLogic();
        public IActionResult _Layout()
        {
            LayoutViewModel viewModel = new LayoutViewModel();
            //viewModel.InvitesCounter = _showReviewLogic.CountInvitesOfUser(1); //UserId van cookie
            return View(viewModel);
        }
    }
}