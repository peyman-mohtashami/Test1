using NUnit.Framework;
using Test1;

namespace Test1.Test
{
	public class BinaryDecoderTests
	{
		[Test]
		public void BinaryDecode_GetDefinedArrayInExample_ReturnsCorrectArray()
		{
			// Arrange
			var binaryDecoder = new BinaryDecoder("", "");
			var inputArray = new byte[]{0x00,0x61,0x01,0x01,0x00,0x62,0x03,0x02,0x03,0x03};
			// Act
			var result = binaryDecoder.BinaryDecode(inputArray);

			// Assert
			var expectedResult = new byte[] { 0x61,0x61,0x62,0x61,0x61,0x62,0x61,0x61 };
			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[Test]
		public void BinaryDecode_GetDefinedArrayInExampleWithErrorInFirstPair_ReturnsArrayWithFirstByte0x3F()
		{
			// Arrange
			var binaryDecoder = new BinaryDecoder("", "");
			var inputArray = new byte[] { 0x01, 0x61, 0x01, 0x01, 0x00, 0x62, 0x03, 0x02, 0x03, 0x03 };
			// Act
			var result = binaryDecoder.BinaryDecode(inputArray);

			// Assert
			var expectedResult2 = new byte[] { 0x3F, 0x3F, 0x62, 0x3F, 0x3F, 0x62, 0x3F, 0x3F };
			Assert.That(result, Is.EqualTo(expectedResult2));
		}
	}
}