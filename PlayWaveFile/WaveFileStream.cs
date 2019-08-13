using NAudio.Wave;
using System;

namespace PlayWaveFile
{
    partial class WaveFileStream : WaveStream
    {

        private readonly string readLock = "";

        public FormatChunkFormat FormatChunkData { get; set; }
        public DataChunkFormat DatacChunkData { get; set; }


        public WaveFileStream(string pathToFile)
        {
            AudioFileLoader loader = new AudioFileLoader(pathToFile);
            FormatChunkData = loader.GetFormatData();
            DatacChunkData = loader.GetAudioData();
            WaveFormat = new WaveFormat(FormatChunkData.SampleRate, FormatChunkData.NumberOfChannels);

            //Console.Clear();
            //Console.WriteLine("4: Chunk id: " + new string(ChunkId));
            //Console.WriteLine($"8: File size: {ChunkSize}");
            //Console.WriteLine("12: Format: " + new string(Format));
            //Console.WriteLine("16: Sub chunk 1 ID: " + new string(Subchunk1ID));
            //Console.WriteLine($"20: Sub chunk 1 size: {Subchunk1Size}");
            //Console.WriteLine($"22: Audio format: {AudioFormat}");
            //Console.WriteLine($"24: Channels: {NumberOfChannels}");
            //Console.WriteLine($"28: Sample rate: {SampleRate}");
            //Console.WriteLine($"32: Byte rate: {ByteRate}");
            //Console.WriteLine($"34: Block alignment: {BlockAlign}");
            //Console.WriteLine($"36: Bits per sample: {BitsPerSample}");

            //Console.WriteLine($"40: Sub chunk 2 id: " + new string(subchunk2id));
            //Console.WriteLine($"44: Sub chunk 2 size {subchunk2size}");
            //Console.WriteLine($"48: info header " + new string(infoheader));
            //Console.WriteLine($"52: info tag " + new string(infoTag));
            //Console.WriteLine($"56: info data {infoData}");
            //Console.WriteLine($"60: info encoder " + new string(infoData2));
            //Console.WriteLine($"70: info version " + new string(infoData3));
            //Console.WriteLine($"74: subchunk 3 id " + new string(subchunk3id));
            //Console.WriteLine($"78: subchunk 3 size {subchunk3Size}");


            //data = subchunk3Data;
            //Length = data.LongLength;
            //Position = 0;

        }

        public override WaveFormat WaveFormat { get; }
        public override long Length { get; }
        public override long Position { get; set; }

        public override int Read(byte[] buffer, int offset, int count)
        {
            lock (readLock)
            {
                Buffer.BlockCopy(DatacChunkData.AudioData, (int)Position, buffer, offset, count);
                Position += buffer.Length;
                return buffer.Length;
            }
        }
    }
}
