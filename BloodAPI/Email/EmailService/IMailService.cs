namespace BloodAPI.Email.EmailService
{
    public interface IMailService
    {
        void SendEmail(MailDto request); 
    }
}
