using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Day10
{
	class Program
	{
		static void Main(string[] args)
		{
			var testRing = new Ring(new List<int>() {0,1,2,3,4});
			PerformOperations(testRing, new List<int>() {3, 4, 1, 5}, 0, 0);
			Console.WriteLine(testRing);

			var ring = new Ring();
			var operations = new List<int>() { 83, 0, 193, 1, 254, 237, 187, 40, 88, 27, 2, 255, 149, 29, 42, 100 };
			PerformOperations(ring, operations, 0, 0);
			Console.WriteLine(ring[0] + " * " + ring[1] + " = " + ring[0]*ring[1]);


			//Part 2
			var part2ring = new Ring();
			string input = "83,0,193,1,254,237,187,40,88,27,2,255,149,29,42,100";
			var part2Operations = ASCIIEncoding.ASCII.GetBytes(input).Select(i => (int) i).ToList();
			part2Operations.AddRange( new List<int>() { 17, 31, 73, 47, 23 });

			int currentPosition = 0;
			int skipSize = 0;
			for (int i = 0; i < 64; i++)
			{
				var newLoc = PerformOperations(part2ring, part2Operations, currentPosition, skipSize);
				currentPosition = newLoc.Item1;
				skipSize = newLoc.Item2;
			}

			Console.WriteLine(part2ring.GetDenseHash());

			Console.ReadLine();

		}

		static Tuple<int,int> PerformOperations(Ring ring, List<int> operations, int currentPosition, int skipSize)
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



	public class Ring
	{
		private readonly int _length = 256;
		private readonly int[] _items;

		public int this[int index]
		{
			get
			{
				int loc = index % _length;
				return _items[loc];
			}
			set
			{
				int loc = index % _length;
				_items[loc] = value;
			}
		}

		public Ring()
		{
			_items = Enumerable.Range(0, 256).ToArray();
		}

		public Ring(List<int> numbers)
		{
			_length = numbers.Count;
			_items = numbers.ToArray();
		}

		public List<int> GetItems(int start, int count)
		{
			List<int> numbers = new List<int>();

			if (count > _length)
			{
				count = _length;
			}

			for (int i = 0; i < count; i++)
			{
				numbers.Add( this[start+i]);
			}

			return numbers;
		}

		public void SetItems(int start, List<int> numbers)
		{
			foreach (var number in numbers)
			{
				this[start] = number;
				start++;
			}
		}

		public string GetDenseHash()
		{
			StringBuilder denseHash = new StringBuilder();
			
			for (int i = 0; i < 16; i++)
			{
				var items = GetItems(0 + (i*16), 16);
				int hashNumber = 0;
				items.ForEach( n => hashNumber ^= n);

				denseHash.Append(hashNumber.ToString("X"));
			}

			return denseHash.ToString();
		}

		public override string ToString()
		{
			return string.Join(",", _items);
		}
	}
}
