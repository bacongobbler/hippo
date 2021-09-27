using System.Linq;
using System.Security.Cryptography;
using OpenSSL.PrivateKeyDecoder;
using OpenSSL.PublicKeyDecoder;

namespace Hippo.Services
{
    public class OpenSSLService : ISSLService
    {
        /// <summary>
        /// Load the public key and private key and verify that the parameters match.
        ///
        /// TODO(bacongobbler): apparently calling .Decode() only works on Windows. Will require testing.
        /// </summary>
        public bool ValidateCertificate(string pubkey, string key)
        {
            IOpenSSLPublicKeyDecoder publicKeyDecoder = new OpenSSLPublicKeyDecoder();
            RSACryptoServiceProvider publicKeyCsp = publicKeyDecoder.Decode(pubkey);
            var publicParameters = publicKeyCsp.ExportParameters(false);

            IOpenSSLPrivateKeyDecoder privateKeyDecoder = new OpenSSLPrivateKeyDecoder();
            RSACryptoServiceProvider privateKeyCsp = privateKeyDecoder.Decode(key);
            var privateParameters = privateKeyCsp.ExportParameters(false);

            if (!publicParameters.Modulus.SequenceEqual(privateParameters.Modulus)
                || !publicParameters.Exponent.SequenceEqual(privateParameters.Exponent))
            {
                return false;
            }
            return true;
        }
    }
}
