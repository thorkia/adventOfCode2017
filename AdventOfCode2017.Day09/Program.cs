using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace AdventOfCode2017.Day09
{
	class Program
	{
		static void Main(string[] args)
		{
			var tests = new List<string>
			{
				"{}",
				"{{{}}}",
				"{{},{}}",
				"{{{},{},{{}}}}",
				"{<a>,<a>,<a>,<a>}",
				"{{<ab>},{<ab>},{<ab>},{<ab>}}",
				"{{<!!>},{<!!>},{<!!>},{<!!>}}",
				"{{<a!>},{<a!>},{<a!>},{<ab>}}"
			};

			
			foreach (var test in tests)
			{
				int testGarbage = 0;
				string cleaned = RemoveGarbage(RemoveIgnores(test), out testGarbage);

				Console.WriteLine(cleaned + " -> " + CountPoints(cleaned) + " -> " + testGarbage);

			}

			
			int garbageCount = 0;
			var cleanedInput = RemoveGarbage(RemoveIgnores(File.ReadAllText("Input.txt")), out garbageCount);
			Console.WriteLine(CountPoints(cleanedInput));
			Console.WriteLine(garbageCount);
			

			Console.ReadLine();
		}

		private static string RemoveIgnores(string source)
		{			
			while (source.Contains("!"))
			{
				var chars = new char[source.Length];
				int charLocation = 0;

				for (int i = 0; i < source.Length; i++)
				{
					
					if (source[i] != '!')
					{
						chars[charLocation] = source[i];
						charLocation++;
					}
					else
					{
						i++;
					}
				}

				source = new string(chars).Trim();
			}

			return source;
		}

		private static string RemoveGarbage(string source, out int amountOfGarbage)
		{
			amountOfGarbage = 0;

			while (source.Contains("<"))
			{
				var chars = new char[source.Length];
				int charLocation = 0;

				for (int i = 0; i < source.Length; i++)
				{
					if (source[i] != '<')
					{
						chars[charLocation] = source[i];
						charLocation++;
					}
					else
					{
						//Empty loop to search for end point
						while (source[++i] != '>')
						{
							amountOfGarbage++;
						};
					}
				}

				source = new string(chars);
			}

			var commaChars = new char[source.Length];
			int commaLocation = 0;
			for (int i = 0; i < source.Length; i++)
			{
				if (source[i] == ',' && source[i + 1] != '{')
				{
				}
				else
				{
					commaChars[commaLocation] = source[i];
					commaLocation++;
				}
			}

			return new string(commaChars).Replace(" ", "").Trim();
		}

		private static int CountPoints(string source)
		{
			int points = 0;
			int currentLevel = 0;

			for (int i = 0; i < source.Length; i++)
			{
				if (source[i] == '{')
				{
					currentLevel++;
					points += currentLevel;
				}

				if (source[i] == '}')
				{
					currentLevel--;
				}
			}

			return points;
		}
	}
}
