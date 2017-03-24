using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTS
{
	/// <summary>
	/// Generic event args
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class GenericEventArgs<T> : EventArgs
	{
		/// <summary>
		/// Initialize a new instance of the genericeventargs
		/// </summary>
		/// <param name="eventData">The event data</param>
		public GenericEventArgs(T eventData)
		{
			this.EventData = eventData;
		}

		/// <summary>
		/// Gets the event data
		/// </summary>
		public T EventData { get; private set; }
	}
}
