using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Day13
{
	class Program
	{
		static void Main(string[] args)
		{
			List<string> testRuleText = new List<string>()
			{
				"0: 3",
				"1: 2",
				"4: 4",
				"6: 4",
			};

			Firewall testFirewall = new Firewall(testRuleText);
			Console.WriteLine(RunFirewall(testFirewall));

			Console.WriteLine("Part 1");

			List<string> ruleText = File.ReadAllLines("Input.txt").ToList();
			Firewall firewall = new Firewall(ruleText);
			Console.WriteLine(RunFirewall(firewall));
			

			//Part 2 - delay the trip.

			int testDelay = -1;
			bool testCleanRun = false;
			do
			{
				testDelay++;
				Firewall wall = new Firewall(testRuleText);
				testCleanRun = CleanRunFirewall(wall, testDelay);
			} while (!testCleanRun);

			Console.WriteLine(testDelay);

			int delay = -1;
			bool cleanRun = false;
			do
			{
				delay++;
				Firewall wall = new Firewall(ruleText);
				cleanRun = CleanRunFirewall(wall, delay);
			} while (!cleanRun);

			Console.WriteLine(cleanRun);

			Console.ReadLine();
		}

		static int RunFirewall(Firewall firewall)
		{			
			int position = -1;
			List<int> collisions = new List<int>();
			do
			{
				position++;
				if (firewall.Collision(position))
				{
					collisions.Add(position);
				}
				firewall.TimeStep();

			} while (position <= firewall.Size);

			int severity = firewall.GetTripSeverity(collisions);
			return severity;
		}

		static bool CleanRunFirewall(Firewall firewall, int delay)
		{
			for (int time = 0; time < delay; time++)
			{
				firewall.TimeStep();
			}

			int position = -1;
			
			do
			{
				position++;
				if (firewall.Collision(position))
				{
					return false;
				}
				firewall.TimeStep();

			} while (position <= firewall.Size);

			return true;
		}
	}

	public class Firewall
	{
		Dictionary<int, int> _fireWallRange = new Dictionary<int, int>();
		Dictionary<int, int> _fireWallPosition = new Dictionary<int, int>();
		Dictionary<int, int> _fireWallMoveAmount = new Dictionary<int, int>();

		public int Size { get; }

		public Firewall(List<string> definitions)
		{
			foreach (var definition in definitions)
			{
				var item = definition.Split(":".ToCharArray());
				_fireWallRange[int.Parse(item[0])] = int.Parse(item[1]);				
			}

			foreach (var key in _fireWallRange.Keys)
			{
				_fireWallPosition[key] = 0;
				_fireWallMoveAmount[key] = 1;
				Size = key > Size ? key : Size;
			}
		}

		public bool Collision(int position)
		{
			if (!_fireWallRange.ContainsKey(position))
			{
				return false;
			}

			if (_fireWallPosition[position] != 0)
			{
				return false;
			}

			return true;
		}

		public void TimeStep()
		{
			foreach (var key in _fireWallPosition.Keys.ToList())
			{
				_fireWallPosition[key]+=_fireWallMoveAmount[key];

				if (_fireWallPosition[key] + 1 == _fireWallRange[key] || 
					_fireWallPosition[key]-1 < 0)
				{
					_fireWallMoveAmount[key] *= -1;
				}

				
			}
		}

		public int GetTripSeverity(List<int> collisions)
		{
			int amount = 0;
			foreach (var collision in collisions)
			{
				amount += collision * _fireWallRange[collision];
			}

			return amount;			
		}
	}
}
