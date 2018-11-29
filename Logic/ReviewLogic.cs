using System;
using System.Collections.Generic;
using System.Text;
using Data.Context;
using Models;

namespace Logic
{
    public class ReviewLogic
    {
        private ReviewContext _reviewContext;
        private CompanyContext _companyContext;

        public ReviewLogic()
        {
            _reviewContext = new ReviewContext();
            _companyContext = new CompanyContext();
        }

        public List<Category> GetAllCategories()
        {
            return _reviewContext.GetAllCategories();
        }

        public void AddReview(Review review, int userId)
        {
            _reviewContext.AddReview(review, userId);
        }

        public void AddRatingToReview(Category category, int reviewId)
        {
            _reviewContext.AddRatingToReview(category, reviewId);
        }

		public List<Review> GetReviews(int id)
		{
			return _reviewContext.GetReviewsById(id);
		}

        public Review GetNewReviewById(int id)
        {
            var review = _reviewContext.GetNewReviewById(id);
            review.Company = _companyContext.GetCompanyById(review.CompanyId);
            return review;
        }
    }
}
