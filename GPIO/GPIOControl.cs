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
		/// <summary>
		/// Mode of lightning for the LED circle
		/// </summary>
		public enum Mode
		{
			Boot,		//Should be used only on boot or maybe also in update
			Reflexion,	//While reflecting for a while, not for small reflexion
			Help,		//When rescue is coming
			StandBy,	//no light, most of the time
			Speak		//LightUp before speaking
		}

		/// <summary>
		/// GPIO States
		/// </summary>
		public enum State
		{
			LOW, HIGH
		}

		/// <summary>
		/// Pin that used for controlling the leds through the arduino
		/// </summary>
		const int _first = 7;
		const int _second = 0;
		const int _third = 2;

		/// <summary>
		/// A bool to check if GPIO are accessible, for PC debugging support. 
		/// </summary>
		public static bool IsGPIOEnabled = false;

		public static void SetLed(Mode _mode)
		{
			if (IsGPIOEnabled)
				LightMode(_mode);
			else
				PrintMode(_mode);
		}

		/// <summary>
		/// For Windows dev purpose
		/// </summary>
		/// <param name="mode"></param>
		private static void PrintMode(Mode mode)
		{
			switch (mode)
			{
				case Mode.Reflexion:
					LogControl.Write("[GPIOCONTROL] : Set Mode Reflexion");
					break;
				case Mode.StandBy:
					LogControl.Write("[GPIOCONTROL] : Set Mode StandBy");
					break;
				case Mode.Help:
					LogControl.Write("[GPIOCONTROL] : Set Mode Help");
					break;
				case Mode.Speak:
					LogControl.Write("[GPIOCONTROL] : Set Mode Speak");
					break;
				case Mode.Boot:
					LogControl.Write("[GPIOCONTROL] : Set Mode Boot");
					break;
			}
		}

		/// <summary>
		/// Handle the activation of the correct GPIO pin
		/// </summary>
		/// <param name="_mode">Enum for the selection mode</param>
		private static void LightMode(Mode _mode)
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
			catch (Exception e)
			{
				LogControl.Write("[GPIOCONTROL] : Error | " + e.Message);
			}
			Thread.Sleep(50);
		}
	}
}
