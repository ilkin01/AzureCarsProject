//using App.Business.Abstract;
//using App.Business.Concrete;
//using App.DataAccess.Concrete.EfEntityFramework;
//using App.Entities.Models;
//using Microsoft.AspNetCore.Http.Extensions;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore.Diagnostics;
//using Microsoft.Extensions.Hosting;
//using Newtonsoft.Json;
////using Microsoft.EntityFrameworkCore;
//using System.Data.Entity;
//using WebUI.Dtos;
//using WebUI.Helper;
//using WebUI.Models;

//namespace WebUI.Controllers
//{
//	public class CarController : Controller
//	{
//		private readonly CarDBContext _dbContext;
//		private ICarService _carService;
//		private IPostService _postService;
//		private IMainService _IMainService;
//		private IFavoriteService _favoriteService;
//		private ICloudinaryImageService _cloudinaryImageService;
//		private IFilterService _filterService;
//		private IHelperService _helperService;
//		private IFinanceService _financeService;
//		private INotificationService _notificationService;
//		public static List<Image> images { get; set; } = new List<Image>();
//		private List<IFormFile> images2;
//		private static CarFilter _carFilter;

//		private static int TotalPages;
//		/// <summary>
//		/// Bu Property Meqsedi Sehife (Page) Basina Gosterilecek Masin Sayisi
//		/// </summary>
//		private static int CarPageCount;

//		/// <summary>
//		/// Filter edilende pagination islesin deye yaradilmis deyisen
//		/// </summary>
//		private static List<Post> fC = new List<Post>();
//		private static List<Car> AllCars = new List<Car>();

//		private static List<Post> AllPosts = new List<Post>();

//		private static List<Post> AllVips = new List<Post>();
//		/// <summary>
//		/// fcnin vipsidi
//		/// </summary>
//		private static List<Post> allVips = new List<Post>();
//		/// <summary>
//		/// bu deyisen eger sort olunubsa allpostu yeniden constructorun icinde assign elemek olmaz
//		/// </summary>
//		private static bool isSorted = false;
//		public CarController(CarDBContext dbContext, ICarService carService, IPostService postService, IFavoriteService favoriteService = null, ICloudinaryImageService cloudinaryImageService = null, IFilterService filterService = null, IHelperService helperService = null, IMainService iMainService = null, IFinanceService financeService = null, INotificationService notificationService = null)
//		{
//			_dbContext = dbContext;
//			images2 = new List<IFormFile>();
//			_carService = carService;
//			_postService = postService;
//			_favoriteService = favoriteService;
//			_cloudinaryImageService = cloudinaryImageService;
//			_filterService = filterService;
//			_helperService = helperService;
//			_IMainService = iMainService;
//			_notificationService = notificationService;
//			if (!isSorted)
//			{
//				//Buradaki Allcars zaten postlarin icinde var
//				//AllCars = _carService.GetAll();
//				AllPosts = _postService.GetAll().Where(p => p.IsDeleted == false).ToList();
//				AllVips = AllPosts.FindAll(p => p.IsVip == true);
//			}
//			CarPageCount = 9;

//			//This is for test
//			TotalPages = AllPosts.Count / CarPageCount;
//			_financeService = financeService;

//			// this is for real use
//			//TotalPages = _carService.GetAll().Count/CarPageCount;
//		}

//		//-------------------------------------------------------------------------
//		#region Notification Operations

//		[HttpPost]
//		public IActionResult AddNotification(string vendor, string model)
//		{
//			if (AccountController.CurrentUser != null)
//			{

//				var notification = new Notification
//				{
//					Vendor = vendor,
//					Model = model,
//					UserId = AccountController.CurrentUser.Id
//				};
//				_notificationService.Add(notification);
//				_dbContext.SaveChanges();
//				return Ok(notification);
//			}
//			return BadRequest("User Is Null");
//		}
//		[HttpPost]
//		public IActionResult DeleteNotification(int id)
//		{
//			var notification = _notificationService.GetAll().FirstOrDefault(n => n.Id == id);
//			if (notification != null)
//			{
//				_notificationService.Delete(notification);
//				_dbContext.SaveChanges();
//				return Ok("Notification Is Deleted");
//			}
//			return BadRequest();
//		}
//		[HttpPost]
//		public IActionResult UpdateNotification(string vendor, string model, int id)
//		{
//			var notification = _notificationService.GetAll().FirstOrDefault(n => n.Id == id);
//			if (notification != null)
//			{
//				notification.Vendor = vendor;
//				notification.Model = model;
//				_notificationService.Update(notification);
//				_dbContext.SaveChanges();
//				return Ok("Notification Is Updated");
//			}
//			return BadRequest();
//		}
//		#endregion
//		//-------------------------------------------------------------------------

//		[HttpPost]
//		public IActionResult ShowSimilarCars(int carId)
//		{
//			var similarPosts = new List<Post>();
//			if (carId != 0)
//			{
//				var post = _postService.GetAll().FirstOrDefault(p => p.CarId == carId);
//				var allPosts = _postService.GetAll().ToList();

//				foreach (var post1 in allPosts)
//				{
//					if ((post1.Id != post.Id) && // Exclude the current post
//						(post1.Car.Main.Vendor == post.Car.Main.Vendor &&
//						post1.Car.Main.Model == post.Car.Main.Model))
//					{
//						similarPosts.Add(post1);
//					}
//				}
//			}
//			var result = new SimilarPostDto();
//			result.Posts = similarPosts;
//			if (AccountController.CurrentUser != null)
//			{
//				result.Favorites = AccountController.CurrentUser.Favorites;
//			}
//			return Ok(result);
//		}





//		public IActionResult About()
//		{
//			return View();
//		}

//		private void InitializeBrandModels(Dictionary<string, List<string>> brandModels, List<Main> mains)
//		{
//			mains = mains.OrderBy((m) => m.Vendor).ToList();
//			foreach (var main in mains)
//			{
//				if (!brandModels.ContainsKey(main.Vendor))
//				{
//					brandModels[main.Vendor] = new List<string>();
//				}
//				brandModels[main.Vendor].Add(main.Model);
//			}
//		}

//		public IActionResult Cars()
//		{
//			List<Main> mains = _IMainService.GetAll().ToList();
//			Dictionary<string, List<string>> brandModels = new Dictionary<string, List<string>>();
//			InitializeBrandModels(brandModels, mains);

//			ViewBag.BrandModels = Newtonsoft.Json.JsonConvert.SerializeObject(brandModels);
//			var posts = _postService.GetAll().Where(p => p.IsDeleted == false).ToList();

