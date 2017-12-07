using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Day2
{
	class Program
	{
		static void Main(string[] args)
		{
			List<string> test1 = new List<string>() { "5 1 9 5", "7 5 3",  "2 4 6 8"};

			Console.WriteLine(TableCheckSum(test1));

			Console.ReadLine();
		}


		static int TableCheckSum(List<string> rows)
		{			
			var item = rows.Select( r => r.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select( c => int.Parse(c)));

			var rowDiffs = item.ToList().Select(r => RowDiff(r));

			return rowDiffs.Sum();

		}

		static int RowDiff(IEnumerable<int> numbers)
		{
			return numbers.Max() - numbers.Min();
		}
	}
}
