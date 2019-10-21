using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Test1
{
	class Program
	{
		static void Main(string[] args)
		{
			var inputPath = AppDomain.CurrentDomain.BaseDirectory + @"files\original.bin";
			var outputPath = AppDomain.CurrentDomain.BaseDirectory + @"files\decoded.bin";

			var binaryDecoder = new BinaryDecoder(inputPath,outputPath);
			binaryDecoder.init();
		}
	}
}
