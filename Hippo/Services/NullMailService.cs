namespace Hippo.Services
{
    public class NullMailService: IMailService
    {
        public void SendMessage(string to, string subject, string body)
        {
        }
    }
}