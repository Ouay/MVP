using RunControl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Modem
{
	public class SMSHandler
	{
		/// <summary>
		/// Local path of the executable (used for script launch)
		/// </summary>
		public string _Path { get; set; }

		public SMSHandler()
		{
			_Path  = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
		}

		public string GetContentSMS()
		{
			SMS sms = new SMS(this._Path);
			string Content = sms.ReadSMSString();
			return Content;
		}

		public string GetDateSMS()
		{
			string count = "";
			try
			{
				string Content = GetContentSMS();
				count = "";
				string[] counter = Content.Split(new string[] { "<Date>", "</Date>" }, StringSplitOptions.RemoveEmptyEntries);
				count = counter[1];
			}
			catch(IndexOutOfRangeException outOfRange)
			{
				count = "";
			}
			LogControl.Write("Date " + count);
			return count;
		}

		/// <summary>
		/// Make a request to send an SMS
		/// </summary>
		/// <param name="_phoneNb">Phone number</param>
		/// <param name="_msg">sPayload of the SMS</param>
		public void SendSMS(string _phoneNb, string _msg)
		{
			SMS sms = new SMS(this._Path);
			sms.Send(_phoneNb, _msg);
		}

		public List<SMSContent> ReadSMS()
		{
			SMS sms = new SMS(this._Path);
			List<SMSContent> t = sms.ReadSMS();
			return t;
		}
	}
}
