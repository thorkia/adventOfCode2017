using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Day8
{
	class Program
	{
		static void Main(string[] args)
		{
			var testData = File.ReadAllLines("TestData.txt").Select(l => new Instruction(l.Trim())).ToList();

			Dictionary<string, int> testRegistersPart1 = new Dictionary<string, int>();
			int testHighValue = 0;

			testData.ForEach(t =>
			{
				t.PerformOperation(testRegistersPart1);
				if (testRegistersPart1[t.Register] > testHighValue)
				{
					testHighValue = testRegistersPart1[t.Register];
				}
			});

			Console.WriteLine("Part 1: " + testRegistersPart1.Select( kvp => kvp.Value).Max());
			Console.WriteLine("Part 2: " + testHighValue);


			var part1Data = File.ReadAllLines("Input.txt").Select(l => new Instruction(l.Trim())).ToList();

			Dictionary<string, int> part1Registers = new Dictionary<string, int>();
			int part2Value = 0;
			part1Data.ForEach(t =>
			{
				t.PerformOperation(part1Registers);

				if (part1Registers[t.Register] > part2Value)
				{
					part2Value = part1Registers[t.Register];
				}

			});

			Console.WriteLine("Part 1: " + part1Registers.Select(kvp => kvp.Value).Max());
			Console.WriteLine("Part 2: " + part2Value);

			Console.ReadLine();

		}
	}


	public class Instruction
	{
		public string Register { get; }

		public string Action { get; }

		public int Amount { get; }


		public Conditional Check { get; }

		public Instruction(string source)
		{			
			int locationOfIf = source.IndexOf("if");

			Check = new Conditional( source.Substring(locationOfIf+2));

			source = source.Substring(0, locationOfIf).Trim();
			int firstSpace = source.IndexOf(" ");
			int lastSpace = source.LastIndexOf(" ");

			Register = source.Substring(0, firstSpace).Trim();
			Amount = int.Parse(source.Substring(lastSpace).Trim());
			Action = source.Substring(firstSpace, lastSpace - firstSpace).Trim();
		}

		public void PerformOperation(Dictionary<string, int> registers)
		{
			if (!registers.ContainsKey(Register))
			{
				registers[Register] = 0;
			}

			if (!Check.CanExecute(registers))
			{
				return;
			}


			switch (Action)
			{
				case "inc":
					registers[Register] += Amount;
					break;
				case "dec":
					registers[Register] -= Amount;
					break;
			}
		}

	}

	public class Conditional
	{
		public string Register { get; }
		public string Comparison { get; }
		public int Amount { get; }

		public Conditional(string source)
		{
			source = source.Trim();

			int firstSpace = source.IndexOf(" ");
			int lastSpace = source.LastIndexOf(" ");

			Register = source.Substring(0, firstSpace).Trim();
			Amount = int.Parse(source.Substring(lastSpace).Trim());
			Comparison = source.Substring(firstSpace, lastSpace - firstSpace).Trim();
		}

		public bool CanExecute(IReadOnlyDictionary<string, int> registers)
		{
			var value = registers.ContainsKey(Register) ? registers[Register] : 0;

			switch (Comparison)
			{
				case "<":
					return value < Amount;
				case ">":
					return value > Amount;
				case "<=":
					return value <= Amount;
				case ">=":
					return value >= Amount;
				case "==":
					return value == Amount;
				case "!=":
					return value != Amount;
			}

			return true;
		}
	}
}
