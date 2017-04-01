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
			ScrollWhite,
			DimmingWhite,
			ScrollGreen,
			Yellow
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
			LogControl.Write("[GPIOCONTROL] : change color");
			try
			{
				if (Init.WiringPiSetup() != -1)
				{
					GPIO.pinMode(_first, (int)GPIO.GPIOpinmode.Output);
					GPIO.pinMode(_second, (int)GPIO.GPIOpinmode.Output);
					GPIO.pinMode(_third, (int)GPIO.GPIOpinmode.Output);

					switch (_mode)
					{
						case Mode.ScrollWhite:
							GPIO.digitalWrite(_first, (int)State.LOW);
							GPIO.digitalWrite(_second, (int)State.LOW);
							GPIO.digitalWrite(_third, (int)State.LOW);
							Console.WriteLine("ScrollWhite");
							break;
						case Mode.DimmingWhite:
							GPIO.digitalWrite(_first, (int)State.LOW);
							GPIO.digitalWrite(_second, (int)State.HIGH);
							GPIO.digitalWrite(_third, (int)State.LOW);
							break;
						case Mode.ScrollGreen:
							GPIO.digitalWrite(_first, (int)State.LOW);
							GPIO.digitalWrite(_second, (int)State.HIGH);
							GPIO.digitalWrite(_third, (int)State.HIGH);
							Console.WriteLine("ScrollGreen");
							break;
						case Mode.Yellow:
							GPIO.digitalWrite(_first, (int)State.LOW);
							GPIO.digitalWrite(_second, (int)State.LOW);
							GPIO.digitalWrite(_third, (int)State.HIGH);
							break;
						default:
							GPIO.digitalWrite(_first, (int)State.HIGH);
							GPIO.digitalWrite(_second, (int)State.LOW);
							GPIO.digitalWrite(_third, (int)State.HIGH);
							Console.WriteLine("Default");
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
			Thread.Sleep(5000);
		}
	}
}
