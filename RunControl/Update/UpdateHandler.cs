using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAppUpdate.Framework;
using NAppUpdate.Framework.Sources;
using NAppUpdate.Framework.Common;
using System.IO;

namespace RunControl.Update
{
	class UpdateHandler
	{
		private string source = @"http://update.ouay.ch/update.xml";
		public UpdateHandler()
		{

		}

		public bool CheckUpdate()
		{
			try
			{
				UpdateManager _UpdateManager = UpdateManager.Instance;
				_UpdateManager.UpdateSource = PrepareUpdateSource();
				_UpdateManager.Config.TempFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TMP");
				_UpdateManager.ReinstateIfRestarted();
				if (UpdateManager.Instance.State == UpdateManager.UpdateProcessState.Checked ||
					   UpdateManager.Instance.State == UpdateManager.UpdateProcessState.AfterRestart ||
					   UpdateManager.Instance.State == UpdateManager.UpdateProcessState.AppliedSuccessfully)
					UpdateManager.Instance.CleanUp();
				UpdateCheck();
				return true;
			}
			catch (Exception e)
			{
				LogControl.Write("[UpdateHandler] : ERROR | " + e.ToString());
				return false;
			}
		}

		private IUpdateSource PrepareUpdateSource()
		{
			LogControl.Write("[UpdateHandler] : Preparing Update Source");
			return new NAppUpdate.Framework.Sources.SimpleWebSource(source);
		}

		protected void UpdateCheck()
		{
			UpdateManager _UpdateManager = UpdateManager.Instance;

			LogControl.Write("[UpdateHandler] : Checking for updates");
			_UpdateManager.BeginCheckForUpdates(asyncResult =>
			{

				if (asyncResult.IsCompleted)
				{
					LogControl.Write("[UpdateHandler] : Check updates Completed");
					//still need to check for caught exceptions if any and rethrow
					((UpdateProcessAsyncResult)asyncResult).EndInvoke();
					//no updates were found, or an error has occured...
					if(_UpdateManager.UpdatesAvailable == 0)
					{
						LogControl.Write("[UpdateHandler] : No update available");
						return;
					}
				}
				TryUpdate();
			}, null);
		}

		private void TryUpdate()
		{
			LogControl.Write("[UpdateHandler] : Updating");
			UpdateManager.Instance.ReportProgress += status =>
			{
				LogControl.Write("[UpdateHandler] : " + status.Percentage + "%");
			};
			UpdateManager.Instance.BeginPrepareUpdates(asyncResult =>
			{
				try
				{
					UpdateManager.Instance.ApplyUpdates(false);
				}
				catch(Exception e)
				{
					LogControl.Write("[UpdateHandler] : ERROR Update | " + e.ToString());
				}
				if (UpdateManager.Instance.State == UpdateManager.UpdateProcessState.RollbackRequired)
					UpdateManager.Instance.RollbackUpdates();
			}, null);
		}
	}
}
