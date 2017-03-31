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
			LogControl.Write("[[SCENARIO 2] START]");
			stt = _stt;
			tts = _tts;
			smsHandler = _sms;
		}
	}
}
