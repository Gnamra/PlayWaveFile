using System;
using System.IO;
using static PlayWaveFile.WaveFileStream;

namespace PlayWaveFile
{
    class AudioFileLoader
    {
        public FormatChunkFormat FormatInformation { get; set; }

        private readonly BinaryReader binReader;
        private string CurrentChunkId { get; set; }
        private int CurrentChunkSize { get; set; }
        private int CurrentChunkStart { get; set; }

        public AudioFileLoader(string pathToFile)
        {
            binReader = new BinaryReader(File.OpenRead(pathToFile));
            CurrentChunkId = "";
            CurrentChunkSize = 0;
        }

        private void NextChunk()
        {
            // Add check to see if at the start of a chunk or not

            CurrentChunkStart = (int)binReader.BaseStream.Position;
            CurrentChunkId = new string(binReader.ReadChars(4));

            if (CurrentChunkId.Equals("RIFF"))
            {
                CurrentChunkSize = binReader.ReadInt32();
                var type = new string(binReader.ReadChars(4));
            }
            else
            {
                // Doesn't account for pad bytes
                CurrentChunkSize = binReader.ReadInt32();
                binReader.ReadBytes(CurrentChunkSize);
            }
        }

        public FormatChunkFormat GetFormatData()
        {
            while (!CurrentChunkId.Equals("fmt ")) { NextChunk(); };
            binReader.BaseStream.Position = CurrentChunkStart + 8;

            FormatChunkFormat data = new FormatChunkFormat
            {
                ChunkId = CurrentChunkId,
                ChunkSize = CurrentChunkSize,
                AudioFormat = binReader.ReadInt16(),
                NumberOfChannels = binReader.ReadInt16(),
                SampleRate = binReader.ReadInt32(),
                ByteRate = binReader.ReadInt32(),
                BlockAlign = binReader.ReadInt16(),
                BitsPerSample = binReader.ReadInt16()
            };

            Console.WriteLine("========= FORMAT CHUNK =========");
            Console.WriteLine("0: Chunk id: " + new string(data.ChunkId));
            Console.WriteLine($"4: File size: {data.ChunkSize}");
            Console.WriteLine($"6: Audio format: {data.AudioFormat}");
            Console.WriteLine($"8: Channels: {data.NumberOfChannels}");
            Console.WriteLine($"12: Sample rate: {data.SampleRate}");
            Console.WriteLine($"16: Byte rate: {data.ByteRate}");
            Console.WriteLine($"18: Block alignment: {data.BlockAlign}");
            Console.WriteLine($"20: Bits per sample: {data.BitsPerSample}");
            return data;
        }
        public DataChunkFormat GetAudioData()
        {
            CurrentChunkStart = (int)binReader.BaseStream.Position;
            while (!CurrentChunkId.Equals("data")) { NextChunk(); };
            binReader.BaseStream.Position = CurrentChunkStart;
            DataChunkFormat data = new DataChunkFormat
            {
                ChunkID = CurrentChunkId,
                ChunkSize = CurrentChunkSize,
                AudioData = binReader.ReadBytes(CurrentChunkSize)
            };
            Console.WriteLine("========= DATA CHUNK =========");
            Console.WriteLine("0: Chunk id: " + data.ChunkID);
            Console.WriteLine($"4: Chunk size: {data.ChunkSize}");
            Console.WriteLine($"8: Chunk Data: {data.AudioData}");
            return data;
        }
       
    }
}
