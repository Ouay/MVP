using System;
using System.Diagnostics;

namespace RunControl
{
	public class Close
	{
		const bool DEBUG = false;

		public static void CloseProgram(bool prompt = false)
		{
			Debug.WriteLineIf(DEBUG, "Closing");
			if (prompt)
			{
				Console.WriteLine("Press any key to quit");
				Console.ReadKey();
			}
			Environment.Exit(0);
		}
	}
}
