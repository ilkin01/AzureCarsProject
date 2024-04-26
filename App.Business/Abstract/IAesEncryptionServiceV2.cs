using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Abstract
{
	public interface IAesEncryptionServiceV2
	{
		string Encrypt(string plainText);
		string Decrypt(string cipherText);

	}
}