//			ViewBag.Posts = posts;
//			_carFilter = _filterService.GetFilterDatas();
//			ViewBag.Filter = _carFilter;
//			ViewBag.Test = AccountController.test;
//			ViewBag.CurrentUser = AccountController.CurrentUser;

//			var paginationDto = new PaginationDto();
//			paginationDto.CurrentPage = 1;
//			paginationDto.SelectedPage = 1;
//			if (AccountController.CurrentUser != null)
//			{
//				ViewBag.Favorites = AccountController.CurrentUser.Favorites;
//			}
//			Pagination(paginationDto, AllPosts, AllVips);

//			ViewBag.Pagination = paginationDto;



//			return View();
//		}

//		[HttpPost]
//		public IActionResult Pagination(int currentPage, int selectedPage)
//		{
//			if (fC.Count == 0)
//			{
//				var paginationDto = new PaginationDto();
//				paginationDto.CurrentPage = currentPage;
//				paginationDto.SelectedPage = selectedPage;
//				Pagination(paginationDto, AllPosts, AllVips);
//				//Bu Hissede Optimizasiya etmek olar hansiki biz burada butun favoritelere yolllamaq yerine
//				//sadece hemin carlara ait favoriteleri yaniki hemin pagedeki carlara ait favoriteleri yollamaq lazimdir
//				if (AccountController.CurrentUser != null)
//				{
//					paginationDto.Favorites = AccountController.CurrentUser.Favorites;
//				}
//				return Ok(paginationDto);
//			}
//			else
//			{
//				AllPosts = fC.Where(p => p.IsDeleted == false).ToList();
//				var paginationDto = new PaginationDto();
//				paginationDto.CurrentPage = currentPage;
//				paginationDto.SelectedPage = selectedPage;
//				Pagination(paginationDto, AllPosts, allVips);
//				if (AccountController.CurrentUser != null)
//				{
//					paginationDto.Favorites = AccountController.CurrentUser.Favorites;
//				}
//				return Ok(paginationDto);
//			}
//		}
//		/// <summary>
//		/// bura gelen pagination dtonun icerisine yazilir datalar
//		/// </summary>
//		/// <param name="paginationDto"></param>
//		/// <param name="filteredCars"></param>
//		/// <param name="vipPosts"></param>
//		public void Pagination(PaginationDto paginationDto, List<Post> filteredCars, List<Post> vipPosts)
//		{

//			int startIndex = (paginationDto.SelectedPage - 1) * CarPageCount;
//			int endIndex = startIndex + CarPageCount;
//			paginationDto.Posts = filteredCars.Skip(startIndex).Take(CarPageCount).ToList();
//			paginationDto.Vips = vipPosts.Skip(startIndex).Take(6).ToList();
//			paginationDto.TotalPages2 = (int)Math.Ceiling((double)vipPosts.Count / CarPageCount);
//			paginationDto.TotalPages = (int)Math.Ceiling((double)filteredCars.Count / CarPageCount);

//		}



//		[HttpPost]
//		public IActionResult Cars(CarFilterViewModel carFilterViewModel)
//		{

//			List<Main> mains = _IMainService.GetAll().ToList();
//			Dictionary<string, List<string>> brandModels = new Dictionary<string, List<string>>();
//			InitializeBrandModels(brandModels, mains);
//			//foreach (var main in mains)
//			//{
//			//    if (!brandModels.ContainsKey(main.Vendor))
//			//    {
//			//        brandModels[main.Vendor] = new List<string>();
//			//    }
//			//    brandModels[main.Vendor].Add(main.Model);
//			//}

//			ViewBag.BrandModels = JsonConvert.SerializeObject(brandModels);

//			_helperService.ReplaceTextInStringFields(carFilterViewModel, "Open This For Selection", "");
//			//if (HelperFunctions.IsEmpty(carFilterViewModel))
//			//{
//			//    return RedirectToAction("Cars", "Car");
//			//}
//			var filter = new Filter
//			{
//				Cities = carFilterViewModel.Cities,
//				Engines = carFilterViewModel.Engines,
//				Finances = carFilterViewModel.Finances,
//				Mains = carFilterViewModel.Mains,
//				MarketPlaces = carFilterViewModel.MarketPlaces,
//				MaxCarPrice = carFilterViewModel.MaxCarPrice,
//				MaxHorsePower = carFilterViewModel.MaxHorsePower,
//				MaxKilometer = carFilterViewModel.MaxKilometer,
//				MinCarPrice = carFilterViewModel.MinCarPrice,
//				MinHorsePower = carFilterViewModel.MinHorsePower,
//				MinKilometer = carFilterViewModel.MinKilometer,
//				OwnerCounts = carFilterViewModel.OwnerCounts,
//				SeatCounts = carFilterViewModel.SeatCounts,
//				Statuses = carFilterViewModel.Statuses,
//				TractionTypes = carFilterViewModel.TractionTypes,
//				Transmissions = carFilterViewModel.Transmissions
//			};
//			var filteredCars = _filterService.GetFilteredCars(filter);
//			var paginationDto = new PaginationDto();
//			paginationDto.CurrentPage = 1; // You can update this as needed
//			paginationDto.SelectedPage = 1; // You can update this as needed

//			// Apply filters to VIP posts (assuming VIP posts are stored in AllVips)
//			var filteredVips = filteredCars.Where(p => p.IsVip == true && p.IsDeleted == false).ToList();

//			AllPosts = filteredCars;
//			allVips = filteredVips;
//			fC = filteredCars;
//			allVips = filteredVips;
//			// Update AllVips with filtered VIP posts

//			Pagination(paginationDto, AllPosts, allVips); // Pass the filtered cars to Pagination method
//			ViewBag.Filter = _carFilter;
//			ViewBag.Posts = filteredCars;

//			// Pass the filtered VIP posts to the view
//			ViewBag.Vips = filteredVips;

//			return View();
//		}
//		//[HttpPost]
//		//public IActionResult AddImage(string param)
//		//{
//		//    images.Add(new Image { Link = param });
//		//    return Ok(param);
//		//}

