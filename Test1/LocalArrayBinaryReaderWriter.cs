using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test1
{
	public class LocalArrayBinaryReaderWriter : IArrayBinaryReaderWriter
	{
		public byte[] Read(string inputPath)
		{
			try
			{
				byte[] fileData;
				using (var fs = File.OpenRead(inputPath))
				{
					using (var binaryReader = new BinaryReader(fs))
					{
						fileData = binaryReader.ReadBytes((int) fs.Length);
					}

					Logger.Log("Read file: " + inputPath);
				}

				return fileData;
			}
			catch (Exception e)
			{
				Logger.Log(e.Message, Logger.Error);
				return null;
			}
		}

		public bool Write(byte[] array, string outputPath)
		{
			try
			{
				File.WriteAllBytes(outputPath, array);
				Logger.Log("Write file: " + outputPath);
				return true;
			}
			catch (Exception e)
			{
				Logger.Log(e.Message, Logger.Error);
				return false;
			}
		}
	}
}
