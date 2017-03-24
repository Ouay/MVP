using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTS;

namespace MVP
{
    class Program
    {
        static void Main(string[] args)
        {
			CognitiveAccess a = new CognitiveAccess();
			a.Say("Hi");
        }
    }
}
