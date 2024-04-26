using App.Business.Abstract;
using App.DataAccess.Abstract;
using App.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Concrete
{
    public class NotificationService : INotificationService
    {
        private INotificationDal _notificationDal;

        public NotificationService(INotificationDal notificationDal)
        {
            _notificationDal = notificationDal;
        }

        public Notification Get()
        {
            return _notificationDal.Get();
        }

        public List<Notification> GetAll()
        {
            return _notificationDal.GetList();
        }

        public void Add(Notification notification)
        {
            _notificationDal.Add(notification);
        }

        public void Delete(Notification notification)
        {
            _notificationDal.Delete(notification);
        }

        public void Update(Notification notification)
        {
            _notificationDal.Update(notification);
        }
    }
}
