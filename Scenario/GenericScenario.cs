using System;
using Modem;
using RunControl;
using STT;
using TTS;
using GPIO;

namespace Scenario
{
	public class GenericScenario
	{
		protected RecognitionCognitive stt;
		protected CognitiveAccess tts;
		protected SMSHandler smsHandler;

		public virtual void Start()
		{
			GPIOControl.SetLed(GPIOControl.Mode.Reflexion);
			tts = new CognitiveAccess();
			stt = new RecognitionCognitive();
			smsHandler = new SMSHandler();
			GPIOControl.SetLed(GPIOControl.Mode.StandBy);
			tts.Say("Bonjour, je suis Loic");
			ScenarioOne one = new ScenarioOne(stt, tts, smsHandler);
			one.Start();

			ScenarioTwo two = new ScenarioTwo(stt, tts, smsHandler);
			two.Start();
		}
	}
}
