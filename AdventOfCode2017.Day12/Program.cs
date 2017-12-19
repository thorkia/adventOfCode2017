using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Day12
{
	class Program
	{
		static void Main(string[] args)
		{
			List<Item> testItems = new List<Item>();

			foreach (var readAllLine in File.ReadAllLines("testInput.txt"))
			{
				testItems.Add(new Item(readAllLine));
			}

			//Determine Connected Items
			HashSet<int> testConnectedItems = new HashSet<int>();
			Queue<Item> testProcessQueue = new Queue<Item>();
			testProcessQueue.Enqueue(testItems.First(i => i.Id == 0));

			while (testProcessQueue.Count != 0)
			{
				var item = testProcessQueue.Dequeue();
				testConnectedItems.Add(item.Id);
				foreach (var item1 in testItems.Where( i => item.Connected.Contains(i.Id) && !testConnectedItems.Contains(i.Id)))
				{					
					testProcessQueue.Enqueue(item1);
				}
			}

			Console.WriteLine(testConnectedItems.Count);

			Console.WriteLine("Part 1");

			List<Item> items = new List<Item>();

			foreach (var readAllLine in File.ReadAllLines("Input.txt"))
			{
				items.Add(new Item(readAllLine));
			}

			int groupCount = 0;
			while (items.Count > 0)
			{
				//Determine Connected Items
				HashSet<int> connectedItems = new HashSet<int>();
				Queue<Item> processQueue = new Queue<Item>();
				processQueue.Enqueue(items.First());

				while (processQueue.Count != 0)
				{
					var item = processQueue.Dequeue();					
					connectedItems.Add(item.Id);
					foreach (var item1 in items.Where(i => item.Connected.Contains(i.Id) && !connectedItems.Contains(i.Id)))
					{
						processQueue.Enqueue(item1);
					}

					items.Remove(item);
				}

				Console.WriteLine(connectedItems.Count);
				groupCount++;
			}
			Console.WriteLine($"Group Count {groupCount}");
			Console.ReadLine();

		}
	}

	public class Item
	{
		public int Id { get; private set; }
		public HashSet<int> Connected { get; private set; }

		public Item(string line)
		{
			int split1 = line.IndexOf("<");
			int split2 = line.IndexOf(">");

			Id = int.Parse(line.Substring(0, split1));

			Connected = new HashSet<int>();
			foreach (var s in line.Substring(split2+1).Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
			{
				Connected.Add(int.Parse(s));
			}
		}
	}
}
