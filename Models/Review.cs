using System;
using System.Collections.Generic;

namespace Models
{
    public class Review
    {
        public int Id { get; set; }
		public Account Account { get; set; }
		public int UserId { get; set; }
        public string Function { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Overall { get; set; }
        public string Explanation { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public List<Category> Categories { get; set; }
    }
}