//		public IActionResult FavoritesMenu()
//		{
//			if (AccountController.CurrentUser != null)
//			{
//				//var userFavorites = _favoriteService.GetAll();
//				//var favortitesTemp = new List<Favorite>();
//				//for (int i = 0; i < AccountController.CurrentUser.Favorites.Count; i++)
//				//{
//				//    if (userFavorites[i].User.Id == AccountController.CurrentUser.Id)
//				//    {
//				//        favortitesTemp.Add(userFavorites[i]);
//				//    }
//				//}
//				//AccountController.CurrentUser.Favorites = favortitesTemp;
//				var favorites = AccountController.CurrentUser.Favorites;
//				foreach (var favorite in favorites)
//				{
//					//[ROH] Refactor Olunmamis Hali
//					//favorite.Post.Car = AllCars.FirstOrDefault(f => f.Id == favorite.Post.Car.Id);
//					//AllCars silindiyine gore allpostdan gedib axtarir ve ordaki cari qaytarir
//					favorite.Post.Car = AllPosts.FirstOrDefault(p => p.Car.Id == favorite.Post.Car.Id).Car;
//				}
//				//ViewBag.Favorites = AccountController.CurrentUser.Favorites;
//				ViewBag.Favorites = favorites;
//				return View();
//			}
//			else
//			{
//				//return Json(new { redirectUrl = Url.Action("Login", "Account") });
//				return RedirectToAction("Login", "Account", new { Data = "First you must login" });
//			}
//		}

//		[HttpPost]
//		public IActionResult AddImage(string param)
//		{
//			// Handle the image here, save it to the appropriate location, and update your logic.
//			// For demonstration purposes, let's assume you're saving the image to a folder named "Images".

//			images.Add(new Image { Link = param });
//			//images.Add(image);
//			return Ok(param);

//		}

//		[HttpPost]
//		public IActionResult DeleteCar(int carId)
//		{
//			var post = AccountController.CurrentUser.Posts.FirstOrDefault(c => c.Car.Id == carId);
//			post.IsDeleted = true;
//			_postService.Update(post);
//			var p = _postService.GetAll();
//			return Ok(AccountController.CurrentUser.Posts);
//		}

//		[HttpPost]
//		public IActionResult ReSharePost(int id)
//		{
//			var post = AccountController.CurrentUser.Posts.FirstOrDefault(P => P.Id == id);
//			post.IsDeleted = false;
//			post.Date = DateTime.Now;
//			_postService.Update(post);
//			var p = _postService.GetAll();
//			return Ok(AccountController.CurrentUser.Posts);
//		}


//		[HttpPost]
//		public IActionResult MakePostVip(int id)
//		{
//			var post = AccountController.CurrentUser.Posts.FirstOrDefault(P => P.Id == id);
//			post.IsVip = true;
//			_postService.Update(post);
//			var p = _postService.GetAll();
//			return Ok(AccountController.CurrentUser.Posts);
//		}

//		[HttpPost]
//		public IActionResult DeleteFromVip(int id)
//		{
//			var post = AccountController.CurrentUser.Posts.FirstOrDefault(P => P.Id == id);
//			post.IsVip = false;
//			_postService.Update(post);
//			var p = _postService.GetAll();
//			return Ok(AccountController.CurrentUser.Posts);
//		}

//		public IActionResult SelectedCar(int carId, int edit = 0)
//		{
//			var post = AllPosts.FirstOrDefault(c => c.Car.Id == carId);
//			//[ROH] Refactor Olunmamis Hali
//			//Burada eger hec ViewBag Car Istifade olunmursa silmek lazimdi
//			//ViewBag.Car = AllCars.FirstOrDefault(c => c.Id == carId);
//			//Refactor hali
//			ViewBag.Car = AllPosts.FirstOrDefault(p => p.Car.Id == carId).Car;


//			ViewBag.Post = post;
//			if (edit == 1)
//			{
//				ViewBag.IsEditable = true;
//			}
//			post.ViewCount += 1;
//			_postService.Update(post);
//			return View();
//		}

//		/// <summary>
//		/// Bura string json gelecek icerisinde post id comment filan olacaq
//		/// </summary>
//		/// <param name=""></param>
//		/// <param name=""></param>
//		/// <returns></returns>
//		[HttpPost]
//		public IActionResult AddReview(string comment, int postId)
//		{
//			if (!string.IsNullOrEmpty(comment))
//			{
//				var review = new Review
//				{
//					Comment = comment,
//					Date = DateTime.Now,
//					IsDeleted = false,
//					User = AccountController.CurrentUser,
//					PostId = postId,
//					Post = null
//				};
//				_dbContext.Reviews.Add(review);
//				_dbContext.SaveChanges();
//				return Ok(review);
//			}
//			return BadRequest();
//		}

//		public IActionResult AddCar()
//		{

//			if (AccountController.CurrentUser != null)
//			{
//				List<Main> mains = _IMainService.GetAll().ToList();
//				Dictionary<string, List<string>> brandModels = new Dictionary<string, List<string>>();
//				foreach (var main in mains)
//				{
//					if (!brandModels.ContainsKey(main.Vendor))
//					{
//						brandModels[main.Vendor] = new List<string>();
//					}
//					brandModels[main.Vendor].Add(main.Model);
//				}

//				ViewBag.BrandModels = Newtonsoft.Json.JsonConvert.SerializeObject(brandModels);
//				ViewBag.SeedDatas = new AddCarViewModel();
//				ViewBag.User = AccountController.CurrentUser;
//				return View();
//			}
//			else
//			{
//				return RedirectToAction("Login", "Account", new { Data = "First you must login", actionName = "AddCar", controllerName = "Car" });
//			}

//			// ---------------------------------------------------------------------------

//			// FOR REAL USE
//			//if (AccountController.CurrentUser != null)
//			//{
//			//    ViewBag.User = AccountController.CurrentUser;
//			//    return View();
//			//}

//			//return RedirectToAction("Login", "Account",new { Data= "You must register first" });


//			// FOR TEST
//			// ---------------------------------------------------------------------------


//		}

//		[HttpPost]
//		public IActionResult SaveChanges(int carId, string changes)
//		{
//			var Changes = JsonConvert.DeserializeObject<List<string>>(changes);
//			//[ROH] Refactor Olunmamis Hali
//			//var car = AllCars.FirstOrDefault(c => c.Id == carId);
//			//Refactor hali
//			var car = AllPosts.FirstOrDefault(p => p.Car.Id == carId).Car;

//			car.About = Changes[0];
//			car.VINCode = Changes[1];
//			var finance = _financeService.GetAll().FirstOrDefault(c => c.Id == car.Finance.Id);
//			finance.Price = decimal.Parse(Changes[2]);
//			_carService.Update(car);
//			_financeService.Update(finance);
//			var userPost = AccountController.CurrentUser.Posts.FirstOrDefault(p => p.Car.Id == car.Id);
//			userPost.Car.Finance.Price = finance.Price;
//			return Ok(car);
//		}

