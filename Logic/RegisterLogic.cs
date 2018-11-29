using Data.Context;
using Models;

namespace Logic
{
    public class RegisterLogic
    {
        RegisterContext registerContext = new RegisterContext();
        public void RegisterUser(string newUserName, string newUserEmail, string newUserPassWord, int newUserRole, string eMailLoggedIn)
        {
            //SendMail(eMailLoggedIn, newUserEmail, newUserPassWord, newUserRole);
            registerContext.RegisterUser(newUserName, newUserEmail, newUserPassWord, newUserRole);
        }

        public void FirstTimeLogIn(Account dataForUser)
        {
            registerContext.FirstTimeLogIn(dataForUser);
        }

    }
}
