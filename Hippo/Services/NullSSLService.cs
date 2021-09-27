namespace Hippo.Services
{
    public class NullSSLService : ISSLService
    {
        public bool ValidateCertificate(string pubkey, string key)
        {
            return true;
        }
    }
}
