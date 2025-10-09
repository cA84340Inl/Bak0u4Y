// 代码生成时间: 2025-10-10 02:31:25
// SslTlsCertificateManager.cs
// This class provides functionality to manage SSL/TLS certificates.
using System;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using Microsoft.AspNetCore.Components;

namespace BlazorSslTlsCertificateManagement
{
    // Define a custom exception for certificate errors.
    public class CertificateException : Exception
    {
        public CertificateException(string message) : base(message) { }
    }

    // The SslTlsCertificateManager class is responsible for managing SSL/TLS certificates.
    public class SslTlsCertificateManager
    {
        private readonly string certificatePath;

        // Constructor with path to the certificate directory.
        public SslTlsCertificateManager(string certificatePath)
        {
            if (string.IsNullOrWhiteSpace(certificatePath))
                throw new ArgumentException("Certificate path cannot be null or whitespace.", nameof(certificatePath));

            this.certificatePath = certificatePath;
        }

        // Method to load a certificate from a file.
        public X509Certificate2 LoadCertificate(string fileName)
        {
            try
            {
                string filePath = Path.Combine(certificatePath, fileName);
                if (!File.Exists(filePath))
                    throw new FileNotFoundException($"Certificate file not found: {fileName}");

                return new X509Certificate2(filePath);
            }
            catch (Exception ex)
            {
                throw new CertificateException($"Failed to load certificate: {ex.Message}");
            }
        }

        // Method to install a new certificate.
        public void InstallCertificate(string fileName, byte[] certificateData)
        {
            try
            {
                string filePath = Path.Combine(certificatePath, fileName);
                File.WriteAllBytes(filePath, certificateData);
            }
            catch (Exception ex)
            {
                throw new CertificateException($"Failed to install certificate: {ex.Message}");
            }
        }

        // Method to remove an existing certificate.
        public void RemoveCertificate(string fileName)
        {
            try
            {
                string filePath = Path.Combine(certificatePath, fileName);
                if (!File.Exists(filePath))
                    throw new FileNotFoundException($"Certificate file not found: {fileName}");

                File.Delete(filePath);
            }
            catch (Exception ex)
            {
                throw new CertificateException($"Failed to remove certificate: {ex.Message}");
            }
        }

        // Method to list all certificates in the directory.
        public string[] ListCertificates()
        {
            try
            {
                return Directory.GetFiles(certificatePath, "*.cer"); // Assuming .cer extension for certificates.
            }
            catch (Exception ex)
            {
                throw new CertificateException($"Failed to list certificates: {ex.Message}");
            }
        }
    }
}
