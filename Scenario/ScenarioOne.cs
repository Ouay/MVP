using System;
using Modem;
using RunControl;
using STT;
using TTS;
using System.Collections.Generic;
using GPIO;
using Sound;
using System.IO;
using System.Reflection;
using System.Threading;

namespace Scenario
{
	public class ScenarioOne : GenericScenario
	{
		public ScenarioOne(RecognitionCognitive _stt, CognitiveAccess _tts, SMSHandler _sms, SoundPlayer _sound)
		{
			LogControl.Write("[SCENARIO 1] :  Loaded]");
			stt = _stt;
			tts = _tts;
			smsHandler = _sms;
			soundPlayer = _sound;
		}

		public override void Start()
		{
			LogControl.Write("[SCENARIO 1] : Start");
			//Vérifie si ya un appel a l'aide
			string response = Listen();
			GPIOControl.SetLed(GPIOControl.Mode.Reflexion);
			//smsHandler.SendSMS("+41786268658", response);
			smsHandler.SendSMS("+41789476812", response);
			response = WaitSMS();
			GPIOControl.SetLed(GPIOControl.Mode.Speak);
			tts.Say(response);
			Thread.Sleep(1000);
			soundPlayer.Play(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "/say.wav");
			GPIOControl.SetLed(GPIOControl.Mode.Help);
		}

		private string WaitSMS()
		{
			try
			{
				LogControl.Write("[SCENARIO 1] : Attente d'un SMS");
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
			catch(IndexOutOfRangeException e)
			{
				LogControl.Write("Out of range");
				return "";
			}
		}

		private string ParseContent(List<SMSContent> list)
		{
			foreach(SMSContent s in list)
			{
				//if (s.Number == "+41789476812")
				if(s.Number == "+41786268658")
					return s.Message;
			}
			return string.Empty;
		}

		private string Listen()
		{
			LogControl.Write("[SCENARIO 1] : listening");
			string phrase = @"besoin aide";
			bool search = true;
			string response= string.Empty;
			while (search)
			{
				if(!stt.Record())
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
			return "J'ai besoin d'aide, je suis tombé";
		}
	}
}
