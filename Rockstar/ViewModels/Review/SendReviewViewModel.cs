using System;
using System.Collections.Generic;
using Models;

namespace Rockstar.ViewModels.Review
{
    public class SendReviewViewModel
    {
        public Models.Review Review { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public List<Company> Companies { get; set; }
        public List<Models.Account> Accounts { get; set; }
        public string MailReason { get; set; }
        public string Function { get; set; }
        public DateTime BeginMaand { get; set; }
        public DateTime EindMaand { get; set; } 

        public SendReviewViewModel()
        {
            Companies = new List<Company>();
        }
    }
}
