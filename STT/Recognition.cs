using NAudio.Wave;
using Syn.Speech.Api;
using Syn.Speech.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace STT
{
	public class Recognition
	{
		public WaveInEvent waveInStream;
		private static StreamSpeechRecognizer _recognizer;
		WaveFileWriter writer;
		private MemoryStream mem;


		public Recognition()
		{
			waveInStream = new WaveInEvent()
			{
				NumberOfBuffers = 2,
				WaveFormat = new WaveFormat(16000, 1)
			};
			mem = new MemoryStream();
			writer = new WaveFileWriter(mem, waveInStream.WaveFormat);
			waveInStream.DataAvailable += WaveInStream_DataAvailable; ;
			var modelPath = Path.Combine(Directory.GetCurrentDirectory(), "VoiceModel/");
			var dictionaryPath = Path.Combine(modelPath, "cmudict-en-us.dict");
			var languageModelPath = Path.Combine(modelPath, "en-us.lm.dmp");
			var configuration = new Configuration
			{
				AcousticModelPath = modelPath,
				DictionaryPath = dictionaryPath,
				LanguageModelPath = languageModelPath,
				UseGrammar = false
			};
			_recognizer = new StreamSpeechRecognizer(configuration);
		}

		private void WaveInStream_DataAvailable(object sender, WaveInEventArgs e)
		{
			writer.Write(e.Buffer, 0, e.BytesRecorded);
		}

		public void StartRecording()
		{
			Console.WriteLine("Start");
			waveInStream.NumberOfBuffers = 2;
			waveInStream.StartRecording();
			Console.ReadKey();
			recognize();
		}

		private void recognize()
		{
			waveInStream.StopRecording();
			mem.Position = 0;
			_recognizer.StartRecognition(mem, new TimeFrame(mem.Length));
			SpeechResult result = _recognizer.GetResult();
			_recognizer.StopRecognition();
			Console.WriteLine("result: " + result.GetHypothesis());
			Console.ReadKey();
			Console.ReadKey();
		}
	}
}
