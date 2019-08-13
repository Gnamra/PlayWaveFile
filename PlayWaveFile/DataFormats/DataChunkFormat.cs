namespace PlayWaveFile
{
    partial class WaveFileStream
    {
        public struct DataChunkFormat
        {
            public string ChunkID { get; set; }
            public int ChunkSize { get; set; }
            public byte[] AudioData { get; set; }
        }
    }
}
