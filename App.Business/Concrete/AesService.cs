using App.Business.Abstract;
using System.Security.Cryptography;
using System.Text;

namespace App.Business.Concrete
{
	public class AesService : IAesService
	{
		public string Decrypt(byte[] encryptedData, byte[] key, byte[] iv)
		{
			using AesManaged aes = new AesManaged
			{
				Key = key,
				IV = iv,
				Mode = CipherMode.CBC,
				Padding = PaddingMode.PKCS7
			};

			ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

			string plaintext;

			using (var ms = new System.IO.MemoryStream(encryptedData))
			{
				using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
				{
					using (var reader = new System.IO.StreamReader(cs))
					{
						plaintext = reader.ReadToEnd();
					}
				}
			}

			return plaintext;
		}

		public byte[] Encrypt(string plainText, byte[] key, byte[] iv)
		{
			using AesManaged aes = new AesManaged
			{
				Key = key,
				IV = iv,
				Mode = CipherMode.CBC,
				Padding = PaddingMode.PKCS7
			};

			ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

			byte[] encrypted;

			using (var ms = new System.IO.MemoryStream())
			{
				using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
				{
					byte[] plaintextBytes = Encoding.UTF8.GetBytes(plainText);
					cs.Write(plaintextBytes, 0, plaintextBytes.Length);
				}

				encrypted = ms.ToArray();
			}

			return encrypted;
		}
	}
}


// EXAMPLE CODES ---------------------------------------------------------------


//public static void Main(string[] args)
//{
//    // Example usage:
//    string plaintext = "This is a secret message.";
//    byte[] key = new byte[32]; // 256-bit key (32 bytes) for AES-256
//    byte[] iv = new byte[16]; // 128-bit IV (16 bytes) for AES

//    // Generate random key and IV (for demonstration purposes only)
//    using (var rng = new RNGCryptoServiceProvider())
//    {
//        rng.GetBytes(key);
//        rng.GetBytes(iv);
//    }

//    byte[] encryptedData = AesEncryption.Encrypt(plaintext, key, iv);

//    // Display the encrypted data (in Base64-encoded string form for readability)
//    string encryptedDataString = Convert.ToBase64String(encryptedData);
//    Console.WriteLine("Encrypted data (Base64 string):");
//    Console.WriteLine(encryptedDataString);

//    // Decrypt the encrypted data
//    string decryptedText = AesEncryption.Decrypt(encryptedData, key, iv);
//    Console.WriteLine("\nDecrypted text:");
//    Console.WriteLine(decryptedText);
//}