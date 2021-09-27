namespace Hippo.Services
{
    public interface ISSLService
    {
        bool ValidateCertificate(string pubkey, string key);
    }
}
