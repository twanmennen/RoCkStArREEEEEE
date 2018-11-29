using System;
using System.Collections.Generic;
using System.Text;
using Data.Context;
using Models;

namespace Logic
{
	public class ShowReviewLogic
	{
		ShowReviewContext _showReviewContext = new ShowReviewContext();
        CompanyContext _companyContext = new CompanyContext();

	    public ShowReviewLogic()
	    {
	        _showReviewContext = new ShowReviewContext();
            _companyContext = new CompanyContext();
	    }

        public List<Review> SearchReviews(string search)
		{
			return _showReviewContext.SearchReviews(search);
		}

		public List<Review> SearchReviewsByRating(int rating, string searchword)
		{
			return _showReviewContext.SearchReviewsByRating(rating, searchword);
		}

		public List<Review> GetInvitesOfUser(int userId)
	    {
	        var invitesOfUser = _showReviewContext.GetInvitesOfUser(userId);
	        foreach (var review in invitesOfUser)
	        {
	            review.Company = _companyContext.GetCompanyById(review.CompanyId);
	        }

	        return invitesOfUser;
	    }

        public int CountInvitesOfUser(int userId)
	    {
	        return _showReviewContext.CountInvitesOfUser(userId);
	    }
    }
}
