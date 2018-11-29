using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using Data.Context;
using Models;

namespace Logic
{
    public class AccountLogic
    {
       private  AccountContext _accountContext = new AccountContext();
       

        public Account GetAccountById(int id)
        {
            return _accountContext.GetAccountById(id);
        }

        public List<Account> GetAllAccounts()
        {
            return _accountContext.GetAllAccounts();
        }

        public List<Account> GetAllRockstarAccounts()
        {
            var allAccounts = _accountContext.GetAllAccounts();
            var rockstarAccounts = new List<Account>();

            foreach (var account in allAccounts)
            {
                if (account.RoleId == 3)
                {
                    rockstarAccounts.Add(account);
                }
            }

            return rockstarAccounts;
        }

        public List<Review> GetReviewsByUser(int id)
        {
            return _accountContext.GetReviewsByUser(id);
        }
    }
}
