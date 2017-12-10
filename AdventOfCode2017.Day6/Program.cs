using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Day6
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine(StepsToRepeat(new int[] { 0, 2, 7, 0 }));

			var sourceArray = new int[] {4, 1, 15, 12, 0, 9, 9, 5, 5, 8, 7, 3, 14, 5, 12, 3};
			Console.WriteLine(StepsToRepeat(sourceArray));

			//Part 2 - since we need to find out how many steps until this repeats, just run it again from the end point to get the new count
			Console.WriteLine(StepsToRepeat(sourceArray));

			Console.ReadLine();
		}

		static int StepsToRepeat(int[] numbers)
		{
			HashSet<string> numberSets = new HashSet<string>();

			int stepCount = 0;

			do
			{
				//Record the current state before taking a step
				numberSets.Add(string.Join(string.Empty, numbers));
				//Take a step and redistribute the numbers
				stepCount++;
				SpreadNumbers(numbers);				
			} while (!numberSets.Contains(string.Join(string.Empty, numbers))); //If the new set is in the system, we stop


			return stepCount;
		}

		static void SpreadNumbers(int[] numbers)
		{
			int numberToSpread = numbers.Max();
			int position = Array.IndexOf(numbers, numberToSpread);

			//Set the value at position to 0 - and start at the next item and add
			numbers[position] = 0;
			
			while (numberToSpread > 0)
			{
				position++;
				position = position % numbers.Length;

				numbers[position]++;
				numberToSpread--;
			}

		}
	}
}
