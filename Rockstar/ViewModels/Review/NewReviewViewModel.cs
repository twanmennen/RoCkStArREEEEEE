using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace Rockstar.ViewModels.Review
{
    public class NewReviewViewModel
    {
        public int StarsTechniques { get; set; }
        public int StarsInnovation { get; set; }
        public List<Category> Categories { get; set; }
        public Category Category { get; set; }
        public Models.Review Review { get; set; }
    }
}
