using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2017.Day10;

namespace AdventOfCode2017.Day14
{
	class Program
	{
		static void Main(string[] args)
		{
			string testInputBase = "flqrgnkx";
			List<string> testRowStates = new List<string>();
			for (int i = 0; i < 128; i++)
			{
				string hash = PerformHash(testInputBase + "-" + i);

				StringBuilder binaryString = new StringBuilder();
				//Convert to Binary
				foreach (var item in hash)
				{
					byte b = byte.Parse(item.ToString(), NumberStyles.HexNumber);

					var s = Convert.ToString(b, 2);
					s = s.PadLeft(4, '0');
					binaryString.Append(s);
				}
				testRowStates.Add(binaryString.ToString());

			}

			var testCount = testRowStates.Sum( row => row.Replace("0", "").Length);
			Console.WriteLine(testCount);

			string inputBase = "ffayrhll";
			List<string> rowStates = new List<string>();

			for (int i = 0; i < 128; i++)
			{
				string hash = PerformHash(inputBase + "-" + i);

				StringBuilder binaryString = new StringBuilder();
				//Convert to Binary
				foreach (var item in hash)
				{
					byte b = byte.Parse(item.ToString(), NumberStyles.HexNumber);

					var s = Convert.ToString(b, 2);
					s = s.PadLeft(4, '0');
					binaryString.Append(s);
				}
				rowStates.Add(binaryString.ToString());
			}
			var count = rowStates.Sum(row => row.Replace("0", "").Length);
			Console.WriteLine(count);


			//Part 2 - count contiguous regions


			Console.ReadLine();
		}

		static string PerformHash(string source)
		{
			var ring = new Ring();

			var part2Operations = ASCIIEncoding.ASCII.GetBytes(source).Select(b => (int)b).ToList();

			part2Operations.AddRange(new List<int>() { 17, 31, 73, 47, 23 });

			int currentPosition = 0;
			int skipSize = 0;
			for (int x = 0; x < 64; x++)
			{
				var newLoc = PerformOperations(ring, part2Operations, currentPosition, skipSize);
				currentPosition = newLoc.Item1;
				skipSize = newLoc.Item2;
			}

			return ring.GetDenseHash();
		}

		static Tuple<int, int> PerformOperations(Ring ring, List<int> operations, int currentPosition, int skipSize)
		{
			foreach (var operation in operations)
			{
				var items = ring.GetItems(currentPosition, operation);
				items.Reverse();
				ring.SetItems(currentPosition, items);

				currentPosition += operation + skipSize;
				skipSize++;
			}

			return new Tuple<int, int>(currentPosition, skipSize);
		}
	}

	
}
