using RunControl;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
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

		internal string ReadSMSString()
		{
			List<SMSContent> listSMS = new List<SMSContent>();
			ProcessStartInfo P = new ProcessStartInfo();
			P.FileName = path + "/ReadSMS.sh";
			P.RedirectStandardOutput = true;
			P.UseShellExecute = false;
			Process process = new Process();
			process.StartInfo = P;
			process.Start();
			string text = "";
			while (!process.StandardOutput.EndOfStream)
				text += process.StandardOutput.ReadLine();
			LogControl.Write("Text = " + text);
			process.WaitForExit();
			return text;
		}

		/// <summary>
		/// Send a message using SendSMS.sh script in path
		/// </summary>
		/// <param name="_number">Phone number</param>
		/// <param name="_message">Payload</param>
		/// <returns></returns>
		public bool Send(string _number, string _message)
		{
			LogControl.Write("[SMS] : Sending an SMS");
			try
			{
				ProcessStartInfo P = new ProcessStartInfo();
				P.FileName = path + "/SendSMS.sh";
				P.Arguments = _number + " " + _message;
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
				LogControl.Write("[SMS] : Error at send | " + e.Message);
				return false;
			}
		}

		public List<SMSContent> ReadSMS()
		{
			List<SMSContent> listSMS = new List<SMSContent>();
			ProcessStartInfo P = new ProcessStartInfo();
			P.FileName = path + "/ReadSMS.sh";
			P.RedirectStandardOutput = true;
			P.UseShellExecute = false;
			Process process = new Process();
			process.StartInfo = P;
			process.Start();
			string text = "";
			while (!process.StandardOutput.EndOfStream)
				text += process.StandardOutput.ReadLine();

			process.WaitForExit();
			listSMS = ParseInput(text);
			return listSMS;
		}

		private List<SMSContent> ParseInput(string output)
		{
			List<SMSContent> list = new List<SMSContent>();
			int count = 0;
			string[] counter = output.Split(new string[] { "<count>", "</count>" },StringSplitOptions.RemoveEmptyEntries);
			count = Int32.Parse(counter[1]);
			LogControl.Write("Counted " + count + " SMS");
			string[] Msg = counter[2].Split(new string[] { "<Message>", "</Message>" }, StringSplitOptions.RemoveEmptyEntries);
			return list;
		}
	}
}
