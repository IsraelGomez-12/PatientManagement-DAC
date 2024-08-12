using System;
namespace PatientManagement.Application.Helpers.EncryptionServices
{
	public interface IAesEncryptionService
	{
        string Encrypt(string plainText);
        string Decrypt(string cipherText);
    }
}

