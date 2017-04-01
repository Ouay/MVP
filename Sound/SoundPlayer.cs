using RunControl;
using System;
using System.Diagnostics;

namespace Sound
{
	public class SoundPlayer
	{
		public SoundPlayer()
		{

		}

		public void Play(string _filePath)
		{
			try
			{
				LogControl.Write("[SOUNDPLAYER] : Playing sound");
				ProcessStartInfo P = new ProcessStartInfo();
				P.FileName = "aplay";
				P.Arguments = _filePath;
				Process pro = Process.Start(P);
				//pro.StartInfo = P;
				//pro.Start();
				//pro.WaitForExit();

				//Console.ReadKey();
			}
			catch(Exception e)
			{
				LogControl.Write("[SOUNDPLAYER] : ERROR | " + e.Message);
			}
		}
	}
}
