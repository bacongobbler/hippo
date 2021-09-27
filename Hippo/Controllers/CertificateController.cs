using Hippo.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hippo.Controllers
{
    public class CertificateController : Controller
    {
        private readonly ISSLService _sslService;

        public CertificateController(ISSLService sslService)
        {
            _sslService = sslService;
        }

        public IActionResult ValidateCertificate(string pubkey, string key)
        {
            if (!_sslService.ValidateCertificate(pubkey, key))
            {
                return Json($"certificate is invalid: public key and private key do not match.");
            }
            return Json(true);
        }
    }
}
