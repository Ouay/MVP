using System;
using Modem;
using RunControl;
using STT;
using TTS;

namespace Scenario
{
	public class GenericScenario
	{
		protected RecognitionCognitive stt;
		protected CognitiveAccess tts;
		protected SMSHandler smsHandler;

		public virtual void Start()
		{
			
			tts = new CognitiveAccess();
			stt = new RecognitionCognitive();
			smsHandler = new SMSHandler();
			ScenarioOne one = new ScenarioOne(stt, tts, smsHandler);
			one.Start();

			ScenarioTwo two = new ScenarioTwo(stt, tts, smsHandler);
			two.Start();
		}
	}
}
