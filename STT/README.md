# Purposes 

This module performs any Speach-To-Text processes. From recording/streaming the microphone to requesting api to convert the input.

## Recording
There are multiple solutions to get the audio, but lots of them doesn't work on the Raspberry Pi. At the end, it appears that 2 solutions may be used: 

- Recording and audio file
  - With NAudio
  - With terminal process
- Having an external program that stream microphone to a local server and this program listen to it.

## Dependencies

Nuget Package | Remarks
--------------|-------
NAudio | Recording
SynSpeech | Offline traduction
Other | blablab
