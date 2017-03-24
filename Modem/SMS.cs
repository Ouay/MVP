using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modem
{
	class SMS
	{
		/// <summary>
		/// Path of the location of the scripts
		/// </summary>
		private string path = string.Empty;

		public SMS(string _path)
		{
			path = _path;
		}

		/// <summary>
		/// Send a message using SendSMS.sh script in path
		/// </summary>
		/// <param name="_number">Phone number</param>
		/// <param name="_message">Payload</param>
		/// <returns></returns>
		public bool Send(string _number, string _message)
		{
			try
			{
				ProcessStartInfo P = new ProcessStartInfo();
				P.FileName = path + "/SendSMS.sh";
				P.Arguments = "+41786268658 " + _message;
				P.UseShellExecute = false;
				P.RedirectStandardOutput = true;
				Process pro = new Process();
				pro.StartInfo = P;
				pro.Start();
				pro.WaitForExit();
				return true;
			}
			catch(Exception e)
			{
				return false;
			}
		}

		public List<SMSContent> ReadSMS()
		{
			List<SMSContent> listSMS = new List<SMSContent>();
			ProcessStartInfo P = new ProcessStartInfo();
			P.FileName = path + "/ReadSMS.sh";
			Process process = new Process();
			process.StartInfo = P;
			process.Start();
			string output = process.StandardOutput.ReadToEnd();
			process.WaitForExit();
			listSMS = ParseInput(output);

			return listSMS;
		}

		private List<SMSContent> ParseInput(string output)
		{
			List<SMSContent> list = new List<SMSContent>();

			return list;
		}
	}
}
