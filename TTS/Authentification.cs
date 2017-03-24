using RunControl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TTS
{
	class Authentification
	{
		public static readonly string AccessUri = @"https://api.cognitive.microsoft.com/sts/v1.0/issueToken";
		private string apiKey;
		private string accessToken;
		private Timer accessTokenRenewer;

		private const int RefreshTokenDuration = 9; //9 minutes only

		public Authentification(string apiKey)
		{
			this.apiKey = apiKey;
			this.accessToken = HttpPost(AccessUri, this.apiKey);

			//Renew token every specified minutes
			accessTokenRenewer = new Timer(new TimerCallback(OnTokenExpiredCallback), this, TimeSpan.FromMinutes(RefreshTokenDuration), TimeSpan.FromMilliseconds(-1));
		}

		public string GetAccessToken()
		{
			return this.accessToken;
		}

		private void RenewAccessToken()
		{
			string newAccessToken = HttpPost(AccessUri, this.apiKey);

			this.accessToken = newAccessToken;
			LogControl.Write("[TTS] : Authentification | Renew token");
		}

		private void OnTokenExpiredCallback(object stateInfo)
		{
			try
			{
				RenewAccessToken();
			}
			catch(Exception ex)
			{
				LogControl.Write("[TTS] : Authentification | ERROR : Failed to renew access token. Details : " + ex.Message);
			}
			finally
			{
				try
				{
					accessTokenRenewer.Change(TimeSpan.FromMinutes(RefreshTokenDuration), TimeSpan.FromMilliseconds(-1));
				}
				catch(Exception e)
				{
					LogControl.Write("[TTS] : Authentification | ERROR : Failed to reschedule the timer");
				}
			}
		}

		private string HttpPost(string accessUri, string apiKey)
		{
			WebRequest webRequest = WebRequest.Create(accessUri);
			webRequest.Method = "Post";
			webRequest.ContentLength = 0;
			webRequest.Headers["Ocp-Apim-Subscription-Key"] = apiKey;

		System.Net.ServicePointManager.ServerCertificateValidationCallback += (s, ce, ca, p) => true;
			using (WebResponse response = webRequest.GetResponse())
			{
				using (Stream stream = response.GetResponseStream())
				{
					using (MemoryStream ms = new MemoryStream())
					{
						byte[] waveBytes = null;
						int count = 0;
						do
						{
							byte[] buf = new byte[1024];
							count = stream.Read(buf, 0, 1024);
							ms.Write(buf, 0, count);
						} while (stream.CanRead && count > 0);

						waveBytes = ms.ToArray();
						return Encoding.UTF8.GetString(waveBytes);
					}
				}
			}

		}
	}
}
