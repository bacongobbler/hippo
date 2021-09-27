using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Hippo.Models
{
    /// <summary>
    /// A x.509 certificate.
    /// </summary>
    public class Certificate : BaseEntity
    {

        /// <summary>
        /// The account owner of the certificate.
        /// </summary>
        [Required]
        public virtual Account Owner { get; set; }

        /// <summary>
        /// The x.509 certificate's public key.
        /// There is no upper limit on the size of an x.509 public key.
        /// </summary>
        [Required]
        [Remote(action: "ValidateCertificate", controller: "Certificate", AdditionalFields = nameof(PrivateKey))]
        public string PublicKey { get; set; }

        /// <summary>
        /// The certificate's private key.
        /// There is no upper limit on the size of an x.509 private key.
        /// </summary>
        [Required]
        [Remote(action: "ValidateCertificate", controller: "Certificate", AdditionalFields = nameof(PublicKey))]
        public string PrivateKey { get; set; }
    }
}
