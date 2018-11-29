using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace Rockstar.ViewModels.Review
{
	public class SearchReviewViewModel
	{
		public string SearchWord { get; set; }
		public List<Models.Review> SearchResults { get; set; } = new List<Models.Review>();
        public List<Models.Review> SearchResultsByRating { get; set; } = new List<Models.Review>();
		public int Rating { get; set; }
	
	}
}
