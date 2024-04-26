using System.Text;

namespace WebUI.Models
{
    public class SmsViewModel
    {
        public string sms1 { get; set; }
        public string sms2 { get; set; }
        public string sms3 { get; set; }
        public string sms4 { get; set; }

        public string GetSms()
        {
            var sb = new StringBuilder();
            sb.Append(sms1);
            sb.Append(sms2);
            sb.Append(sms3);
            sb.Append(sms4);
            return sb.ToString();
        }
    }
}