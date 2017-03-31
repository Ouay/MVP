using System;
using Modem;
using RunControl;
using STT;
using TTS;

namespace Scenario
{
	public class ScenarioTwo : GenericScenario
	{
		public ScenarioTwo(RecognitionCognitive _stt, CognitiveAccess _tts, SMSHandler _sms)
		{
			LogControl.Write("[SCENARIO 2] : Loaded]");
			stt = _stt;
			tts = _tts;
			smsHandler = _sms;
		}

		public override void Start()
		{
			LogControl.Write("[SCENARIO 2] : Start");
			string response = WaitSMS();
		}

		private string WaitSMS()
		{
			
		}
	}
}
