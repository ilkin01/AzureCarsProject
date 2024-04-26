using App.Business.Abstract;
using App.Business.Concrete;
using App.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using WebUI.Dtos;
using WebUI.Helper;
using WebUI.Models;

namespace WebUI.Controllers
{
	public class AccountController : Controller
	{
		private IUserService _userService;
		private ILoginService _loginService;
		private ISmsSenderService _smsSenderService;
		private INotificationService _notificationService;
		private ICodeGeneratorService _codeGeneratorService;
		private IHttpContextAccessor _contextAccessor;
		private static string SmsCode = "";
		public static User CurrentUser = null;
		private static string phoneNumber = "";
		private IMainService _IMainService;
		private IAesEncryptionServiceV2 _aesEncryptionServiceV2;
		public static string test = "";
		private HelperFunctions _helperFunctions = new HelperFunctions();
		public static string lastAction = "";
		public static string lastController = "";
		public AccountController(IUserService userService, ILoginService loginService = null, ISmsSenderService smsSenderService = null, ICodeGeneratorService codeGeneratorService = null, IMainService iMainService = null, INotificationService notificationService = null, IAesEncryptionServiceV2 aesEncryptionServiceV2 = null, IHttpContextAccessor contextAccessor = null)
		{
			_userService = userService;
			_loginService = loginService;
			_smsSenderService = smsSenderService;
			_codeGeneratorService = codeGeneratorService;
			_IMainService = iMainService;
			_notificationService = notificationService;
			_aesEncryptionServiceV2 = aesEncryptionServiceV2;
			_contextAccessor = contextAccessor;

			//// FOR TEST 
			//CurrentUser =_userService.GetAll().FirstOrDefault(u=>u.Id==4);

		}

		private void InitializeBrandModels(Dictionary<string, List<string>> brandModels, List<Main> mains)
		{
			mains = mains.OrderBy((m) => m.Vendor).ToList();
			foreach (var main in mains)
			{
				if (!brandModels.ContainsKey(main.Vendor))
				{
					brandModels[main.Vendor] = new List<string>();
				}
				brandModels[main.Vendor].Add(main.Model);
			}
		}
		public IActionResult LogOut()
		{
			CurrentUser = null;
			return new RedirectResult("/Car/Cars");
		}

		/// <summary>
		/// Ola Bilsin silindi lazimsiz funksiya
		/// </summary>
		/// <returns></returns>
		public IActionResult GetAllPosts()
		{
			// Retrieve posts and convert to JSON
			var posts = AccountController.CurrentUser.Posts;
			ViewBag.Posts = posts;

			return Json(posts);
		}

		public IActionResult Profile()
		{
			if (CurrentUser != null)
			{

				JsonSerializerSettings jss = new JsonSerializerSettings();
				jss.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
				List<Main> mains = _IMainService.GetAll().ToList();
				Dictionary<string, List<string>> brandModels = new Dictionary<string, List<string>>();
				InitializeBrandModels(brandModels, mains);
				ViewBag.BrandModels = Newtonsoft.Json.JsonConvert.SerializeObject(brandModels);
				var notification = _notificationService.GetAll().ToList().FirstOrDefault(f => f.UserId == CurrentUser.Id);
				ViewBag.Notification = notification;
				string json = JsonConvert.SerializeObject(CurrentUser, jss);
				ViewBag.PhoneNumber = test;
				var posts = AccountController.CurrentUser.Posts;
				var postsString = JsonConvert.SerializeObject(posts, jss);
				ViewBag.Posts = posts;
				ViewBag.CurrentUser = CurrentUser;
				return View();
			}
			else
			{
				return Login("", "", "");
			}
		}

		[HttpPost]
		public void ChangeUserName(string newName)
		{
			if (CurrentUser != null && newName != null)
			{
				CurrentUser.Fullname = newName;
				_userService.Update(CurrentUser);
			}
		}

		public IActionResult Login(string Data, string actionName, string controllerName)
		{
			if (!string.IsNullOrEmpty(actionName) && !string.IsNullOrEmpty(controllerName))
			{

				lastAction = actionName;
				lastController = controllerName;
			}
			if (AccountController.CurrentUser != null)
			{
				return RedirectToAction("Profile", "Account");
			}
			ViewBag.Data = Data;
			return View();
		}



