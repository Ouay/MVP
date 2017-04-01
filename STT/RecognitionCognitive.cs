using RunControl;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace STT
{
	public class RecognitionCognitive
	{
		public bool Record(int second = 3)
		{
			try
			{
				LogControl.Write("[RECORDING] : Start recording");
				ProcessStartInfo P = new ProcessStartInfo();
				P.FileName = "arecord";
				P.Arguments = "-D plughw:2,0 -d " + second + " record.wav -f cd";
				P.UseShellExecute = false;
				P.RedirectStandardOutput = true;
				Process pro = new Process();
				pro.StartInfo = P;
				pro.Start();
				pro.WaitForExit();
				LogControl.Write("[RECORDING] : End recording");
			}
			catch (Exception e)
			{
				LogControl.Write("[RECORDING] : ERROR | " + e);
				Thread.Sleep(3000);
				return false;
			}
			return true;
		}

		Authentification auth;

		public RecognitionCognitive()
		{
			auth = new Authentification();
		}

		public string SetupRequest()
		{

			string requestUri = @"https://speech.platform.bing.com/recognize";
			/* URI Params. Refer to the README file for more information. */
			requestUri += @"?scenarios=smd";                                  // websearch is the other main option.
			requestUri += @"&appid=D4D52672-91D7-4C74-8AD8-42B1D98141A5";     // You must use this ID.
			requestUri += @"&locale=fr-FR";                                   // We support several other languages.  Refer to README file.
			requestUri += @"&device.os=wp7";
			requestUri += @"&version=3.0";
			requestUri += @"&format=json";
			requestUri += @"&instanceid=565D69FF-E928-4B7E-87DA-9A750B96D9E3";
			requestUri += @"&requestid=" + Guid.NewGuid().ToString();

			string host = @"speech.platform.bing.com";
			string contentType = @"audio/wav; codec=""audio/pcm""; samplerate=44100";

			string audioFile = "record.wav";
			string responseString;
			FileStream fs = null;

			try
			{
				var token = auth.GetAccessToken();
				LogControl.Write("[RECOGNITION] : Token: " + token);
				LogControl.Write("[RECOGNITION] : Request Uri: " + requestUri);

				HttpWebRequest request = null;
				request = (HttpWebRequest)HttpWebRequest.Create(requestUri);
				request.SendChunked = true;
				request.Accept = @"application/json;text/xml";
				request.Method = "POST";
				request.ProtocolVersion = HttpVersion.Version11;
				request.Host = host;
				request.ContentType = contentType;
				request.Headers["Authorization"] = "Bearer " + token;
				System.Net.ServicePointManager.ServerCertificateValidationCallback += (s, ce, ca, p) => true;

				using (fs = new FileStream(audioFile, FileMode.Open, FileAccess.Read))
				{

					/*
                     * Open a request stream and write 1024 byte chunks in the stream one at a time.
                     */
					byte[] buffer = null;
					int bytesRead = 0;
					using (Stream requestStream = request.GetRequestStream())
					{
						/*
                         * Read 1024 raw bytes from the input audio file.
                         */
						buffer = new Byte[checked((uint)Math.Min(1024, (int)fs.Length))];
						while ((bytesRead = fs.Read(buffer, 0, buffer.Length)) != 0)
						{
							requestStream.Write(buffer, 0, bytesRead);
						}

						// Flush
						requestStream.Flush();
					}

					/*
                     * Get the response from the service.
                     */
					using (WebResponse response = request.GetResponse())
					{
						LogControl.Write("[RECOGNITION] : HttpWebResponse.StatusCode : " + ((HttpWebResponse)response).StatusCode);

						using (StreamReader sr = new StreamReader(response.GetResponseStream()))
						{
							responseString = sr.ReadToEnd();
						}

						LogControl.Write("[RECOGNITION] : Response" + responseString);
						return responseString;
					}
				}
			}
			catch (Exception ex)
			{
				LogControl.Write("[RECOGNITION] : ERROR | " + ex.Message);
			}
			return null;
		}
	}
}
