using Data.Context;
using Models;

namespace Logic
{
    public class SendReviewLogic
    {
        private SendReviewContext _sendReviewContext;
        public MailLogic MailLogic { get; set; } = new MailLogic();
        public AccountLogic AccountLogic { get; set; } = new AccountLogic();


        public SendReviewLogic()
        {
            _sendReviewContext = new SendReviewContext();
        }

        public void SendReview(Review review, int loggedInUserId, string mailReason)
        {
            AccountLogic AC = new AccountLogic();
            Account loggedInAgent = AC.GetAccountById(loggedInUserId);
            Account invitedRockstar = AccountLogic.GetAccountById(review.UserId);
            MailLogic.SendMailReviewInvite(loggedInAgent, invitedRockstar, review.Company, mailReason);

            _sendReviewContext.SendReview(review);
        }
    }
}
