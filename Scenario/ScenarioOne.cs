using System;
using Modem;
using RunControl;
using STT;
using TTS;
using System.Collections.Generic;

namespace Scenario
{
	public class ScenarioOne : GenericScenario
	{
		public ScenarioOne(RecognitionCognitive _stt, CognitiveAccess _tts, SMSHandler _sms)
		{
			LogControl.Write("[[SCENARIO 1] START]");
			stt = _stt;
			tts = _tts;
			smsHandler = _sms;
			StartSimulation();
		}

		private void StartSimulation()
		{
			//Vérifie si ya un appel a l'aide
			string response = Listen();
			smsHandler.SendSMS("", response);
			response = WaitSMS();
			tts.Say(response);
		}

		private string WaitSMS()
		{
			string response = string.Empty;
			bool noResponse = true;
			while (noResponse)
			{
				List<SMSContent> list = smsHandler.ReadSMS();
				response = ParseContent(list);
				if (response != string.Empty)
					noResponse = false;
			}

			return response;
		}

		private string ParseContent(List<SMSContent> list)
		{
			foreach(SMSContent s in list)
			{
				if (s.Number == "+41782882")
					return s.Message;
			}
			return string.Empty;
		}

		private string Listen()
		{
			string phrase = @"besoin aide";
			bool search = true;
			string response= string.Empty;
			while (search)
			{
				stt.Record();
				response = stt.SetupRequest();
				foreach (string s in phrase.Split(' '))
					if (response.Contains(s))
						search = false;
			}
			return response;
		}
	}
}
