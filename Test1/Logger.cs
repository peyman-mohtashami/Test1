using System;
using System.IO;

namespace Test1
{
	public class Logger
	{
		public const int Message = 0;
		public const int Error = 1;

		public static void Log(string message, int type = Message)
		{
			Console.ForegroundColor = ConsoleColor.Cyan;

			if (type == Error)
			{
				Console.ForegroundColor = ConsoleColor.Red;
			}

			Console.WriteLine(type == Message ? "log: {0:hh:mm:ss t z} {1}" : "err: {0:hh:mm:ss t z} {1}",
					DateTime.Now,
					message);
			Console.ResetColor();
		}
	}
}