//		// FOR REAL USE
//		[HttpPost]
//		public IActionResult Favorite(int param)
//		{
//			if (AccountController.CurrentUser != null)
//			{
//				Favorite favorite = new Favorite
//				{
//					PostId = param,
//					UserId = AccountController.CurrentUser.Id
//				};
//				var posts = _postService.GetAll();
//				favorite.Post = posts.FirstOrDefault(p => p.Id == param);
//				favorite.User = AccountController.CurrentUser;
//				_favoriteService.Add(favorite);
//				AccountController.CurrentUser.Favorites.Add(favorite);

//				return Json(new { id = favorite.Id });
//			}
//			else
//			{
//				return Json(new { redirectUrl = Url.Action("Login", "Account") });
//			}
//		}
//		// ---------------------------------------------------------------------------
//		[HttpPost]
//		public IActionResult Sort(string data)
//		{
//			if (data == "By Date")
//			{
//				AllPosts = AllPosts.OrderBy(p => p.Date).ToList();
//				isSorted = false;
//			}
//			else if (data == "priceHTL")
//			{
//				AllPosts = AllPosts.OrderByDescending(p => p.Car.Finance.Price).ToList();
//				AllVips = AllVips.OrderByDescending(p => p.Car.Finance.Price).ToList();
//				isSorted = true;
//			}
//			else if (data == "priceLTH")
//			{
//				AllPosts = AllPosts.OrderBy(p => p.Car.Finance.Price).ToList();
//				AllVips = AllVips.OrderBy(p => p.Car.Finance.Price).ToList();
//				isSorted = true;
//			}
//			else
//			{
//				AllPosts = AllPosts.OrderBy(p => p.Date).ToList();
//				isSorted = false;
//			}
//			var paginationDto = new PaginationDto();
//			Pagination(paginationDto, AllPosts, AllVips);
//			if (AccountController.CurrentUser != null)
//			{
//				paginationDto.Favorites = AccountController.CurrentUser.Favorites;
//			}



//			return Ok(paginationDto);
//		}



//		[HttpPost]
//		public IActionResult FavoriteDelete(int param)
//		{
//			if (AccountController.CurrentUser != null)
//			{
//				var favorite = AccountController.CurrentUser.Favorites.Find(f => f.PostId == param);

//				AccountController.CurrentUser.Favorites.Remove(favorite);
//				_favoriteService.Delete(favorite);

//				return Json(new { favorite.PostId });

//			}
//			else
//			{
//				return Json(new { redirectUrl = Url.Action("Login", "Account") });
//			}
//		}

//		[HttpPost]
//		public IActionResult AddCar(CarViewModel viewModel)
//		{
//			try
//			{
//				// Create and add the 'Main' entity
//				var main = new Main
//				{
//					Vendor = viewModel.Main.Vendor,
//					Model = viewModel.Main.Model,
//					SubModel = "S",
//					CarType = viewModel.Main.CarType,
//					Year = viewModel.Main.Year,
//					Kilometer = viewModel.Main.Kilometer,
//					Color = viewModel.Main.Color,
//				};
//				_dbContext.Mains.Add(main);

//				// Create and add the 'Engine' entity
//				var engine = new Engine
//				{
//					EngineCapacity = viewModel.Engine.EngineCapacity,
//					EnginePower = viewModel.Engine.EnginePower,
//					FuelType = viewModel.Engine.FuelType,
//				};
//				_dbContext.Engines.Add(engine);

//				// Create and add the 'Finance' entity
//				var finance = new Finance
//				{
//					Price = viewModel.Finance.Price,
//					CurrencyType = "USD",
//					IsCredit = viewModel.Finance.IsCredit,
//					IsBarter = viewModel.Finance.IsBarter,
//					IsGuarantee = false,
//				};
//				_dbContext.Finances.Add(finance);

//				// Create and add the 'Status' entity
//				var status = new Status
//				{
//					IsHit = viewModel.Status.IsHit,
//					IsPaint = viewModel.Status.IsPaint,
//				};
//				_dbContext.Statuses.Add(status);

//				//    foreach (var item in images)
//				//    {
//				//        var result = _cloudinaryImageService.AddToCloduinary(item.Link);
//				//        item.Link = result;
//				//    }

//				foreach (var item in images)
//				{
//					var result = _cloudinaryImageService.AddToCloduinary(item.Link);
//					if(result != null)
//					{
//						item.Link = result;
//					}
//					else
//					{
//						_helperService.ShowError("Error Happened In Cloudinary", "Cloudinary");
//						throw new Exception("Error Happened In Cloudinary");
//					}
//				}

//				// Create and add the 'Car' entity
//				var car = new Car
//				{
//					MarketPlace = viewModel.MarketPlace,
//					VINCode = viewModel.VINCode,
//					SeatCount = viewModel.SeatCount,
//					OwnerCount = viewModel.OwnerCount,
//					City = viewModel.City,
//					About = viewModel.About,
//					Transmission = viewModel.Transmission,
//					TractionType = viewModel.TractionType,
//					Main = main,
//					Engine = engine,
//					Status = status,
//					Finance = finance,
//					Images = images,
//					OwnerId = AccountController.CurrentUser.Id, // Assuming this is the owner's ID
//				};

//				foreach (var image in images)
//				{
//					image.Car = car;
//					_dbContext.Images.Add(image);
//				}

//				_dbContext.Cars.Add(car);


//				// Create and add the 'Post' entity
//				var post = new Post
//				{
//					Car = car,
//					Date = DateTime.Now,
//					IsBoosted = false,
//					IsDeleted = false,
//					IsPremium = false,
//					ViewCount = 0,
//					IsVip = false,
//					UserId = AccountController.CurrentUser.Id, // Assuming this is the user's ID
//				};
//				_dbContext.Posts.Add(post);

//				// Save all changes to the database after adding the post
//				_dbContext.SaveChanges();

//				_helperService.SendNotification(car.Main.Vendor, car.Main.Model, post.Id);
//				//DateTime oneWeekAgo = DateTime.Now.AddDays(-7);

//				//var recentPosts = AllPosts
//				//	.Where(post => post.Date >= oneWeekAgo)
//				//	.ToList();
//				//foreach (var item in recentPosts)
//				//{
//				//	item.IsDeleted = true;
//				//	_dbContext.Posts.Update(item);
//				//}
//				//_dbContext.SaveChanges();
//				AccountController.CurrentUser.Posts.Add(post);
//				return RedirectToAction("Cars", "Car");
//			}
//			catch (Exception ex)
//			{
//				// Handle exceptions here, log, or return an error response
//				// ...
//				return View(viewModel); // Return to the view with an error message, for example
//			}
//		}
//	}
//}



using App.Business.Abstract;
using App.DataAccess.Concrete.EfEntityFramework;
using App.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
//using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using System.Security.Claims;
using WebUI.Dtos;
using WebUI.Helper;
using WebUI.Models;

