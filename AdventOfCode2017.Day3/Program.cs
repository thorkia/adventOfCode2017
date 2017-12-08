using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Day3
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine(GetSteps(23));
			Console.WriteLine(GetSteps(1024));

			Console.WriteLine(GetSteps(289326));

			Console.ReadLine();

			Console.WriteLine("Debug");
			Console.WriteLine(GetStepValue(289326));

			Console.ReadLine();
		}


		static int GetSteps(int targetNumber)
		{
			int gridSize = (int)Math.Ceiling(Math.Sqrt(targetNumber));

			//The grid must be an odd number
			if (gridSize % 2 == 0)
			{
				gridSize++;
			}

			int maxNumber = gridSize * gridSize;

			int bottomMid = maxNumber - ((gridSize - 1) / 2);
			int leftMid = bottomMid - (gridSize - 1);
			int topMid = leftMid - (gridSize - 1);
			int rightMid = topMid - (gridSize - 1);

			//The maximum number of steps in any direction to the edge of the grid
			int maxStepsDirection = (gridSize - 1) / 2;

			//Find the side mid point it is <= (gridsize-1)/2 from
			//Steps will then be the diff from the side + (gridsize-1)/2
			if (Math.Abs(targetNumber - bottomMid) <= maxStepsDirection)
			{
				return maxStepsDirection + Math.Abs(targetNumber - bottomMid);
			}

			if (Math.Abs(targetNumber - leftMid) <= maxStepsDirection)
			{
				return maxStepsDirection + Math.Abs(targetNumber - leftMid);
			}

			if (Math.Abs(targetNumber - topMid) <= maxStepsDirection)
			{
				return maxStepsDirection + Math.Abs(targetNumber - topMid);
			}

			if (Math.Abs(targetNumber - rightMid) <= maxStepsDirection)
			{
				return maxStepsDirection + Math.Abs(targetNumber - rightMid);
			}

			return 0;
		}

		static int GetStepValue(int checkValue)
		{
			int gridSize = 25;

			//The grid must be an odd number
			int midpoint = gridSize / 2;

			int[,] grid = new int[gridSize,gridSize];

			//Initialize Grid
			grid[midpoint, midpoint] = 1;

			grid[midpoint + 1, midpoint] = 1;
			grid[midpoint + 1, midpoint - 1] = 2;
			grid[midpoint, midpoint - 1] = 4;
			grid[midpoint - 1, midpoint - 1] = 5;
			grid[midpoint - 1, midpoint] = 10;
			grid[midpoint - 1, midpoint + 1] = 11;
			grid[midpoint, midpoint + 1] = 23;
			grid[midpoint + 1, midpoint + 1] = 25;

			int value = -1;
			int ringNumber = 1;
			
			while (value < 0)
			{
				ringNumber++;
				value = BuildRing(ringNumber, midpoint, grid, checkValue);
			}

			return value;
		}

		static int BuildRing(int ringNumber, int midPoint, int[,] grid, int maxNumber)
		{
			int x = midPoint + ringNumber;
			int y = midPoint + (ringNumber-1);

			grid[x, y] = GetCellValue(x, y, grid);

			if (grid[x, y] > maxNumber)
			{
				return grid[x, y];
			}

			//Go up Ring Number + 1
			for (int i = 0; i < ringNumber + (ringNumber-1); i++)
			{
				y--;
				grid[x, y] = GetCellValue(x, y, grid);

				if (grid[x, y] > maxNumber)
				{
					return grid[x, y];
				}
			}			

			//Go left ringNumber + 2
			for (int i = 0; i < ringNumber * 2; i++)
			{
				x--;
				grid[x, y] = GetCellValue(x, y, grid);

				if (grid[x, y] > maxNumber)
				{
					return grid[x, y];
				}
			}			

			//Go down ringNumber + 2
			for (int i = 0; i < ringNumber * 2; i++)
			{
				y++;
				grid[x, y] = GetCellValue(x, y, grid);

				if (grid[x, y] > maxNumber)
				{
					return grid[x, y];
				}
			}
			
			//Go right ringNumber + 2			
			for (int i = 0; i < ringNumber * 2; i++)
			{
				x++;
				grid[x, y] = GetCellValue(x, y, grid);

				if (grid[x, y] > maxNumber)
				{
					return grid[x, y];
				}
			}
			

			//Return -1 if no cell is greater
			return -1;
		}

		static int GetCellValue(int x, int y, int[,] grid)
		{
			int cellValue = 0;
			int maxX = grid.GetLength(0);
			int maxY = grid.GetLength(1);

			if (y + 1 < maxY)
			{
				cellValue += grid[x, y + 1];
			}

			if (y - 1 >= 0)
			{
				cellValue += grid[x, y - 1];
			}

			if (x + 1 < maxX)
			{
				cellValue += grid[x + 1, y];
			}

			if (x - 1 >= 0)
			{
				cellValue += grid[x - 1, y];
			}

			if (x - 1 >= 0 && y-1 >= 0)
			{
				cellValue += grid[x - 1, y - 1];
			}

			if (x - 1 >= 0 && y + 1 < maxY)
			{
				cellValue += grid[x - 1, y + 1];
			}

			if (x + 1 < maxX && y - 1 >= 0)
			{
				cellValue += grid[x + 1, y - 1];
			}

			if (x + 1 < maxX && y + 1 < maxY)
			{
				cellValue += grid[x + 1, y + 1];
			}

			return cellValue;
		}
	}
}