		public IActionResult Register()
		{
			return View();
		}
		public IActionResult SMS()
		{
			return View();
		}


		public IActionResult CurrentCars()
		{

			return View();
		}


		public IActionResult YourProfile()
		{

			return View();
		}

		[HttpPost]
		public IActionResult SMS(SmsViewModel pnVM)
		{

			if (pnVM != null)
			{
				if (SmsCode == pnVM.GetSms())
				{
					Redirecter(phoneNumber);
					test = phoneNumber;
					if (!string.IsNullOrEmpty(lastAction) && !string.IsNullOrEmpty(lastController))
					{
						var lastActionTemp = lastAction;
						var lastControllerTemp = lastController;
						lastAction = "";
						lastController = "";
						return RedirectToAction(lastActionTemp, lastControllerTemp);
					}
					return RedirectToAction("Cars", "Car");
				}
			}
			return View();
		}


		private readonly string _secretKey = "BezKraskaIsSecretKey";
		private readonly int _expirationMinutes = 120;
		public string GenerateJwtToken(string username, int id, string phoneNumber)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.UTF8.GetBytes(_secretKey);

			var claims = new List<Claim>
			{
				new (JwtRegisteredClaimNames.Name, username),
				new ("Id", id.ToString()),
				new ("PhoneNumber", phoneNumber)
			 };

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.UtcNow.AddMinutes(_expirationMinutes),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};


			var token = tokenHandler.CreateToken(tokenDescriptor);


			return tokenHandler.WriteToken(token);
		}
		private IActionResult Redirecter(string phoneNumber)
		{
			if (!string.IsNullOrEmpty(phoneNumber))
			{
				//This Is New Code
				var encryptedPhoneNumber = _aesEncryptionServiceV2.Encrypt(phoneNumber);
				var result = _userService.Exists(encryptedPhoneNumber);
				//This Is old
				//var result = _userService.Exists(phoneNumber);
				if (result == null)
				{
					Register(phoneNumber);
				}
				else
				{
					var token = GenerateJwtToken(result.Fullname, result.Id, phoneNumber);
					Response.Cookies.Append("token", token);
					CurrentUser = result;
					return Ok(new { Token = token });

				}
			}
			return BadRequest("Invalid request.");
		}
		/// <summary>
		///  
		/// </summary>
		/// <param name="phoneNumber"></param>
		/// <returns></returns>
		private IActionResult Register(string phoneNumber)
		{
			if (!string.IsNullOrEmpty(phoneNumber))
			{
				//NEW CODE ---------=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
				string encryptedText = _aesEncryptionServiceV2.Encrypt(phoneNumber);
				var user = new User
				{
					PhoneNumber = encryptedText,
					//Gives User Default Fullname
					Fullname = "User" + Guid.NewGuid().ToString(),
				};

				//OLD CODE
				//var user = new User
				//{
				//    PhoneNumber = phoneNumber,
				//    //Gives User Default Fullname
				//    Fullname = "User" + Guid.NewGuid().ToString(),
				//};
				_userService.Add(user);
				GenerateJwtToken(user.Fullname, user.Id, phoneNumber);
				CurrentUser = user;

			}

			return View();
		}


		/// <summary>
		/// bu funksiya login servisden istifade ederek usere sms gonderir ve sonra user varsa sms kodunu
		/// viewbaga yazir ve onu redirect eleyir sms-in view hissesine 
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		[HttpPost]
		public IActionResult Login(PhoneNumberViewModel phoneNumberViewModel)
		{
			if (phoneNumberViewModel != null)
			{
				//var sms = _codeGeneratorService.GenerateCode();
				//_smsSenderService.SendSms(phoneNumberViewModel.PhoneNumber, sms);
				ViewBag.Test = "Test";

				var sms = "6434";
				SmsCode = sms;
				phoneNumber = phoneNumberViewModel.PhoneNumber;
				// Task Number:1 Details In Tasks
				//Redirect
				return RedirectToAction("SMS", "Account");
			}
			return View();
		}
	}
}