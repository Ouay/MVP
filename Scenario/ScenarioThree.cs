using Modem;
using RunControl;
using STT;
using TTS;

namespace Scenario
{
	public class ScenarioThree : GenericScenario
	{
		public ScenarioThree(RecognitionCognitive _stt, CognitiveAccess _tts, SMSHandler _sms)
		{
			LogControl.Write("[[SCENARIO 3] START]");
			stt = _stt;
			tts = _tts;
			smsHandler = _sms;
		}
	}
}
