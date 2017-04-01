using RunControl;
using System;
using System.Diagnostics;

namespace Sound
{
	public static class SoundPlayer
	{
		public static void Play(string _filePath)
		{
			try
			{
				LogControl.Write("[SOUNDPLAYER] : Playing sound");
				ProcessStartInfo P = new ProcessStartInfo();
				P.FileName = "aplay";
				P.Arguments = "-Dhw:2,0 " + _filePath;
				P.UseShellExecute = false;
				P.RedirectStandardOutput = true;
				Process pro = new Process();
				pro.StartInfo = P;
				pro.Start();
				pro.WaitForExit();

				Console.ReadKey();
			}
			catch(Exception e)
			{
				LogControl.Write("[SOUNDPLAYER] : ERROR | " + e.Message);
			}
		}
	}
}
