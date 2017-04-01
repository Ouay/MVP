using System;
using Modem;
using RunControl;
using STT;
using TTS;
using GPIO;
using Sound;

namespace Scenario
{
	public class GenericScenario
	{
		protected RecognitionCognitive stt;
		protected CognitiveAccess tts;
		protected SMSHandler smsHandler;
		protected SoundPlayer soundPlayer;

		public virtual void Start()
		{
			GPIOControl.SetLed(GPIOControl.Mode.Reflexion);
			tts = new CognitiveAccess();
			stt = new RecognitionCognitive();
			smsHandler = new SMSHandler();
			soundPlayer = new SoundPlayer();
			GPIOControl.SetLed(GPIOControl.Mode.StandBy);
			//tts.Say("Bonjour, je suis Loic");
			ScenarioOne one = new ScenarioOne(stt, tts, smsHandler, soundPlayer);
			one.Start();

			ScenarioTwo two = new ScenarioTwo(stt, tts, smsHandler, soundPlayer);
			two.Start();
		}
	}
}
