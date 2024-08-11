using System;
namespace PatientManagement.Application.Services
{
	public interface IAesEncryptionService
	{
        string Encrypt(string plainText);
        string Decrypt(string cipherText);
    }
}

