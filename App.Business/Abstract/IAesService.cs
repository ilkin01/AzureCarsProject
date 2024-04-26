using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Abstract
{
	public interface IAesService
	{
		byte[] Encrypt(string plainText, byte[] key, byte[] iv);
		string Decrypt(byte[] encryptedData, byte[] key, byte[] iv);
	}
}
