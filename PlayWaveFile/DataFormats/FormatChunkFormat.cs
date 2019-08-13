namespace PlayWaveFile
{
    partial class WaveFileStream
    {
        public struct FormatChunkFormat
        {
            public string ChunkId { get; set; }
            public int ChunkSize { get; set; }
            public short AudioFormat { get; set; }
            public short NumberOfChannels { get; set; }
            public int SampleRate { get; set; }
            public int ByteRate { get; set; }
            public short BlockAlign { get; set; }
            public short BitsPerSample { get; set; }
        }
    }
}
