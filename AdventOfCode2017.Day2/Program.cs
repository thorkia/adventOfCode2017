using System;
using System.Collections.Generic;
using System.ComponentModel;
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

			List<string> question1 = new List<string>()
			{
				"121 59  141 21  120 67  58  49  22  46  56  112 53  111 104 130",
				"1926  1910  760 2055  28  2242  146 1485  163 976 1842  1982  137 1387  162 789",
				"4088  258 2060  1014  4420  177 4159  194 2794  4673  4092  681 174 2924  170 3548",
				"191 407 253 192 207 425 580 231 197 382 404 472 164 571 500 216",
				"4700  1161  168 5398  5227  5119  252 2552  4887  5060  1152  3297  847 4525  220 262",
				"2417  992 1445  184 554 2940  209 2574  2262  1911  2923  204 2273  2760  506 157",
				"644 155 638 78  385 408 152 360 588 618 313 126 172 220 217 161",
				"227 1047  117 500 1445  222 29  913 190 791 230 1281  1385  226 856 1380",
				"436 46  141 545 122 86  283 124 249 511 347 502 168 468 117 94",
				"2949  3286  2492  2145  1615  159 663 1158  154 939 166 2867  141 324 2862  641",
				"1394  151 90  548 767 1572  150 913 141 1646  154 1351  1506  1510  707 400",
				"646 178 1228  1229  270 167 161 1134  193 1312  1428  131 1457  719 1288  989",
				"1108  1042  93  140 822 124 1037  1075  125 941 1125  298 136 94  135 711",
				"112 2429  1987  2129  2557  1827  477 100 78  634 352 1637  588 77  1624  2500",
				"514 218 209 185 197 137 393 555 588 569 710 537 48  309 519 138",
				"1567  3246  4194  151 3112  903 1575  134 150 4184  3718  4077  180 4307  4097  1705"
			};

			Console.WriteLine(TableCheckSum(question1));

			Console.ReadLine();

			//Part 2

			List<string> test2 = new List<string>() { "5 9 2 8", "9 4 7 3", "3 8 6 5"};

			Console.WriteLine(DivTableCheckSum(test2));
			Console.WriteLine(DivTableCheckSum(question1));

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


		static int DivTableCheckSum(List<string> rows)
		{
			var convertedToInt = rows.Select(r => r.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(c => int.Parse(c)));

			var rowDivs = convertedToInt.ToList().Select(r => RowDiv(r));

			return rowDivs.Sum();
		}

		static int RowDiv(IEnumerable<int> numbers)
		{
			var max = numbers.Count();
			var numArray = numbers.ToArray();

			for (int i =0; i < max-1; i++)
			{
				int secondIndex = i + 1;
				while (secondIndex < max)
				{
					if (numArray[i] % numArray[secondIndex] == 0)
					{
						return numArray[i] / numArray[secondIndex];
					}

					if (numArray[secondIndex] % numArray[i] == 0)
					{
						return numArray[secondIndex] / numArray[i];
					}

					secondIndex++;
				}
			}

			return 0;
		}
	}
}
