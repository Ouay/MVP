using RunControl;
using Sound;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TTS
{
	/// <summary>
	/// Gender of the voice
	/// </summary>
	public enum Gender
	{
		Female,
		Male
	}

	/// <summary>
	/// Voice output formats
	/// </summary>
	public enum AudioOutputFormat
	{
		/// <summary>
		/// raw-8khz-8bit-mono-mulaw request output audio format type.
		/// </summary>
		Raw8Khz8BitMonoMULaw,
		/// <summary>
		/// raw-16khz-16bit-mono-pcm request output audio format type.
		/// </summary>
		Raw16Khz16BitMonoPcm,
		/// <summary>
		/// riff-8khz-8bit-mono-mulaw request output audio format type.
		/// </summary>
		Riff8Khz8BitMonoMULaw,
		/// <summary>
		/// riff-16khz-16bit-mono-pcm request output audio format type.
		/// </summary>
		Riff16Khz16BitMonoPcm
	}

	public class CognitiveAccess
	{
		string accessToken;

		public CognitiveAccess()
		{
			LogControl.Write("[TTS] : Starting Authentification");

			Authentification auth = new Authentification(Key.BingSpeech);

			try
			{
				accessToken = auth.GetAccessToken();
				LogControl.Write("[TTS] : Token = " + accessToken);
			}
			catch(Exception ex)
			{
				LogControl.Write("[TTS] : Failed authentification");
				LogControl.Write("[TTS] : ERROR | " + ex.Message);
				return;
			}

			LogControl.Write("[TTS] : Authentification Correct");
		}

		public void Say(string message)
		{
			string requestUri = "https://speech.platform.bing.com/synthesize";

			var cortana = new Synthetise(new Synthetise.InputOptions()
			{
				RequestUri = new Uri(requestUri),
				// Text to be spoken.
				Text = message,
				VoiceType = Gender.Female,
				// Refer to the documentation for complete list of supported locales.
				Locale = "fr-FR",
				// You can also customize the output voice. Refer to the documentation to view the different
				// voices that the TTS service can output.
				VoiceName = "Microsoft Server Speech Text to Speech Voice (fr-FR, Julie, Apollo)",
				// Service can return audio in different output format. 
				OutputFormat = AudioOutputFormat.Riff16Khz16BitMonoPcm,
				AuthorizationToken = "Bearer " + accessToken,
			});

			cortana.OnAudioAvailable += PlayAudio;
			cortana.OnError += ErrorHandler;
			cortana.Speak(CancellationToken.None).Wait();
		}

		/// <summary>
		/// This method is called once the audio returned from the service. 
		/// It will then attempt to save the audio file
		/// </summary>
		/// <param name="sender">The source of the event</param>
		/// <param name="args">The <see cref="cref="GenericEventArgs{Stream}"/> instance containing the event data</param>
		public void PlayAudio(object sender, GenericEventArgs<Stream> args)
		{
			/*The file should be encoded in PCM, with audio format AudioOutputFormat.Riff16Khz16BitMonoPcm.*/
			try
			{
				LogControl.Write("[TTS] : Saving audio file");
				Stream s = args.EventData;
				var fileStream = new
					FileStream(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "/say.wav", FileMode.OpenOrCreate, FileAccess.Write);
				s.CopyTo(fileStream);
				fileStream.Dispose();
				s.Dispose();
			}
			catch(Exception ex)
			{
				LogControl.Write("[TTS] : Saving file failed | ERROR : " + ex.Message);
			}
			LogControl.Write("[TTS] : File Saved");
		}

		public void ErrorHandler(object sender, GenericEventArgs<Exception> e)
		{
			LogControl.Write("[TTS] : Unable to complete the TTS request | " + e.ToString());
		}
	}
}
