using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Day5
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine( ProcessInstructions(new int[] {0,3,0,1,-3}));

			var items = File.ReadAllLines("Day5Input.txt").Select(x => int.Parse(x)).ToArray();

			Console.WriteLine(ProcessInstructions(items));


			//Part 2
			Console.WriteLine(ProcessPart2Instructions(new int[] { 0, 3, 0, 1, -3 }));

			var itemsPart2 = File.ReadAllLines("Day5Input.txt").Select(x => int.Parse(x)).ToArray();
			Console.WriteLine(ProcessPart2Instructions(itemsPart2));

			Console.ReadLine();
		}

		private static int ProcessInstructions(int[] offsets)
		{
			int stepCount = 0;
			int position = 0;

			while (position < offsets.Length)
			{
				//Find how many offsets to jump
				int stepAdjustment = offsets[position];
				
				//Since we visited this position, increment it by one
				offsets[position]++;

				//Jump the number of offsets
				position += stepAdjustment;
				//Increase our step count
				stepCount++;
			}

			return stepCount;
		}

		private static int ProcessPart2Instructions(int[] offsets)
		{
			int stepCount = 0;
			int position = 0;

			while (position < offsets.Length)
			{
				//Find how many offsets to jump
				int stepAdjustment = offsets[position];

				//Since we visited this position, increment it by one
				if (offsets[position] >= 3)
				{
					offsets[position]--;
				}
				else
				{
					offsets[position]++;
				}
				
				//Jump the number of offsets
				position += stepAdjustment;
				//Increase our step count
				stepCount++;
			}

			return stepCount;
		}
	}
}
