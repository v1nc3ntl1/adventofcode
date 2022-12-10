using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day1
{
    /// <summary>
    /// https://adventofcode.com/2022/day/1
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var text = GetAll();
            text = text.Replace("\r\n\r\n", "\r\n<linebreak>\r\n");
            text = text.Replace("\r\n", "|");
            var arrayCalories = text.Split('|');

            var highestCalories = 0;
            var sumCalories = 0;

            var elfCounter = 0;
            var elfGroups = new Dictionary<int, int>();

            for (var counter = 0; counter < arrayCalories.Length; counter++)
            {
                var currentCalories = arrayCalories[counter];

                if (string.Equals(currentCalories, "<linebreak>", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (sumCalories > highestCalories)
                    {
                        highestCalories = sumCalories;
                        Console.WriteLine($"line number: {counter + 1}");
                    }

                    elfGroups.Add(elfCounter, sumCalories);
                    elfCounter++;
                    sumCalories = 0;
                    continue;
                }

                sumCalories = sumCalories + int.Parse(currentCalories);

                var isLastElement = (counter == (arrayCalories.Length - 1));

                if (!isLastElement) continue;

                if (sumCalories > highestCalories)
                {
                    highestCalories = sumCalories;
                }
            }

            Console.WriteLine("highest calories is " + highestCalories);

            var sortedElfGroups = elfGroups.OrderByDescending(e => e.Value).Select(e => e.Value).ToList();
            var topNumber = 3;
            var sumOfTopN = 0; 
            for (var counter = 0; counter < topNumber; counter++)
            {
                sumOfTopN += sortedElfGroups[counter];
            }

            Console.WriteLine($"Top {topNumber} total to {sumOfTopN}");
            Console.ReadLine();
        }

        public static string GetAll()
        {
            return File.ReadAllText("calories.txt", Encoding.UTF8);
        }
    }
}
