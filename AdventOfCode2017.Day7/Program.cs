using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Day7
{
	class Program
	{
		static void Main(string[] args)
		{
			var lines = File.ReadAllLines("TestData.txt");
			var Entries = lines.Select(l => new Entry(l)).ToList();
			var root = FindRootEntry(Entries);
			
			Console.WriteLine(root.Name);

			var Part1 = File.ReadAllLines("Input.txt").Select(l => new Entry(l)).ToList();
			var part1Root = FindRootEntry(Part1);
			Console.WriteLine(part1Root.Name);

			//Part 2 - build the chain of entries - find out which side is different
			BuildSupportEntries(root, Entries);
			//Get Diff amount
			Console.WriteLine(GetDifferentCorrectWeight(root));


			BuildSupportEntries(part1Root, Part1);
			Console.WriteLine(GetDifferentCorrectWeight(part1Root));

			Console.ReadLine();
		}

		private static Entry FindRootEntry(List<Entry> entries)
		{
			//Find first item that supports things
			var entry = entries.FirstOrDefault(e => e.Supporting.Count > 0);

			Entry checkEntry = null;

			while ( (checkEntry = entries.FirstOrDefault(e => e.Supporting.Any(n => n == entry.Name))) != null)
			{
				entry = checkEntry;
				checkEntry = null;
			}

			return entry;
		}

		private static void BuildSupportEntries(Entry source, IEnumerable<Entry> entries)
		{
			source.SupportEntries.AddRange( entries.Where(e=> source.Supporting.Contains(e.Name)));

			source.SupportEntries.Where( e => e.Supporting.Count > 0).ToList().ForEach( se => BuildSupportEntries(se, entries));
		}

		private static int GetDifferentCorrectWeight(Entry rootEntry)
		{
			//Trace the tree?
			int valueToAdjustBy = 0;
			
			var values = rootEntry.GetValues();

			var groups = values.GroupBy(kvp => kvp.Value);
			var groupItems = groups.Select(g => g.Key).ToList();

			if (groupItems.Count != 2)
			{
				throw new InvalidDataException();
			}

			valueToAdjustBy = groupItems[0] - groupItems[1];
			Entry incorrentEntry = rootEntry.GetUnbalancedEntry();
			Entry nextEntry = null;

			do
			{
				if (nextEntry != null)
				{
					incorrentEntry = nextEntry;
				}

				nextEntry = incorrentEntry.GetUnbalancedEntry();
			} while (nextEntry != null);

			return incorrentEntry.Number + valueToAdjustBy;
		}
	}
	

	public class Entry
	{
		public string Name { get; set; }
		public int Number { get; set; }

		public List<string> Supporting { get; set; }

		public List<Entry> SupportEntries { get; set; }

		public Entry(string line)
		{
			int firstSpace = line.IndexOf(" ");
			Name = line.Substring(0, firstSpace);

			int firstBrace = line.IndexOf("(");
			int endBrace = line.IndexOf(")");
			Number = int.Parse(line.Substring(firstBrace+1, endBrace - firstBrace - 1));

			int arrow = line.IndexOf(">");
			if (arrow > 0)
			{
				Supporting = line.Substring(arrow + 1).Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select( s=> s.Trim()).ToList();
			}
			else
			{
				Supporting = new List<string>();
			}

			SupportEntries = new List<Entry>();
		}



		public List<KeyValuePair<string, int>> GetValues()
		{
			var values = SupportEntries.Select(se => new KeyValuePair<string, int>(se.Name, se.GetTreeValue()));				
			
			return values.ToList();
		}

		public int GetTreeValue()
		{
			return Number + SupportEntries.Sum( se => se.GetTreeValue()); 
		}

		//All Entries on this side are balanced
		public Entry GetUnbalancedEntry()
		{
			var group = GetValues().GroupBy(kvp => kvp.Value).FirstOrDefault(g => g.Count() == 1);

			if (group == null)
			{
				return null;
			}

			return SupportEntries.FirstOrDefault( se => se.Name == group.FirstOrDefault().Key);

		}
	}
}