namespace WebUI.Controllers
{
	public class CarController : Controller
	{
		private readonly CarDBContext _dbContext;
		private IPostService _postService;
		private IMainService _IMainService;
		private IFavoriteService _favoriteService;
		private ICloudinaryImageService _cloudinaryImageService;
		private IFilterService _filterService;
		private IHelperService _helperService;
		private IFinanceService _financeService;
		private INotificationService _notificationService;
		public static List<Image> images { get; set; } = new List<Image>();
		private List<IFormFile> images2;
		private static CarFilter _carFilter;

		private static int TotalPages;
		/// <summary>
		/// Bu Property Meqsedi Sehife (Page) Basina Gosterilecek Masin Sayisi
		/// </summary>
		private static int CarPageCount;

		/// <summary>
		/// Filter edilende pagination islesin deye yaradilmis deyisen
		/// </summary>
		private static List<Post> fC = new List<Post>();
		private static List<Car> AllCars = new List<Car>();

		//[ROH]
		//AllPost Old Variant
		private static List<Post> AllPosts = new List<Post>();
		//AllPost New Variant
		private static IEnumerable<Post> AllPostsV2;


		private static List<Post> AllVips = new List<Post>();
		/// <summary>
		/// fcnin vipsidi
		/// </summary>
		private static List<Post> allVips = new List<Post>();
		/// <summary>
		/// bu deyisen eger sort olunubsa allpostu yeniden constructorun icinde assign elemek olmaz
		/// </summary>
		private static bool isSorted = false;

		private readonly IHttpContextAccessor _httpContextAccessor;

		public CarController(CarDBContext dbContext, ICarService carService, IPostService postService, IFavoriteService favoriteService = null, ICloudinaryImageService cloudinaryImageService = null, IFilterService filterService = null, IHelperService helperService = null, IMainService iMainService = null, IFinanceService financeService = null, INotificationService notificationService = null, IHttpContextAccessor httpContextAccessor = null)
		{
			_dbContext = dbContext;
			images2 = new List<IFormFile>();
			_postService = postService;
			_favoriteService = favoriteService;
			_cloudinaryImageService = cloudinaryImageService;
			_filterService = filterService;
			_helperService = helperService;
			_IMainService = iMainService;
			_notificationService = notificationService;
			if (!isSorted)
			{
				//Buradaki Allcars zaten postlarin icinde var
				//AllCars = _carService.GetAll();
				var op1 = DateTime.Now;

				AllPostsV2 = _postService.GetAll().Where(p => p.IsDeleted == false);
				var op1End = DateTime.Now;
				var duration = op1 - op1End;
				Console.WriteLine(duration);
				op1 = DateTime.Now;

				AllPosts = _postService.GetAll().Where(p => p.IsDeleted == false).ToList();

				op1End = DateTime.Now;
				duration = op1 - op1End;
				Console.WriteLine(duration);
				AllVips = AllPosts.FindAll(p => p.IsVip == true);
			}
			CarPageCount = 9;

			//This is for test
			TotalPages = AllPosts.Count / CarPageCount;
			_financeService = financeService;
			_httpContextAccessor = httpContextAccessor;

			// this is for real use
			//TotalPages = _carService.GetAll().Count/CarPageCount;
		}

		//-------------------------------------------------------------------------
		#region Notification Operations

		[HttpPost]
		public IActionResult AddNotification(string vendor, string model)
		{
			if (AccountController.CurrentUser != null)
			{

				var notification = new Notification
				{
					Vendor = vendor,
					Model = model,
					UserId = AccountController.CurrentUser.Id
				};
				_notificationService.Add(notification);
				_dbContext.SaveChanges();
				return Ok(notification);
			}
			return BadRequest("User Is Null");
		}
		[HttpPost]
		public IActionResult DeleteNotification(int id)
		{
			var notification = _notificationService.GetAll().FirstOrDefault(n => n.Id == id);
			if (notification != null)
			{
				_notificationService.Delete(notification);
				_dbContext.SaveChanges();
				return Ok("Notification Is Deleted");
			}
			return BadRequest();
		}
		[HttpPost]
		public IActionResult UpdateNotification(string vendor, string model, int id)
		{
			var notification = _notificationService.GetAll().FirstOrDefault(n => n.Id == id);
			if (notification != null)
			{
				notification.Vendor = vendor;
				notification.Model = model;
				_notificationService.Update(notification);
				_dbContext.SaveChanges();
				return Ok("Notification Is Updated");
			}
			return BadRequest();
		}
		#endregion
		//-------------------------------------------------------------------------

		[HttpPost]
		public IActionResult ShowSimilarCars(int carId)
		{
			var similarPosts = new List<Post>();
			if (carId != 0)
			{
				var post = _postService.GetAll().FirstOrDefault(p => p.CarId == carId);
				var allPosts = _postService.GetAll().ToList();

				foreach (var post1 in allPosts)
				{
					if ((post1.Id != post.Id) && // Exclude the current post
						(post1.Car.Main.Vendor == post.Car.Main.Vendor &&
						post1.Car.Main.Model == post.Car.Main.Model))
					{
						similarPosts.Add(post1);
					}
				}
			}
			var result = new SimilarPostDto();
			result.Posts = similarPosts;
			if (AccountController.CurrentUser != null)
			{
				result.Favorites = AccountController.CurrentUser.Favorites;
			}
			return Ok(result);
		}





