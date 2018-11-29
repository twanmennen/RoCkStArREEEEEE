using Data.Context;
using Models;

namespace Logic
{
    public class LogInLogic
    {
        LogInContext _logInContext = new LogInContext();

        public bool[] LoginCheck(string eMail, string passWord)
        {
            return _logInContext.LoginCheck(eMail, passWord);
        }

        public Account GetAccountByEMail(string eMail)
        {
            return _logInContext.GetAccountByEMail(eMail);
        }
    }
}
