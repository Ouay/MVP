using RunControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GPIO
{
	public class GPIOControl
	{
		public enum Mode
		{
			Boot,
			Reflexion,
			Help,
			StandBy,
			Speak
		}

		public enum State
		{
			LOW, HIGH
		}

		const int _first = 7;
		const int _second = 0;
		const int _third = 2;

		public static void SetLed(Mode _mode)
		{
			try
			{
				if (Init.WiringPiSetup() != -1)
				{
					GPIO.pinMode(_first, (int)GPIO.GPIOpinmode.Output);
					GPIO.pinMode(_second, (int)GPIO.GPIOpinmode.Output);
					GPIO.pinMode(_third, (int)GPIO.GPIOpinmode.Output);

					switch (_mode)
					{
						case Mode.Reflexion:
							GPIO.digitalWrite(_first, (int)State.LOW);
							GPIO.digitalWrite(_second, (int)State.LOW);
							GPIO.digitalWrite(_third, (int)State.LOW);
							LogControl.Write("[GPIOCONTROL] : Set Mode Reflexion");
							break;
						case Mode.StandBy:
							GPIO.digitalWrite(_first, (int)State.LOW);
							GPIO.digitalWrite(_second, (int)State.HIGH);
							GPIO.digitalWrite(_third, (int)State.LOW);
							LogControl.Write("[GPIOCONTROL] : Set Mode StandBy");
							break;
						case Mode.Help:
							GPIO.digitalWrite(_first, (int)State.LOW);
							GPIO.digitalWrite(_second, (int)State.HIGH);
							GPIO.digitalWrite(_third, (int)State.HIGH);
							LogControl.Write("[GPIOCONTROL] : Set Mode Help");
							break;
						case Mode.Speak:
							GPIO.digitalWrite(_first, (int)State.LOW);
							GPIO.digitalWrite(_second, (int)State.LOW);
							GPIO.digitalWrite(_third, (int)State.HIGH);
							LogControl.Write("[GPIOCONTROL] : Set Mode Speak");
							break;
						case Mode.Boot:
							GPIO.digitalWrite(_first, (int)State.HIGH);
							GPIO.digitalWrite(_second, (int)State.LOW);
							GPIO.digitalWrite(_third, (int)State.LOW);
							LogControl.Write("[GPIOCONTROL] : Set Mode Boot");
							break;
					}
				}
				else
				{
					LogControl.Write("[GPIOCONTROLER] : Init Failed");
				}
			}
			catch(Exception e)
			{
				LogControl.Write("[GPIOCONTROL] : Error | " + e.Message);
			}
			Thread.Sleep(500);
		}
	}
}