		public IActionResult About()
		{
			return View();
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

		public IActionResult Cars()
		{
			List<Main> mains = _IMainService.GetAll().ToList();
			Dictionary<string, List<string>> brandModels = new Dictionary<string, List<string>>();
			InitializeBrandModels(brandModels, mains);

			ViewBag.BrandModels = Newtonsoft.Json.JsonConvert.SerializeObject(brandModels);
			var posts = _postService.GetAll().Where(p => p.IsDeleted == false).ToList();

			ViewBag.Posts = posts;
			_carFilter = _filterService.GetFilterDatas();
			ViewBag.Filter = _carFilter;
			ViewBag.Test = AccountController.test;
			ViewBag.CurrentUser = AccountController.CurrentUser;

			var paginationDto = new PaginationDto();
			paginationDto.CurrentPage = 1;
			paginationDto.SelectedPage = 1;
			if (AccountController.CurrentUser != null)
			{
				ViewBag.Favorites = AccountController.CurrentUser.Favorites;
			}
			Pagination(paginationDto, AllPosts, AllVips);

			ViewBag.Pagination = paginationDto;



			var token = Request.Cookies["token"];
			if (!string.IsNullOrEmpty(token))
			{

				var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
				var jsonToken = handler.ReadToken(token) as System.IdentityModel.Tokens.Jwt.JwtSecurityToken;

				// Token içindeki bilgileri al
				//var id = jsonToken?.Claims.FirstOrDefault(claim => claim.Type == "Id")?.Value;
				//var phoneNumber = jsonToken?.Claims.FirstOrDefault(claim => claim.Type == "PhoneNumber")?.Value;
				//var Fullaname = jsonToken?.Claims.FirstOrDefault(claim => claim.Type == "Fullname")?.Value;

				var id = jsonToken?.Payload["Id"];
				var fullname = jsonToken?.Payload["name"];
				var phonenumber = jsonToken?.Payload["PhoneNumber"];

                Console.WriteLine(id);
                Console.WriteLine(fullname);
                Console.WriteLine(phonenumber);
                var phoneNumber = jsonToken?.Claims.FirstOrDefault(claim => claim.Type == "PhoneNumber")?.Value;
				var Fullaname = jsonToken?.Claims.FirstOrDefault(claim => claim.Type == "Fullname")?.Value;


			}
			return View();
		}

		[HttpPost]
		public IActionResult Pagination(int currentPage, int selectedPage)
		{
			if (fC.Count == 0)
			{
				var paginationDto = new PaginationDto();
				paginationDto.CurrentPage = currentPage;
				paginationDto.SelectedPage = selectedPage;
				Pagination(paginationDto, AllPosts, AllVips);
				//Bu Hissede Optimizasiya etmek olar hansiki biz burada butun favoritelere yolllamaq yerine
				//sadece hemin carlara ait favoriteleri yaniki hemin pagedeki carlara ait favoriteleri yollamaq lazimdir
				if (AccountController.CurrentUser != null)
				{
					paginationDto.Favorites = AccountController.CurrentUser.Favorites;
				}
				return Ok(paginationDto);
			}
			else
			{
				AllPosts = fC.Where(p => p.IsDeleted == false).ToList();
				var paginationDto = new PaginationDto();
				paginationDto.CurrentPage = currentPage;
				paginationDto.SelectedPage = selectedPage;
				Pagination(paginationDto, AllPosts, allVips);
				if (AccountController.CurrentUser != null)
				{
					paginationDto.Favorites = AccountController.CurrentUser.Favorites;
				}
				return Ok(paginationDto);
			}
		}
		/// <summary>
		/// bura gelen pagination dtonun icerisine yazilir datalar
		/// </summary>
		/// <param name="paginationDto"></param>
		/// <param name="filteredCars"></param>
		/// <param name="vipPosts"></param>
		public void Pagination(PaginationDto paginationDto, List<Post> filteredCars, List<Post> vipPosts)
		{

			int startIndex = (paginationDto.SelectedPage - 1) * CarPageCount;
			int endIndex = startIndex + CarPageCount;
			paginationDto.Posts = filteredCars.Skip(startIndex).Take(CarPageCount).ToList();
			paginationDto.Vips = vipPosts.Skip(startIndex).Take(6).ToList();
			paginationDto.TotalPages2 = (int)Math.Ceiling((double)vipPosts.Count / CarPageCount);
			paginationDto.TotalPages = (int)Math.Ceiling((double)filteredCars.Count / CarPageCount);

		}



		[HttpPost]
		public IActionResult Cars(CarFilterViewModel carFilterViewModel)
		{

			List<Main> mains = _IMainService.GetAll().ToList();
			Dictionary<string, List<string>> brandModels = new Dictionary<string, List<string>>();
			InitializeBrandModels(brandModels, mains);
			//foreach (var main in mains)
			//{
			//    if (!brandModels.ContainsKey(main.Vendor))
			//    {
			//        brandModels[main.Vendor] = new List<string>();
			//    }
			//    brandModels[main.Vendor].Add(main.Model);
			//}

			ViewBag.BrandModels = JsonConvert.SerializeObject(brandModels);

			_helperService.ReplaceTextInStringFields(carFilterViewModel, "Open This For Selection", "");
			//if (HelperFunctions.IsEmpty(carFilterViewModel))
			//{
			//    return RedirectToAction("Cars", "Car");
			//}
			var filter = new Filter
			{
				Cities = carFilterViewModel.Cities,
				Engines = carFilterViewModel.Engines,
				Finances = carFilterViewModel.Finances,
				Mains = carFilterViewModel.Mains,
				MarketPlaces = carFilterViewModel.MarketPlaces,
				MaxCarPrice = carFilterViewModel.MaxCarPrice,
				MaxHorsePower = carFilterViewModel.MaxHorsePower,
				MaxKilometer = carFilterViewModel.MaxKilometer,
				MinCarPrice = carFilterViewModel.MinCarPrice,
				MinHorsePower = carFilterViewModel.MinHorsePower,
				MinKilometer = carFilterViewModel.MinKilometer,
				OwnerCounts = carFilterViewModel.OwnerCounts,
				SeatCounts = carFilterViewModel.SeatCounts,
				Statuses = carFilterViewModel.Statuses,
				TractionTypes = carFilterViewModel.TractionTypes,
				Transmissions = carFilterViewModel.Transmissions
			};
			var filteredCars = _filterService.GetFilteredCars(filter);
			var paginationDto = new PaginationDto();
			paginationDto.CurrentPage = 1; // You can update this as needed
			paginationDto.SelectedPage = 1; // You can update this as needed

			// Apply filters to VIP posts (assuming VIP posts are stored in AllVips)
			var filteredVips = filteredCars.Where(p => p.IsVip == true && p.IsDeleted == false).ToList();

			AllPosts = filteredCars;
			allVips = filteredVips;
			fC = filteredCars;
			allVips = filteredVips;
			// Update AllVips with filtered VIP posts

			Pagination(paginationDto, AllPosts, allVips); // Pass the filtered cars to Pagination method
			ViewBag.Filter = _carFilter;
			ViewBag.Posts = filteredCars;

			// Pass the filtered VIP posts to the view
			ViewBag.Vips = filteredVips;

			return View();
		}
		//[HttpPost]
		//public IActionResult AddImage(string param)
		//{
		//    images.Add(new Image { Link = param });
		//    return Ok(param);
		//}

		public IActionResult FavoritesMenu()
		{
			if (AccountController.CurrentUser != null)
			{
				//var userFavorites = _favoriteService.GetAll();
				//var favortitesTemp = new List<Favorite>();
				//for (int i = 0; i < AccountController.CurrentUser.Favorites.Count; i++)
				//{
				//    if (userFavorites[i].User.Id == AccountController.CurrentUser.Id)
				//    {
				//        favortitesTemp.Add(userFavorites[i]);
				//    }
				//}
				//AccountController.CurrentUser.Favorites = favortitesTemp;
				var favorites = AccountController.CurrentUser.Favorites;
				foreach (var favorite in favorites)
				{
					//[ROH] Refactor Olunmamis Hali
					//favorite.Post.Car = AllCars.FirstOrDefault(f => f.Id == favorite.Post.Car.Id);
					//AllCars silindiyine gore allpostdan gedib axtarir ve ordaki cari qaytarir
					favorite.Post.Car = AllPosts.FirstOrDefault(p => p.Car.Id == favorite.Post.Car.Id).Car;
				}
				//ViewBag.Favorites = AccountController.CurrentUser.Favorites;
				ViewBag.Favorites = favorites;
				return View();
			}
			else
			{
				//return Json(new { redirectUrl = Url.Action("Login", "Account") });
				return RedirectToAction("Login", "Account", new { Data = "First you must login" });
			}
		}

		[HttpPost]
		public IActionResult AddImage(string param)
		{
			// Handle the image here, save it to the appropriate location, and update your logic.
			// For demonstration purposes, let's assume you're saving the image to a folder named "Images".

			images.Add(new Image { Link = param });
			//images.Add(image);
			return Ok(param);

		}

		[HttpPost]
		public IActionResult DeleteCar(int carId)
		{
			var post = AccountController.CurrentUser.Posts.FirstOrDefault(c => c.Car.Id == carId);
			post.IsDeleted = true;
			_postService.Update(post);
			var p = _postService.GetAll();
			return Ok(AccountController.CurrentUser.Posts);
		}

		[HttpPost]
		public IActionResult ReSharePost(int id)
		{
			var post = AccountController.CurrentUser.Posts.FirstOrDefault(P => P.Id == id);
			post.IsDeleted = false;
			post.Date = DateTime.Now;
			_postService.Update(post);
			var p = _postService.GetAll();
			return Ok(AccountController.CurrentUser.Posts);
		}


		[HttpPost]
		public IActionResult MakePostVip(int id)
		{
			var post = AccountController.CurrentUser.Posts.FirstOrDefault(P => P.Id == id);
			post.IsVip = true;
			_postService.Update(post);
			var p = _postService.GetAll();
			return Ok(AccountController.CurrentUser.Posts);
		}

		[HttpPost]
		public IActionResult DeleteFromVip(int id)
		{
			var post = AccountController.CurrentUser.Posts.FirstOrDefault(P => P.Id == id);
			post.IsVip = false;
			_postService.Update(post);
			var p = _postService.GetAll();
			return Ok(AccountController.CurrentUser.Posts);
		}

		public IActionResult SelectedCar(int carId, int edit = 0)
		{
			var post = AllPosts.FirstOrDefault(c => c.Car.Id == carId);
			//[ROH] Refactor Olunmamis Hali
			//Burada eger hec ViewBag Car Istifade olunmursa silmek lazimdi
			//ViewBag.Car = AllCars.FirstOrDefault(c => c.Id == carId);
			//Refactor hali
			ViewBag.Car = AllPosts.FirstOrDefault(p => p.Car.Id == carId).Car;


			ViewBag.Post = post;
			if (edit == 1)
			{
				ViewBag.IsEditable = true;
			}
			post.ViewCount += 1;
			_postService.Update(post);
			return View();
		}

		/// <summary>
		/// Bura string json gelecek icerisinde post id comment filan olacaq
		/// </summary>
		/// <param name=""></param>
		/// <param name=""></param>
		/// <returns></returns>
		[HttpPost]
		public IActionResult AddReview(string comment, int postId)
		{
			if (!string.IsNullOrEmpty(comment))
			{
				var review = new Review
				{
					Comment = comment,
					Date = DateTime.Now,
					IsDeleted = false,
					User = AccountController.CurrentUser,
					PostId = postId,
					Post = null
				};
				_dbContext.Reviews.Add(review);
				_dbContext.SaveChanges();
				return Ok(review);
			}
			return BadRequest();
		}

		public IActionResult AddCar()
		{

			if (AccountController.CurrentUser != null)
			{
				List<Main> mains = _IMainService.GetAll().ToList();
				Dictionary<string, List<string>> brandModels = new Dictionary<string, List<string>>();
				foreach (var main in mains)
				{
					if (!brandModels.ContainsKey(main.Vendor))
					{
						brandModels[main.Vendor] = new List<string>();
					}
					brandModels[main.Vendor].Add(main.Model);
				}

				ViewBag.BrandModels = Newtonsoft.Json.JsonConvert.SerializeObject(brandModels);
				ViewBag.SeedDatas = new AddCarViewModel();
				ViewBag.User = AccountController.CurrentUser;
				return View();
			}
			else
			{
				return RedirectToAction("Login", "Account", new { Data = "First you must login", actionName = "AddCar", controllerName = "Car" });
			}

			// ---------------------------------------------------------------------------

			// FOR REAL USE
			//if (AccountController.CurrentUser != null)
			//{
			//    ViewBag.User = AccountController.CurrentUser;
			//    return View();
			//}

			//return RedirectToAction("Login", "Account",new { Data= "You must register first" });


			// FOR TEST
			// ---------------------------------------------------------------------------


		}

		[HttpPost]
		public IActionResult SaveChanges(int carId, string changes)
		{
			///

			//[ROH] Refactor olunmamis berbat hali
			///
			//var Changes = JsonConvert.DeserializeObject<List<string>>(changes);
			////[ROH] Refactor Olunmamis Hali
			////var car = AllCars.FirstOrDefault(c => c.Id == carId);
			////Refactor hali
			//var car = AllPosts.FirstOrDefault(p => p.Car.Id == carId).Car;

			//car.About = Changes[0];
			//car.VINCode = Changes[1];
			//var finance = _financeService.GetAll().FirstOrDefault(c => c.Id == car.Finance.Id);
			//finance.Price = decimal.Parse(Changes[2]);
			//_carService.Update(car);
			//_financeService.Update(finance);
			//var userPost = AccountController.CurrentUser.Posts.FirstOrDefault(p => p.Car.Id == car.Id);
			//userPost.Car.Finance.Price = finance.Price;
			//return Ok(car);

			//Yeni Refactor Hali Kohnede Menasiz Yere Butun carlarin icinden alirdiq
			var Changes = JsonConvert.DeserializeObject<List<string>>(changes);
			var post = AllPosts.FirstOrDefault(p => p.Car.Id == carId);

			post.Car.About = Changes[0];
			post.Car.VINCode = Changes[1];

			post.Car.Finance.Price = decimal.Parse(Changes[2]);
			_postService.Update(post);
			_financeService.Update(post.Car.Finance);
			var userPost = AccountController.CurrentUser.Posts.FirstOrDefault(p => p.Car.Id == post.Car.Id);
			userPost.Car.Finance.Price = post.Car.Finance.Price;
			return Ok(post.Car);
		}

		// FOR REAL USE
		[HttpPost]
		public IActionResult Favorite(int param)
		{
			if (AccountController.CurrentUser != null)
			{
				Favorite favorite = new Favorite
				{
					PostId = param,
					UserId = AccountController.CurrentUser.Id
				};
				var posts = _postService.GetAll();
				favorite.Post = posts.FirstOrDefault(p => p.Id == param);
				favorite.User = AccountController.CurrentUser;
				_favoriteService.Add(favorite);
				AccountController.CurrentUser.Favorites.Add(favorite);

				return Json(new { id = favorite.Id });
			}
			else
			{
				return Json(new { redirectUrl = Url.Action("Login", "Account") });
			}
		}
		// ---------------------------------------------------------------------------
		[HttpPost]
		public IActionResult Sort(string data)
		{
			if (data == "By Date")
			{
				AllPosts = AllPosts.OrderBy(p => p.Date).ToList();
				isSorted = false;
			}
			else if (data == "priceHTL")
			{
				AllPosts = AllPosts.OrderByDescending(p => p.Car.Finance.Price).ToList();
				AllVips = AllVips.OrderByDescending(p => p.Car.Finance.Price).ToList();
				isSorted = true;
			}
			else if (data == "priceLTH")
			{
				AllPosts = AllPosts.OrderBy(p => p.Car.Finance.Price).ToList();
				AllVips = AllVips.OrderBy(p => p.Car.Finance.Price).ToList();
				isSorted = true;
			}
			else
			{
				AllPosts = AllPosts.OrderBy(p => p.Date).ToList();
				isSorted = false;
			}
			var paginationDto = new PaginationDto();
			Pagination(paginationDto, AllPosts, AllVips);
			if (AccountController.CurrentUser != null)
			{
				paginationDto.Favorites = AccountController.CurrentUser.Favorites;
			}



			return Ok(paginationDto);
		}



		[HttpPost]
		public IActionResult FavoriteDelete(int param)
		{
			if (AccountController.CurrentUser != null)
			{
				var favorite = AccountController.CurrentUser.Favorites.Find(f => f.PostId == param);

				AccountController.CurrentUser.Favorites.Remove(favorite);
				_favoriteService.Delete(favorite);

				return Json(new { favorite.PostId });

			}
			else
			{
				return Json(new { redirectUrl = Url.Action("Login", "Account") });
			}
		}

		[HttpPost]
		public IActionResult AddCar(CarViewModel viewModel)
		{
			try
			{
				// Create and add the 'Main' entity
				var main = new Main
				{
					Vendor = viewModel.Main.Vendor,
					Model = viewModel.Main.Model,
					SubModel = "S",
					CarType = viewModel.Main.CarType,
					Year = viewModel.Main.Year,
					Kilometer = viewModel.Main.Kilometer,
					Color = viewModel.Main.Color,
				};
				_dbContext.Mains.Add(main);

				// Create and add the 'Engine' entity
				var engine = new Engine
				{
					EngineCapacity = viewModel.Engine.EngineCapacity,
					EnginePower = viewModel.Engine.EnginePower,
					FuelType = viewModel.Engine.FuelType,
				};
				_dbContext.Engines.Add(engine);

				// Create and add the 'Finance' entity
				var finance = new Finance
				{
					Price = viewModel.Finance.Price,
					CurrencyType = "USD",
					IsCredit = viewModel.Finance.IsCredit,
					IsBarter = viewModel.Finance.IsBarter,
					IsGuarantee = false,
				};
				_dbContext.Finances.Add(finance);

				// Create and add the 'Status' entity
				var status = new Status
				{
					IsHit = viewModel.Status.IsHit,
					IsPaint = viewModel.Status.IsPaint,
				};
				_dbContext.Statuses.Add(status);

				//    foreach (var item in images)
				//    {
				//        var result = _cloudinaryImageService.AddToCloduinary(item.Link);
				//        item.Link = result;
				//    }

				foreach (var item in images)
				{
					var result = _cloudinaryImageService.AddToCloduinary(item.Link);
					if (result != null)
					{
						item.Link = result;
					}
					else
					{
						_helperService.ShowError("Error Happened In Cloudinary", "Cloudinary");
						throw new Exception("Error Happened In Cloudinary");
					}
				}

				// Create and add the 'Car' entity
				var car = new Car
				{
					MarketPlace = viewModel.MarketPlace,
					VINCode = viewModel.VINCode,
					SeatCount = viewModel.SeatCount,
					OwnerCount = viewModel.OwnerCount,
					City = viewModel.City,
					About = viewModel.About,
					Transmission = viewModel.Transmission,
					TractionType = viewModel.TractionType,
					Main = main,
					Engine = engine,
					Status = status,
					Finance = finance,
					Images = images,
					OwnerId = AccountController.CurrentUser.Id, // Assuming this is the owner's ID
				};

				foreach (var image in images)
				{
					image.Car = car;
					_dbContext.Images.Add(image);
				}

				_dbContext.Cars.Add(car);


				// Create and add the 'Post' entity
				var post = new Post
				{
					Car = car,
					Date = DateTime.Now,
					IsBoosted = false,
					IsDeleted = false,
					IsPremium = false,
					ViewCount = 0,
					IsVip = false,
					UserId = AccountController.CurrentUser.Id, // Assuming this is the user's ID
				};
				_dbContext.Posts.Add(post);

				// Save all changes to the database after adding the post
				_dbContext.SaveChanges();

				_helperService.SendNotification(car.Main.Vendor, car.Main.Model, post.Id);
				//DateTime oneWeekAgo = DateTime.Now.AddDays(-7);

				//var recentPosts = AllPosts
				//	.Where(post => post.Date >= oneWeekAgo)
				//	.ToList();
				//foreach (var item in recentPosts)
				//{
				//	item.IsDeleted = true;
				//	_dbContext.Posts.Update(item);
				//}
				//_dbContext.SaveChanges();
				AccountController.CurrentUser.Posts.Add(post);
				return RedirectToAction("Cars", "Car");
			}
			catch (Exception ex)
			{
				// Handle exceptions here, log, or return an error response
				// ...
				_helperService.ShowError(ex.Message, "Add Car");
				return View(viewModel); // Return to the view with an error message, for example
			}
		}
	}
}
