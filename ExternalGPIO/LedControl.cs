using Com.Enterprisecoding.RPI.GPIO;
using Com.Enterprisecoding.RPI.GPIO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalGPIO
{
	public class LedControl
	{
		public enum Mode
		{
			ScrollWhite,
			DimmingWhite,
			ScrollGreen,
			Yellow
		}

		public LedControl()
		{
			
		}

		public void SetColor(Mode colorMod)
		{
			WiringPi.Core.PinMode(4, PinMode.Output);
			WiringPi.Core.PinMode(17, PinMode.Output);
			WiringPi.Core.PinMode(27, PinMode.Output);

			switch (colorMod)
			{
				case Mode.ScrollWhite:
					WiringPi.Core.DigitalWrite(7, DigitalValue.Low);
					WiringPi.Core.DigitalWrite(11, DigitalValue.Low);
					WiringPi.Core.DigitalWrite(13, DigitalValue.Low);
					Console.WriteLine("ScrollWhite");
					break;
				case Mode.DimmingWhite:
					WiringPi.Core.DigitalWrite(7, DigitalValue.Low);
					WiringPi.Core.DigitalWrite(11, DigitalValue.High);
					WiringPi.Core.DigitalWrite(13, DigitalValue.Low);
					break;
				case Mode.ScrollGreen:
					WiringPi.Core.DigitalWrite(4, DigitalValue.Low);
					WiringPi.Core.DigitalWrite(11, DigitalValue.High);
					WiringPi.Core.DigitalWrite(13, DigitalValue.High);
					Console.WriteLine("ScrollGreen");
					break;
				case Mode.Yellow:
					WiringPi.Core.DigitalWrite(7, DigitalValue.Low);
					WiringPi.Core.DigitalWrite(11, DigitalValue.High);
					WiringPi.Core.DigitalWrite(13, DigitalValue.High);
					break;
				default:
					WiringPi.Core.DigitalWrite(7, DigitalValue.High);
					WiringPi.Core.DigitalWrite(11, DigitalValue.Low);
					WiringPi.Core.DigitalWrite(13, DigitalValue.High);
					Console.WriteLine("Default");
					break;
			}
		}
	}
}
