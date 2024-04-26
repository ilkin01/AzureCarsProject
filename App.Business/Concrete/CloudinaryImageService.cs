using App.Business.Abstract;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio.Types;
using Twilio;

namespace App.Business.Concrete
{
	public class CloudinaryImageService : ICloudinaryImageService
	{
		
		public string AddToCloduinary(string imageurl)
		{
			try
			{
				Account account = new Account(
				"dtstnopzb",
				"186443664986689",
				"P32cU-SO784uycCw6L34ST_Zl8k");

				Cloudinary cloudinary = new Cloudinary(account);
				cloudinary.Api.Secure = true;

				var uploadParams = new ImageUploadParams()
				{
					File = new FileDescription(imageurl),
					PublicId = Guid.NewGuid().ToString(),
				};
				var uploadResult = cloudinary.Upload(uploadParams);
				return uploadResult.Url.ToString();
			}
			catch (Exception e)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine(" - - - - - - - - - Error Catched - - - - - - - - - ");
				Console.WriteLine(e.Message);
				Console.ForegroundColor = ConsoleColor.White;
				return null;
			}
		}

	}
}
