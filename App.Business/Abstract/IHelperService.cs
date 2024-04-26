using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Abstract
{
    public interface IHelperService
    {

        void ReplaceTextInStringFields(object obj, string searchText, string replacement);

        void SendNotification(string vendor, string model, int id);

        void ShowError(string message, string HappenedWhere);

	}
}
