// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Security.Cryptography.X509Certificates;

namespace AgencyPro.Identity.API.Services.Certificate
{
    public interface ICertificateService
    {
        X509Certificate2 GetCertificateFromKeyVault(string vaultCertificateName);
    }
}