namespace Test1
{
	public interface IArrayBinaryReaderWriter
	{
		byte[] Read(string path);
		bool Write(byte[] array, string path);
	}
}