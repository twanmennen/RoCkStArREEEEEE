using System.Net;
using System.Net.Mail;
using Models;

namespace Logic
{
    public class MailLogic
    {
        public string Body { get; set; }
        public string InvitedRockstarEmail { get; set; }

        public void SendMailNewUser(string eMailLoggedIn, string newUserEMail, string newUserPassWord, int newUserRole)
        {
            InvitedRockstarEmail = newUserEMail;

            Body = $"je bent uitgenodigd door {eMailLoggedIn} om een";
            switch (newUserRole)
            {
                case 2:
                    Body += $"Agent";
                    break;
                case 3:
                    Body += $"Rockstar";
                    break;
                case 4:
                    Body += $"Company";
                    break;
            }
            Body += $" te worden! je kan inloggen met deze gegevens: Email: {newUserEMail} Wachtwoord: {newUserPassWord}";

            SendMail();
        }

        public void SendMailReviewInvite(Account loggedInAgent, Account invitedRockstar, Company company, string message)
        {
            InvitedRockstarEmail = invitedRockstar.Email;

            Body = $"Je bent uitgenodigd door: {loggedInAgent.Name}" + message;

            //SendMail();
        }

        private void SendMail()
        {
            string from = "Rockstarinvites@gmail.com";
            string password = "Rockstar123!";

            MailMessage msg = new MailMessage(from, InvitedRockstarEmail, "uitnodiging voor review", Body);

            SmtpClient sc = new SmtpClient("smtp.gmail.com", 587);
            sc.UseDefaultCredentials = true;
            NetworkCredential cre = new NetworkCredential(from, password);
            sc.Credentials = cre;
            sc.EnableSsl = true;
            sc.Send(msg);
        }
    }
}
