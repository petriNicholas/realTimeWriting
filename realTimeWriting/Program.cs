using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

class Program
{
	static void realTimeWriting(string fileName, float delay = 0.05f, int error = 99)
	{
		if (!File.Exists(fileName))
		{
			Console.WriteLine("file name doesn't match any files");
			return;
		}

		try
		{
			using (StreamReader sr = new StreamReader(fileName))
			{
				int character;
				Random rnd = new Random();
				double cpm = (36.27 * 4.78) / 60;

				while ((character = sr.Read()) != -1)
				{
					if(error > rnd.Next(100))
					{
						Mistake(character, cpm);
					}

					Thread.Sleep((int)(100 / (cpm * rnd.NextDouble())));

					Console.Write((char)character);
				}
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error reading file {fileName}: " + ex);
		}
	}

	static void Mistake(int character, double cpm)
	{
		Random rnd = new Random();
		Console.Write((char)(character + rnd.Next(-15, 15)));
		Thread.Sleep((int)(100 / (cpm * rnd.NextDouble())));
		Console.Write("\b");
	}

	public static void Main(string[] args)
	{
		if (args.Length < 1)
		{
			Console.WriteLine("no args");
			return;
		}

		string fileName = args[0];
		float delay = 0.5f;
		if (float.TryParse(args[1], out delay))
		{
			Console.Write("delay zly format");
		}

		realTimeWriting(fileName, delay);
	}
}