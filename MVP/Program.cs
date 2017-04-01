﻿using STT;
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
using GPIO;
using System.Threading;

namespace MVP
{
    class Program
    {

        static void Main(string[] args)
        {
			GPIOControl.SetLed(GPIOControl.Mode.StandBy);
			Thread.Sleep(1000);
			GenericScenario scenario = new GenericScenario();
			scenario.Start();
		}
	}
}
