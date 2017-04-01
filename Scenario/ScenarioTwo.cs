using System;
using Modem;
using RunControl;
using STT;
using TTS;
using Sound;
using System.IO;
using System.Reflection;

namespace Scenario
{
	public class ScenarioTwo : GenericScenario
	{
		public ScenarioTwo(RecognitionCognitive _stt, CognitiveAccess _tts, SMSHandler _sms, SoundPlayer _sound)
		{
			LogControl.Write("[SCENARIO 2] : Loaded]");
			stt = _stt;
			tts = _tts;
			smsHandler = _sms;
			soundPlayer = _sound;
		}

		public override void Start()
		{
			LogControl.Write("[SCENARIO 2] : Start");
			string response = WaitSMS();
			tts.Say(response);
			soundPlayer.Play(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "/say.wav");
			string toSend = "\"" + Listen() +"\"";
			smsHandler.SendSMS("+41789476812", toSend);
		}

		private string Listen()
		{
			LogControl.Write("[SCENARIO 2] : listening");
			string phrase = @"mal";
			bool search = true;
			string response = string.Empty;
			while (search)
			{
				if (!stt.Record(5))
					continue;
				response = stt.SetupRequest();
				foreach (string s in phrase.Split(' '))
				{
					if (s == null || response == null)
						continue;
					if (response.Contains(s))
						search = false;
				}
			}
			string tmp = response.Split(new string[] { "\"name\":\"", "\",\"lexical" }, StringSplitOptions.RemoveEmptyEntries)[1];
			Console.WriteLine(tmp);
			return tmp;
		}

		private string WaitSMS()
		{
			try
			{
				LogControl.Write("[SCENARIO 2] : Attente d'un SMS");
				string DateSMS = smsHandler.GetDateSMS();
				string response = string.Empty;
				bool noResponse = true;
				while (noResponse)
				{
					response = smsHandler.GetDateSMS();
					if (DateSMS != response)
						noResponse = false;
				}
				LogControl.Write(response);
				string Conten = smsHandler.GetContentSMS();
				string[] counter = Conten.Split(new string[] { "<Content>", "</Content>" }, StringSplitOptions.RemoveEmptyEntries);
				return counter[1];
			}
			catch (IndexOutOfRangeException e)
			{
				LogControl.Write("Out of range");
				return "";
			}
		}
	}
}
