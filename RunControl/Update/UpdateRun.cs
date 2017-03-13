using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunControl.Update
{
	static public class UpdateRun
	{
		public static void Startup()
		{
			UpdateHandler Handler = new UpdateHandler();
			Handler.CheckUpdate();
		}
	}
}
