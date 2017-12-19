using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day11
{
	class Program
	{
		public static Dictionary<string, Hex> Movement = new Dictionary<string, Hex>()
		{
			{ "n", new Hex( 0, +1, -1) },
			{ "ne", new Hex(+1,  0, -1) },
			{ "nw", new Hex(-1, +1,  0) },
			{ "s", new Hex( 0, -1, +1) },
			{ "se", new Hex(+1, -1,  0) },
			{ "sw", new Hex(-1,  0, +1) },
		};

		static void Main(string[] args)
		{
			List<string> testInstructions = new List<string>()
			{
				"ne,ne,ne",
				"ne,ne,sw,sw",
				"ne,ne,s,s",
				"se,sw,se,sw,sw"
			};

			foreach (var testInstruction in testInstructions)
			{
				Hex loc = new Hex(0, 0, 0);

				foreach (var instruction in testInstruction.Split(",".ToCharArray()))
				{
					loc.Move(Movement[instruction]);
				}
				Console.WriteLine(loc.DistanceFromOrigin());
			}

			Console.WriteLine("Part 1");

			Hex part1 = new Hex(0,0,0);
			string instructionSet = File.ReadAllText("Input.txt");
			int furthestDistance = 0;
			foreach (var instruction in instructionSet.Split(",".ToCharArray()))
			{
				part1.Move(Movement[instruction]);
				furthestDistance = furthestDistance < part1.DistanceFromOrigin() ? part1.DistanceFromOrigin() : furthestDistance;
			}

			Console.WriteLine(part1.DistanceFromOrigin());
			Console.WriteLine(furthestDistance);

			Console.ReadLine();
		}
	}

	public class Hex
	{
		public int X { get; private set; }
		public int Y { get; private set; }
		public int Z { get; private set; }

		public Hex(int x, int y, int z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		public void Move(Hex movement)
		{
			X += movement.X;
			Y += movement.Y;
			Z += movement.Z;
		}

		public int Distance(Hex location)
		{
			return Hex.Distance(this, location);
		}

		public int DistanceFromOrigin()
		{
			return Hex.Distance(this, new Hex(0, 0, 0));
		}

		public static int Distance(Hex a, Hex b)
		{
			return (Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y) + Math.Abs(a.Z - b.Z)) / 2;
		}
	}


}

