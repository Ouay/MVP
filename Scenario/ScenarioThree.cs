using Modem;
using RunControl;
using STT;
using TTS;

namespace Scenario
{
	public class ScenarioThree : GenericScenario
	{
		public ScenarioThree()
		{
			LogControl.Write("[[SCENARIO 1] START]");
			stt = new RecognitionCognitive();
			tts = new CognitiveAccess();
			smsHandler = new SMSHandler();
		}
	}
}
