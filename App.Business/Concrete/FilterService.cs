using App.Business.Abstract;
using App.Entities.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Concrete
{
    public class FilterService : IFilterService
    {
        private readonly ICarService _carService;
        private readonly IPostService _postService;

        public FilterService(ICarService carService, IPostService postService = null)
        {
            _carService = carService;
            _postService = postService;
        }

        public CarFilter GetFilterDatas()
        {
            //public List<string> MarketPlace { get; set; }
            //public List<int?> SeatCount { get; set; }
            //public List<int?> OwnerCount { get; set; }
            //public List<string> City { get; set; }
            //public List<string> Transmission { get; set; }
            //public List<string> TractionType { get; set; }
            //public List<Main> Main { get; set; }
            //public List<Engine> Engine { get; set; }
            //public List<Status> Status { get; set; }
            //public List<Finance> Finance { get; set; }
            //public List<Post> Post { get; set; }

            CarFilter filter = new CarFilter();
            foreach (var car in _carService.GetAll())
            {
                filter.MarketPlace.Add(car.MarketPlace);
                filter.SeatCount.Add(car.SeatCount);
                filter.OwnerCount.Add(car.OwnerCount);
                filter.City.Add(car.City);
                filter.Transmission.Add(car.Transmission);
                filter.TractionType.Add(car.TractionType);
                filter.Main.Add(car.Main);
                filter.Engine.Add(car.Engine);
                filter.Status.Add(car.Status);
                filter.Finance.Add(car.Finance);

            }
            return filter;
        }

        //public List<Car> GetFilteredCars(Filter filter)
        //{
        //    // OLD Version
        //    //var cars = _carService.GetAll();
        //    //var filteredCars = new List<Car>();

        //    //foreach (var car in cars)
        //    //{
        //    //    if (FilterMatchesCar(car, filter))
        //    //    {
        //    //        filteredCars.Add(car);
        //    //    }
        //    //}

        //    //return filteredCars;

        //}

        public List<Post> GetFilteredCars(Filter filter)
        {

            var posts = _postService.GetAll();
            var filteredCars = new List<Post>();

            foreach (var post in posts)
            {
                if (FilterMatchesCar(post.Car, filter))
                {
                    filteredCars.Add(post);
                }
            }

            return filteredCars;

        }


        private bool FilterMatchesCar(Car car, Filter filter)
        {
            bool result = true;

            if (filter.Mains != null && car.Main != null)
            {
                if (!string.IsNullOrEmpty(filter.Mains.Vendor))
                {
                    if (!string.IsNullOrEmpty(filter.Mains.Model))
                    {

                        if (car.Main.Vendor == filter.Mains.Vendor && car.Main.Model == filter.Mains.Model)
                        {
                            result = true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (car.Main.Vendor == filter.Mains.Vendor)
                        {
                            result = true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(filter.Mains.SubModel))
                {
                    if (car.Main.SubModel == filter.Mains.SubModel)
                    {
                        result = true;
                    }
                    else
                    {
                        return false;
                    }
                }

                if (!string.IsNullOrEmpty(filter.Mains.CarType))
                {
                    if (car.Main.CarType == filter.Mains.CarType)
                    {
                        result = true;
                    }
                    else
                    {
                        return false;
                    }
                }

                if (car.Engine != null && filter.Engines != null)
                {
                    if (!string.IsNullOrEmpty(filter.Engines.FuelType))
                    {
                        if (car.Engine.FuelType == filter.Engines.FuelType)
                        {
                            result = true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(filter.Transmissions))
                {
                    if (car.Transmission == filter.Transmissions)
                    {
                        result = true;
                    }
                    else
                    {
                        return false;
                    }
                }

                if (!string.IsNullOrEmpty(filter.TractionTypes))
                {
                    if (car.TractionType == filter.TractionTypes)
                    {
                        result = true;
                    }
                    else
                    {
                        return false;
                    }
                }

                if (filter.Mains.Year > 0)
                {
                    if (car.Main.Year == filter.Mains.Year)
                    {
                        result = true;
                    }
                    else
                    {
                        return false;
                    }
                }
                if (!string.IsNullOrEmpty(filter.Mains.Color))
                {
                    if (car.Main.Color == filter.Mains.Color)
                    {
                        result = true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            if (car.Engine != null && filter.Engines != null)
            {
                if (filter.Engines.EngineCapacity > 0)
                {
                    if (car.Engine.EngineCapacity == filter.Engines.EngineCapacity)
                    {
                        result = true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            if (!string.IsNullOrEmpty(filter.Cities))
            {
                if (car.City == filter.Cities)
                {
                    result = true;
                }
                else
                {
                    return false;
                }
            }
            if (!string.IsNullOrEmpty(filter.MarketPlaces))
            {
                if (car.MarketPlace == filter.MarketPlaces)
                {
                    result = true;
                }
                else
                {
                    return false;
                }
            }
            if (filter.OwnerCounts.HasValue)
            {
                if (car.OwnerCount == filter.OwnerCounts)
                {
                    result = true;
                }
                else
                {
                    return false;
                }
            }
            if (filter.SeatCounts.HasValue)
            {
                if (car.SeatCount == filter.SeatCounts)
                {
                    result = true;
                }
                else
                {
                    return false;
                }
            }



            if (car.Engine != null && filter.Engines != null)
            {
                if (filter.MinHorsePower > 0 && filter.MaxHorsePower > 0)
                {
                    if (car.Engine.EnginePower >= filter.MinHorsePower && car.Engine.EnginePower <= filter.MaxHorsePower)
                    {
                        result = true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }






            if (car.Finance != null && filter.Finances != null)
            {
                //if (filter.MinCarPrice > 0)
                //    if (car.Finance.Price >= filter.MinCarPrice)
                //        return true;
                //if (filter.MaxCarPrice > 0)
                //    if (car.Finance.Price <= filter.MaxCarPrice)
                //        return true;

                if (filter.MinCarPrice > 0 && filter.MaxCarPrice > 0)
                {
                    if (car.Finance.Price >= filter.MinCarPrice && car.Finance.Price <= filter.MaxCarPrice)
                    {
                        result = true;
                    }
                    else
                    {
                        return false;
                    }
                }

                if (car.Status != null && filter.Statuses != null)
                {
                    if (filter.Statuses.IsHit)
                    {
                        if (filter.Statuses.IsHit == car.Status.IsHit)
                        {
                            result = true;
                        }
                        else { return false; }
                    }
                    if (filter.Statuses.IsPaint)
                    {
                        if (filter.Statuses.IsPaint == car.Status.IsPaint)
                        {
                            result = true;
                        }
                        else { return false; }
                    }
                }
                if (filter.Mains != null && car.Main != null)
                {
                    if (filter.MaxKilometer > 0)
                    {
                        if ((car.Main.Kilometer >= filter.MinKilometer && car.Main.Kilometer <= filter.MaxKilometer))
                        {
                            result = true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(filter.Finances.CurrencyType))
                {
                    if (car.Finance.CurrencyType == filter.Finances.CurrencyType)
                    {

                        result = true;
                    }
                    else { return false; }
                }

                if (filter.Finances.IsCredit)
                {
                    if (filter.Finances.IsCredit == car.Finance.IsCredit)
                    {
                        result = true;
                    }
                    else { return false; }
                }

                if (filter.Finances.IsBarter)
                {
                    if (filter.Finances.IsBarter == car.Finance.IsBarter)
                    {
                        result = true;
                    }
                    else { return false; }
                }

                if (filter.Finances.IsGuarantee)
                {
                    if (filter.Finances.IsGuarantee == car.Finance.IsGuarantee)
                    {
                        result = true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }


            return result;




            //if (filter.Mains != null && car.Main != null)
            //{
            //    if (!string.IsNullOrEmpty(filter.Mains.Vendor))
            //    {
            //        if (!string.IsNullOrEmpty(filter.Mains.Model))
            //        {

            //            if (car.Main.Vendor == filter.Mains.Vendor && car.Main.Model == filter.Mains.Model)
            //            {
            //                result = true;
            //            }
            //            else
            //            {
            //                result = false;
            //            }
            //        }
            //        else
            //        {
            //            if (car.Main.Vendor == filter.Mains.Vendor)
            //            {
            //                result = true;
            //            }
            //            else
            //            {
            //                result = false;
            //            }
            //        }
            //    }

            //    if (!string.IsNullOrEmpty(filter.Mains.SubModel))
            //    {
            //        if (car.Main.SubModel == filter.Mains.SubModel)
            //        {
            //            result = true;
            //        }
            //        else
            //        {
            //            result = false;
            //        }
            //    }

            //    if (!string.IsNullOrEmpty(filter.Mains.CarType))
            //    {
            //        if (car.Main.CarType == filter.Mains.CarType)
            //        {
            //            result = true;
            //        }
            //        else
            //        {
            //            result = false;
            //        }
            //    }

            //    if (car.Engine != null && filter.Engines != null)
            //    {
            //        if (!string.IsNullOrEmpty(filter.Engines.FuelType))
            //        {
            //            if (car.Engine.FuelType == filter.Engines.FuelType)
            //            {
            //                result = true;
            //            }
            //            else
            //            {
            //                result = false;
            //            }
            //        }
            //    }

            //    if (!string.IsNullOrEmpty(filter.Transmissions))
            //    {
            //        if (car.Transmission == filter.Transmissions)
            //        {
            //            result = true;
            //        }
            //        else
            //        {
            //            result = false;
            //        }
            //    }

            //    if (!string.IsNullOrEmpty(filter.TractionTypes))
            //    {
            //        if (car.TractionType == filter.TractionTypes)
            //        {
            //            result = true;
            //        }
            //        else
            //        {
            //            result = false;
            //        }
            //    }

            //    if (filter.Mains.Year > 0)
            //    {
            //        if (car.Main.Year == filter.Mains.Year)
            //        {
            //            result = true;
            //        }
            //        else
            //        {
            //            result = false;
            //        }
            //    }
            //    if (!string.IsNullOrEmpty(filter.Mains.Color))
            //    {
            //        if (car.Main.Color == filter.Mains.Color)
            //        {
            //            result = true;
            //        }
            //        else
            //        {
            //            result = false;
            //        }
            //    }
            //}
            //if (car.Engine != null && filter.Engines != null)
            //{
            //    if (car.Engine.EngineCapacity == filter.Engines.EngineCapacity)
            //    {
            //        result = true;
            //    }
            //    else
            //    {
            //        result = false;
            //    }
            //}

            //if (!string.IsNullOrEmpty(filter.Cities))
            //{
            //    if (car.City == filter.Cities)
            //    {
            //        result = true;
            //    }
            //    else
            //    {
            //        result = false;
            //    }
            //}
            //if (!string.IsNullOrEmpty(filter.MarketPlaces))
            //{
            //    if (car.MarketPlace == filter.MarketPlaces)
            //    {
            //        result = true;
            //    }
            //    else
            //    {
            //        result = false;
            //    }
            //}
            //if (filter.OwnerCounts.HasValue)
            //{
            //    if (car.OwnerCount == filter.OwnerCounts)
            //    {
            //        result = true;
            //    }
            //    else
            //    {
            //        result = false;
            //    }
            //}
            //if (filter.SeatCounts.HasValue)
            //{
            //    if (car.SeatCount == filter.SeatCounts)
            //    {
            //        result = true;
            //    }
            //    else
            //    {
            //        result = false;
            //    }
            //}



            //if (car.Engine != null && filter.Engines != null)
            //{
            //    if (filter.MinHorsePower > 0 && filter.MaxHorsePower > 0)
            //    {
            //        if (car.Engine.EnginePower >= filter.MinHorsePower && car.Engine.EnginePower <= filter.MaxHorsePower)
            //        {
            //            result = true;
            //        }
            //        else
            //        {
            //            result = false;
            //        }
            //    }
            //}






            //if (car.Finance != null && filter.Finances != null)
            //{
            //    //if (filter.MinCarPrice > 0)
            //    //    if (car.Finance.Price >= filter.MinCarPrice)
            //    //        return true;
            //    //if (filter.MaxCarPrice > 0)
            //    //    if (car.Finance.Price <= filter.MaxCarPrice)
            //    //        return true;

            //    if (filter.MinCarPrice > 0 && filter.MaxCarPrice > 0)
            //    {
            //        if (car.Finance.Price >= filter.MinCarPrice && car.Finance.Price <= filter.MaxCarPrice)
            //        {
            //            result = true;
            //        }
            //        else
            //        {
            //            result = false;
            //        }
            //    }

            //    if (car.Status != null && filter.Statuses != null)
            //    {
            //        if (filter.Statuses.IsHit)
            //        {
            //            if (filter.Statuses.IsHit == car.Status.IsHit)
            //            {
            //                result = true;
            //            }
            //            else { result = false; }
            //        }
            //        if (filter.Statuses.IsPaint)
            //        {
            //            if (filter.Statuses.IsPaint == car.Status.IsPaint)
            //            {
            //                result = true;
            //            }else { result = false; }
            //        }
            //    }
            //    if (filter.Mains != null && car.Main != null)
            //    {
            //        if ((car.Main.Kilometer >= filter.MinKilometer && car.Main.Kilometer <= filter.MaxKilometer))
            //        {
            //            result = true;
            //        }
            //        else
            //        {
            //            result = false;
            //        }
            //    }

            //    if (!string.IsNullOrEmpty(filter.Finances.CurrencyType))
            //    {
            //        if (car.Finance.CurrencyType == filter.Finances.CurrencyType)
            //        {

            //            result = true;
            //        }
            //        else { result = false; }
            //    }

            //    if (filter.Finances.IsCredit)
            //    {
            //        if (filter.Finances.IsCredit == car.Finance.IsCredit)
            //        {
            //            result = true;
            //        }
            //        else { result = false; }
            //    }

            //    if (filter.Finances.IsBarter)
            //    {
            //        if (filter.Finances.IsBarter == car.Finance.IsBarter)
            //        {
            //            result = true;
            //        }
            //        else { result = false; }
            //    }

            //    if (filter.Finances.IsGuarantee)
            //    {
            //        if (filter.Finances.IsGuarantee == car.Finance.IsGuarantee)
            //        {
            //            result = true;
            //        }
            //        else
            //        {
            //            result = false;
            //        }
            //    }
            //}
            //// OLD VERSION BROKEN 
            ////if (filter.MinHorsePower > 0)
            ////    if (car.Engine.EnginePower >= filter.MinHorsePower)
            ////        return true;
            ////if (filter.MaxHorsePower > 0)
            ////    if (car.Engine.EnginePower <= filter.MaxHorsePower)
            ////        return true;
            //if (filter.MinHorsePower > 0 && filter.MaxHorsePower>0)
            //    if (car.Engine.EnginePower >= filter.MinHorsePower && car.Engine.EnginePower <= filter.MaxHorsePower)
            //        return true;

            //// OLD VERSION BROKEN 
            ////if (filter.MinKilometer > 0)
            ////    if (car.Main.Kilometer >= filter.MinKilometer)
            ////        return true;
            ////if (filter.MaxKilometer > 0)
            ////    if (car.Main.Kilometer <= filter.MaxKilometer)
            ////        return true;
            //if (filter.MinKilometer > 0 && filter.MaxKilometer>0)
            //    if (car.Main.Kilometer >= filter.MinKilometer && car.Main.Kilometer <= filter.MaxKilometer)
            //        return true;




            //if (!string.IsNullOrEmpty(filter.MarketPlaces))
            //{
            //	if (car.MarketPlace == filter.MarketPlaces)
            //	{
            //		return true;
            //	}
            //}

            //if (filter.SeatCounts.HasValue)
            //{
            //	if (car.SeatCount == filter.SeatCounts)
            //	{
            //		return true;
            //	}
            //	else
            //	{
            //		return false;
            //	}
            //}


            ////OLD
            ////if (filter.OwnerCounts.HasValue)
            ////	if (car.OwnerCount == filter.OwnerCounts)
            ////		return true;
            //if (filter.OwnerCounts.HasValue)
            //{
            //	if (car.OwnerCount == filter.OwnerCounts)
            //	{
            //		return true;
            //	}
            //	else
            //	{
            //		return false;
            //	}
            //}

            ////// OLD
            ////if (!string.IsNullOrEmpty(filter.Cities))
            ////	if (car.City == filter.Cities)
            ////		return true;
            //if (!string.IsNullOrEmpty(filter.Cities))
            //{
            //	if (car.City == filter.Cities)
            //	{
            //		return true;
            //	}
            //	else
            //	{
            //		return false;
            //	}
            //}

            ////OLD
            ////if (!string.IsNullOrEmpty(filter.Transmissions))
            ////	if (car.Transmission == filter.Transmissions)
            ////		return true;
            //if (!string.IsNullOrEmpty(filter.Transmissions))
            //{
            //	if (car.Transmission == filter.Transmissions)
            //	{
            //		return true;
            //	}
            //	else
            //	{
            //		return false;
            //	}
            //}
            ////OLD
            ////if (!string.IsNullOrEmpty(filter.TractionTypes))
            ////	if (car.TractionType == filter.TractionTypes)
            ////		return true;
            //if (!string.IsNullOrEmpty(filter.TractionTypes))
            //{
            //	if (car.TractionType == filter.TractionTypes)
            //	{
            //		return true;
            //	}
            //	else
            //	{
            //		return false;
            //	}
            //}
            //if (filter.Mains != null && car.Main != null)
            //{
            //	if (!string.IsNullOrEmpty(filter.Mains.Vendor))
            //	{
            //		if (!string.IsNullOrEmpty(filter.Mains.Model))
            //		{

            //			if (car.Main.Vendor == filter.Mains.Vendor && car.Main.Model == filter.Mains.Model)
            //			{
            //				return true;
            //			}
            //			else
            //			{
            //				return false;
            //			}
            //		}
            //		else
            //		{
            //			if (car.Main.Vendor == filter.Mains.Vendor)
            //			{
            //				return true;
            //			}
            //			else
            //			{
            //				return false;
            //			}
            //		}
            //	}


            //	//           if (!string.IsNullOrEmpty(filter.Mains.SubModel))
            //	//if (car.Main.SubModel == filter.Mains.SubModel)
            //	//	return true;
            //	if (!string.IsNullOrEmpty(filter.Mains.SubModel))
            //	{
            //		if (car.Main.SubModel == filter.Mains.SubModel)
            //		{
            //			return true;
            //		}
            //		else
            //		{
            //			return false;
            //		}
            //	}


            //	//if (!string.IsNullOrEmpty(filter.Mains.CarType))
            //	//	if (car.Main.CarType == filter.Mains.CarType)
            //	//		return true;
            //	if (!string.IsNullOrEmpty(filter.Mains.CarType))
            //	{
            //		if (car.Main.CarType == filter.Mains.CarType)
            //		{
            //			return true;
            //		}
            //		else
            //		{
            //			return false;
            //		}
            //	}
            //	if (filter.Mains.Year > 0)
            //		if (car.Main.Year == filter.Mains.Year)
            //			return true;

            //	if ((car.Main.Kilometer >= filter.MinKilometer && car.Main.Kilometer <= filter.MaxKilometer))
            //		return true;

            //	//if (!string.IsNullOrEmpty(filter.Mains.Color))
            //	//	if (car.Main.Color == filter.Mains.Color)
            //	//		return true;
            //	if (!string.IsNullOrEmpty(filter.Mains.Color))
            //	{
            //		if (car.Main.Color == filter.Mains.Color)
            //		{
            //			return true;
            //		}
            //		else
            //		{
            //			return false;
            //		}
            //	}
            //}

            //if (car.Engine != null && filter.Engines != null)
            //{
            //	if (car.Engine.EngineCapacity == filter.Engines.EngineCapacity)
            //		return true;
            //	//OLD
            //	//if (!string.IsNullOrEmpty(filter.Engines.FuelType))
            //	//	if (car.Engine.FuelType == filter.Engines.FuelType)
            //	//		return true;
            //	if (!string.IsNullOrEmpty(filter.Engines.FuelType))
            //	{
            //		if (car.Engine.FuelType == filter.Engines.FuelType)
            //		{
            //			return true;
            //		}
            //		else
            //		{
            //			return false;
            //		}
            //	}
            //	if (filter.MinHorsePower > 0 && filter.MaxHorsePower > 0)
            //		if (car.Engine.EnginePower >= filter.MinHorsePower && car.Engine.EnginePower <= filter.MaxHorsePower)
            //			return true;
            //}

            //if (car.Status != null && filter.Statuses != null)
            //{
            //	if (filter.Statuses.IsHit)
            //		if (filter.Statuses.IsHit == car.Status.IsHit)
            //			return true;
            //	if (filter.Statuses.IsPaint)
            //		if (filter.Statuses.IsPaint == car.Status.IsPaint)
            //			return true;
            //}

            //if (car.Finance != null && filter.Finances != null)
            //{
            //	//if (filter.MinCarPrice > 0)
            //	//    if (car.Finance.Price >= filter.MinCarPrice)
            //	//        return true;
            //	//if (filter.MaxCarPrice > 0)
            //	//    if (car.Finance.Price <= filter.MaxCarPrice)
            //	//        return true;

            //	if (filter.MinCarPrice > 0 && filter.MaxCarPrice > 0)
            //		if (car.Finance.Price >= filter.MinCarPrice && car.Finance.Price <= filter.MaxCarPrice)
            //			return true;



            //	if (!string.IsNullOrEmpty(filter.Finances.CurrencyType))
            //		if (car.Finance.CurrencyType == filter.Finances.CurrencyType)
            //			return true;

            //	if (filter.Finances.IsCredit)
            //		if (filter.Finances.IsCredit == car.Finance.IsCredit)
            //			return true;

            //	if (filter.Finances.IsBarter)
            //		if (filter.Finances.IsBarter == car.Finance.IsBarter)
            //			return true;

            //	if (filter.Finances.IsGuarantee)
            //		if (filter.Finances.IsGuarantee == car.Finance.IsGuarantee)
            //			return true;
            //}
            ////// OLD VERSION BROKEN 
            //////if (filter.MinHorsePower > 0)
            //////    if (car.Engine.EnginePower >= filter.MinHorsePower)
            //////        return true;
            //////if (filter.MaxHorsePower > 0)
            //////    if (car.Engine.EnginePower <= filter.MaxHorsePower)
            //////        return true;
            ////if (filter.MinHorsePower > 0 && filter.MaxHorsePower>0)
            ////    if (car.Engine.EnginePower >= filter.MinHorsePower && car.Engine.EnginePower <= filter.MaxHorsePower)
            ////        return true;

            ////// OLD VERSION BROKEN 
            //////if (filter.MinKilometer > 0)
            //////    if (car.Main.Kilometer >= filter.MinKilometer)
            //////        return true;
            //////if (filter.MaxKilometer > 0)
            //////    if (car.Main.Kilometer <= filter.MaxKilometer)
            //////        return true;
            ////if (filter.MinKilometer > 0 && filter.MaxKilometer>0)
            ////    if (car.Main.Kilometer >= filter.MinKilometer && car.Main.Kilometer <= filter.MaxKilometer)
            ////        return true;

            //return false;







            //if (!string.IsNullOrEmpty(filter.MarketPlaces))
            //{
            //    if (car.MarketPlace != filter.MarketPlaces)
            //    {
            //        return false;
            //    }
            //}




            //if (filter.SeatCounts.HasValue )
            //    if(car.SeatCount != filter.SeatCounts)
            //    return false;

            //if (filter.OwnerCounts.HasValue )
            //    if(car.OwnerCount != filter.OwnerCounts)
            //    return false;

            //if (!string.IsNullOrEmpty(filter.Cities) )
            //    if(car.City != filter.Cities)
            //    return false;

            //if (!string.IsNullOrEmpty(filter.Transmissions))
            //    if(car.Transmission != filter.Transmissions)
            //    return false;

            //if (!string.IsNullOrEmpty(filter.TractionTypes))
            //    if(car.TractionType != filter.TractionTypes)
            //    return false;


            //if (filter.Mains != null && car.Main!=null)
            //{
            //    if (!string.IsNullOrEmpty(filter.Mains.Vendor))
            //        if (car.Main.Vendor == filter.Mains.Vendor)
            //        return true;

            //    if (!string.IsNullOrEmpty(filter.Mains.Model))
            //        if(car.Main.Model != filter.Mains.Model)
            //        return false;

            //    if (!string.IsNullOrEmpty(filter.Mains.SubModel))
            //        if(car.Main.SubModel != filter.Mains.SubModel)
            //        return false;

            //    if (!string.IsNullOrEmpty(filter.Mains.CarType))
            //        if(car.Main.CarType != filter.Mains.CarType)
            //        return false;

            //    if (filter.Mains.Year == null)
            //        if( car.Main.Year != filter.Mains.Year)
            //        return false;

            //    if ((car.Main.Kilometer >= filter.MinKilometer && car.Main.Kilometer < filter.MaxKilometer))
            //        return false;

            //    if (!string.IsNullOrEmpty(filter.Mains.Color))
            //        if(car.Main.Color != filter.Mains.Color)
            //        return false;
            //}

            //if (car.Engine != null && filter.Engines != null)
            //{
            //    if (car.Engine.EngineCapacity < filter.MaxHorsePower)
            //        return false;

            //    if (car.Engine.EnginePower < filter.MinHorsePower)
            //        return false;

            //    if (!string.IsNullOrEmpty(filter.Engines.FuelType))
            //        if(car.Engine.FuelType != filter.Engines.FuelType)
            //        return false;
            //}

            //if (car.Status != null && filter.Statuses != null)
            //{
            //    if (filter.Statuses.IsHit != car.Status.IsHit)
            //        return false;

            //    if (filter.Statuses.IsPaint != car.Status.IsPaint)
            //        return false;
            //}

            //if (car.Finance != null && filter.Finances != null)
            //{
            //    if (car.Finance.Price < filter.MinCarPrice || car.Finance.Price > filter.MaxCarPrice)
            //        return false;

            //    if (!string.IsNullOrEmpty(filter.Finances.CurrencyType))
            //        if(car.Finance.CurrencyType != filter.Finances.CurrencyType)
            //        return false;

            //    if (filter.Finances.IsCredit != car.Finance.IsCredit)
            //        return false;

            //    if (filter.Finances.IsBarter != car.Finance.IsBarter)
            //        return false;

            //    if (filter.Finances.IsGuarantee != car.Finance.IsGuarantee)
            //        return false;
            //}

            //if (car.Engine.EnginePower < filter.MinHorsePower || car.Engine.EnginePower > filter.MaxHorsePower)
            //    return false;

            //if (car.Main.Kilometer < filter.MinKilometer || car.Main.Kilometer > filter.MaxKilometer)
            //    return false;

            //return true;




            //if (!string.IsNullOrEmpty(filter.MarketPlaces) && car.MarketPlace != filter.MarketPlaces)
            //    return false;




            //if (filter.SeatCounts.HasValue && car.SeatCount != filter.SeatCounts)
            //    return false;

            //if (filter.OwnerCounts.HasValue && car.OwnerCount != filter.OwnerCounts)
            //    return false;

            //if (!string.IsNullOrEmpty(filter.Cities) && car.City != filter.Cities)
            //    return false;

            //if (!string.IsNullOrEmpty(filter.Transmissions) && car.Transmission != filter.Transmissions)
            //    return false;

            //if (!string.IsNullOrEmpty(filter.TractionTypes) && car.TractionType != filter.TractionTypes)
            //    return false;

            //if (filter.Mains != null && car.MainId != filter.Mains.Id)
            //    return false;

            //if (filter.Engines != null && car.EngineId != filter.Engines.Id)
            //    return false;

            //if (filter.Statuses != null && car.StatusId != filter.Statuses.Id)
            //    return false;

            //if (filter.Finances != null && car.FinanceId != filter.Finances.Id)
            //    return false;

            //if (car.Main != null)
            //{
            //    if (!string.IsNullOrEmpty(filter.Mains.Vendor) && car.Main.Vendor != filter.Mains.Vendor)
            //        return false;

            //    if (!string.IsNullOrEmpty(filter.Mains.Vendor) && car.Main.Model != filter.Mains.Vendor)
            //        return false;

            //    if (!string.IsNullOrEmpty(filter.Mains.SubModel) && car.Main.SubModel != filter.Mains.SubModel)
            //        return false;

            //    if (!string.IsNullOrEmpty(filter.Mains.CarType) && car.Main.CarType != filter.Mains.CarType)
            //        return false;

            //    if (filter.Mains.Year == null && car.Main.Year != filter.Mains.Year)
            //        return false;

            //    if (car.Main.Kilometer < filter.MinKilometer || car.Main.Kilometer > filter.MaxKilometer)
            //        return false;

            //    if (!string.IsNullOrEmpty(filter.Mains.Color) && car.Main.Color != filter.Mains.Color)
            //        return false;
            //}

            //if (car.Engine != null && filter.Engines != null)
            //{
            //    if (car.Engine.EngineCapacity < filter.MaxHorsePower)
            //        return false;

            //    if (car.Engine.EnginePower < filter.MinHorsePower)
            //        return false;

            //    if (!string.IsNullOrEmpty(filter.Engines.FuelType) && car.Engine.FuelType != filter.Engines.FuelType)
            //        return false;
            //}

            //if (car.Status != null && filter.Statuses != null)
            //{
            //    if (filter.Statuses.IsHit != car.Status.IsHit)
            //        return false;

            //    if (filter.Statuses.IsPaint != car.Status.IsPaint)
            //        return false;
            //}

            //if (car.Finance != null && filter.Finances != null)
            //{
            //    if (car.Finance.Price < filter.MinCarPrice || car.Finance.Price > filter.MaxCarPrice)
            //        return false;

            //    if (!string.IsNullOrEmpty(filter.Finances.CurrencyType) && car.Finance.CurrencyType != filter.Finances.CurrencyType)
            //        return false;

            //    if (filter.Finances.IsCredit != car.Finance.IsCredit)
            //        return false;

            //    if (filter.Finances.IsBarter != car.Finance.IsBarter)
            //        return false;

            //    if (filter.Finances.IsGuarantee != car.Finance.IsGuarantee)
            //        return false;
            //}

            //if (car.Engine.EnginePower < filter.MinHorsePower || car.Engine.EnginePower > filter.MaxHorsePower)
            //    return false;

            //if (car.Main.Kilometer < filter.MinKilometer || car.Main.Kilometer > filter.MaxKilometer)
            //    return false;

            //return true;




        }
    }
}
