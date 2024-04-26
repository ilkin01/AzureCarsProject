using App.Business.Abstract;
using App.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Concrete
{
    public class HelperService : IHelperService
    {

        private INotificationService _notificationService;
        private ISmsSenderService _smsSenderService;

        public HelperService(INotificationService notificationService, ISmsSenderService smsSenderService)
        {
            _notificationService = notificationService;
            _smsSenderService = smsSenderService;
        }

        public void ShowError(string message,string HappenedWhere)
        {
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(" - - - - - - - - - Error Catched - - - - - - - - - ");
			Console.WriteLine($" - - - - - - - - - {HappenedWhere} - - - - - - - - - ");
			Console.WriteLine(message);
			Console.ForegroundColor = ConsoleColor.White;
		}

        public void SendNotification(string vendor, string model,int id)
        {
            var notifications = _notificationService.GetAll();
            foreach (var notification in notifications)
            {
                if (notification.Vendor == vendor)
                {
                    if (notification.Model != null)
                    {
                        if (notification.Model == model)
                        {
                            //Ama burada else yazmaq olmaz cunku else hali olarsa adam bmw m5 bildirimi almaq istese burada sadece
                            //bmw m7 bele olsa o bildirim alacaq
                            //Send Sms or email to user
                            var smsContent =
                    $"Sizin Isteyinize Uygun Olan {notification.Vendor} {notification.Model} Avtomobili Hazirda Saytda Var!" +
                    $"Linke Tiklayaraq Avtomobile Baxa Bilersiz:" +
                    $" https://localhost:7146/Car/SelectedCar?carId={id}";
                            _smsSenderService.SendSms(notification.User.PhoneNumber,smsContent);
                        }
                    }
                    else
                    {
                        //Burada yollama sebebim cunku demeli adam vendoru secib amaki model secmiyib yaniki vendoru
                        //uygun olan butun masinlardan bildirim almaq isteyib
                        //Send Sms or email to user
                        var smsContent =
                    $"Sizin Isteyinize Uygun Olan {notification.Vendor} Avtomobili Hazirda Saytda Var!" +
                    $"Linke Tiklayaraq Avtomobile Baxa Bilersiz:" +
                    $"https://localhost:7146/Car/SelectedCar?carId={id}";
                        _smsSenderService.SendSms(notification.User.PhoneNumber, smsContent);
                    }
                }
            }
        }

        public void ReplaceTextInStringFields(object obj, string searchText, string replacement)
        {
            //Type type = obj.GetType();
            //PropertyInfo[] properties = type.GetProperties();

            //foreach (PropertyInfo property in properties)
            //{
            //    Type propertyType = property.PropertyType;
            //    PropertyInfo[] propertyInfos = propertyType.GetProperties();
            //    if (propertyInfos.Length > 0)
            //    {
            //        foreach (PropertyInfo propertyInfo in propertyInfos)
            //        {
            //            string currentValue = (string)property.GetValue(obj);
            //            string newValue = currentValue.Replace(searchText, replacement);
            //            property.SetValue(obj, newValue);
            //        }
            //    }
            //    if (property.PropertyType == typeof(string))
            //    {
            //        string currentValue = (string)property.GetValue(obj);
            //        string newValue = currentValue.Replace(searchText, replacement);
            //        property.SetValue(obj, newValue);
            //    }

            //}

            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (property.PropertyType == typeof(string))
                {
                    string currentValue = (string)property.GetValue(obj);
                    if (currentValue != null)
                    {
                        string newValue = currentValue.Replace(searchText, replacement);
                        property.SetValue(obj, newValue);
                    }
                }
                else if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
                {
                    object nestedObj = property.GetValue(obj);
                    if (nestedObj != null)
                    {
                        ReplaceTextInStringFields(nestedObj, searchText, replacement);
                    }
                }
            }
        }
    }
}
