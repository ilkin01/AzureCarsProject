using App.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Abstract
{
    public interface INotificationService
    {
        List<Notification> GetAll();
        Notification Get();

        void Add(Notification notification);
        void Update(Notification notification);
        void Delete(Notification notification);
    }
}
