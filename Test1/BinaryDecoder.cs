using System;
using System.Collections.Generic;

namespace Test1
{
	public class BinaryDecoder
	{
		private readonly string _inputPath;
		private readonly string _outputPath;
		private LocalArrayBinaryReaderWriter _localArrayBinaryReaderWriter;

		public BinaryDecoder(string inputPath, string outputPath)
		{
			_inputPath = inputPath;
			_outputPath = outputPath;
		}

		/*
		 * Initialize
		 * Put it in separate part from constructor to simplify unit testing
		 */
		public void init()
		{
			_localArrayBinaryReaderWriter = new LocalArrayBinaryReaderWriter();
			var inputArray = BinaryRead();
			if (inputArray != null)
			{
				var decodedArray = BinaryDecode(inputArray);
				BinaryWrite(decodedArray);
				BinaryShow(decodedArray);
			}
		}

		/*
		 * Read binary file and store it in a byte array
		 */
		private byte[] BinaryRead()
		{
			var localArrayBinaryReaderWriter = new LocalArrayBinaryReaderWriter();
			var inputEncodedArray = localArrayBinaryReaderWriter.Read(_inputPath);
			return inputEncodedArray;
		}

		/*
		 * Decode the binary array
		 */
		public byte[] BinaryDecode(byte[] inputArray)
		{
			var outputDecodedList = new List<byte>();

			// Loop over all pairs is encoded binary array
			for (var i = 0; i < inputArray.Length; i += 2)
			{
				// get each pair (2 bytes)
				var pair = new byte[] { inputArray[i], inputArray[i + 1] };

				if (pair[0] == 0)
				{
					// if the first byte of pair equals zero, put the second byte of the pair as a decoded result
					// outputDecodedList
					outputDecodedList.Add(pair[1]);
				}
				else
				{
					// if the first byte of pair is not equal to zero,
					// find the current index in result (outputDecodedList)
					// find the expected index for starting loop
					// find the expected length of the loop
					
					var currentIndex = outputDecodedList.Count;
					var expectedIndex = currentIndex - pair[0];
					var expectedLength = (expectedIndex + pair[1]);
					// if expected index or expected length is not valid
					// it means that the encoded array has some error
					// generate error and add 0x3F to the result (outputDecodedList)
					if (expectedIndex < 0 || expectedLength > outputDecodedList.Count)
					{
						outputDecodedList.Add(0x3F);
						Console.ForegroundColor = ConsoleColor.Yellow;
						Console.WriteLine("log: {0:hh:mm:ss t z} Original file has an invalid pair at pair index {1}",
						DateTime.Now,
						i/2
						);
						Console.ResetColor();
					}
					else
					{
						// if everything is ok, loop over outputDecodedList and add values to the result
						for (var j = expectedIndex; j < expectedLength; j++)
						{
							outputDecodedList.Add(outputDecodedList[j]);
						}
					}
				}
			}

			var outputDecodedArray = outputDecodedList.ToArray();
			return outputDecodedArray;
		}

		private void BinaryWrite(byte[] outputArray)
		{
			_localArrayBinaryReaderWriter.Write(outputArray, _outputPath);
		}

		private void BinaryShow(byte[] outputArray)
		{
			Console.WriteLine();
			var hexString = BitConverter.ToString(outputArray).Replace("-", ",");
			Console.WriteLine("Hex");
			Console.WriteLine(hexString);
			Console.WriteLine();

			Console.WriteLine("Decimal");
			var decimalString = string.Join(",", outputArray);
			Console.WriteLine(decimalString);
			Console.WriteLine();

			Console.WriteLine("String");
			Console.WriteLine(System.Text.Encoding.ASCII.GetString(outputArray));
			Console.WriteLine();
		}
	}
}