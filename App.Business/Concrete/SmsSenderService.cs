using App.Business.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace App.Business.Concrete
{
	public class SmsSenderService : ISmsSenderService
	{
		public void SendSms(string phoneNumber, string Code)
		{
			try
			{

				var accountSid = "AC106c22d39c7fa9a4a98304f1242481a7";
				var authToken = "601cddcc8f0f3bd8895cf4aa15281e43";
				TwilioClient.Init(accountSid, authToken);

				var messageOptions = new CreateMessageOptions(
				  new PhoneNumber("+994513081274"));
				messageOptions.MessagingServiceSid = "MG5127bb05775a2e085b43d3ad4fcc83d3";
				messageOptions.From = new PhoneNumber("+12623846550");
				messageOptions.Body = Code;


				var message = MessageResource.Create(messageOptions);
				Console.WriteLine(message.Body);
			}
			catch (Exception e)
			{
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" - - - - - - - - - Error Catched - - - - - - - - - ");
                Console.WriteLine(e.Message);
				Console.ForegroundColor= ConsoleColor.White;
            }
		}
	}
}
