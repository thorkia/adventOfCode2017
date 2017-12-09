using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Day4
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine(SimpleVerifyPassword("aa bb cc dd ee"));
			Console.WriteLine(SimpleVerifyPassword("aa bb cc dd aa"));
			Console.WriteLine(SimpleVerifyPassword("aa bb cc dd aaa"));

			var passwords = File.ReadAllLines("Part1.txt");
			int validCount = 0;
			foreach (var password in passwords)
			{
				if (SimpleVerifyPassword(password))
				{
					validCount++;
				}
			}

			Console.WriteLine(validCount);

			//Part 2			
			Console.WriteLine(AnagramVerifyPassword("abcde fghij"));
			Console.WriteLine(AnagramVerifyPassword("abcde xyz ecdab"));
			Console.WriteLine(AnagramVerifyPassword("a ab abc abd abf abj"));
			Console.WriteLine(AnagramVerifyPassword("iiii oiii ooii oooi oooo"));
			Console.WriteLine(AnagramVerifyPassword("oiii ioii iioi iiio"));

			int validAnagramCount = 0;
			foreach (var password in passwords)
			{
				if (AnagramVerifyPassword(password))
				{
					validAnagramCount++;
				}
			}

			Console.WriteLine(validAnagramCount);

			Console.ReadLine();
		}

		private static bool SimpleVerifyPassword(string password)
		{
			var words = password.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

			var wordCount = new Dictionary<string, int>();
			HashSet<string> wordSet = new HashSet<string>();

			foreach (var word in words)
			{
				if (wordSet.Contains(word))
				{
					return false;
				}
				else
				{
					wordSet.Add(word);
				}
			}
			
			return true;
		}

		private static bool AnagramVerifyPassword(string password)
		{
			var words = password.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

			//Group the words by length, then only select the groups that have more than one word
			//The rules say that all the letters must be used, so a 4 letter word can match with a 3 letter on
			var group = words.GroupBy(w => w.Length).Where( w => w.Count() > 1).ToDictionary( g=> g.Key, g => g.ToList());

			//If all the words in the password are different lengths, then dictionary will be empty - meaning we have a valid password
			if (group.Count == 0)
			{
				return true;
			}

			foreach (var wordSet in group.Values)
			{
				var anagrams = AreWordsAnagrams(wordSet);
				//If any of the passwords are Anagrams, this is a bad password
				if (anagrams)
				{
					return false;
				}
			}


			return true;
		}

		private static bool AreWordsAnagrams(IEnumerable<string> words)
		{
			HashSet<string> wordSet = new HashSet<string>();

			foreach (var word in words)
			{
				//Convert to char array, sort, and convert back to string - this will make for an easy check if all the letters are identical
				var charArray = word.ToCharArray().ToList();
				charArray.Sort();
				var sortedWord = new string(charArray.ToArray());

				if (wordSet.Contains(sortedWord))
				{
					return true;
				}
				else
				{
					wordSet.Add(sortedWord);
				}
			}

			//If reach here, we have no duplicate words
			return false;
		}
	}
}
