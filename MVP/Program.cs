using STT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTS;
using Sound;
using Scenario;
using Modem;
using ExternalGPIO;

namespace MVP
{
    class Program
    {

        static void Main(string[] args)
        {
			//GenericScenario scenario = new GenericScenario();
			//scenario.Start();
			LedControl led = new LedControl();
			led.SetColor(LedControl.Mode.ScrollWhite);
        }
	}
}
