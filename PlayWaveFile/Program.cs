using NAudio.Wave;
using System;
using System.IO;

namespace PlayWaveFile
{

    class Program
    {
        static void Main(string[] args)
        {

            Console.Clear();

            WaveFileStream waveProvider = new WaveFileStream("song.wav");

            var waveOut = new WaveOutEvent
            {
                NumberOfBuffers = 2
            };
            waveOut.Init(waveProvider);
            waveOut.Play();

            Console.WriteLine("Playing song!");
            int cx = Console.CursorLeft;
            int cy = Console.CursorTop;

            Console.CursorVisible = false;

            char[] input = new char[30];
            int inputPos = 0;

            while (waveOut.PlaybackState == PlaybackState.Playing)
            {
                if(Console.KeyAvailable)
                {

                    var key = Console.ReadKey(true);
                    if(key.Key == ConsoleKey.Backspace && inputPos != 0)
                    {
                        input[inputPos--] = ' ';
                    }
                    else if(key.Key == ConsoleKey.Enter && inputPos != 0)
                    {
                        if(int.TryParse(new string(input), out int result))
                        {
                            waveProvider.Position = waveProvider.FormatChunkData.ByteRate * result;
                        }
                        inputPos = 0;
                        Array.Clear(input, 0, 30);
                    }
                    else
                    {
                        input[inputPos] = key.KeyChar;
                        if (inputPos <= 28)
                            inputPos++;

                    }
                }
                Console.SetCursorPosition(0, cy + 1);
                Console.Write(input);
                Console.SetCursorPosition(cx, cy);
                Console.Write("Playing for {0}s", waveProvider.Position / waveProvider.FormatChunkData.ByteRate);
            }
        }
    }
}
