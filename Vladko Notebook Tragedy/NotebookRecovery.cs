namespace SoftUni.CSharp.ExamPreparation
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// This assignment is from the Advanced C# Exam Preparation 2015 September.
    /// You can check out the description and submit your solution here
    /// https://judge.softuni.bg/Contests/Practice/Index/84
    /// </summary>
    internal class NotebookRecovery
    {
        private static SortedSet<string> colors = new SortedSet<string>();

        private static Dictionary<string, List<string>> opponents =
            new Dictionary<string, List<string>>();

        private static Dictionary<string, Dictionary<string, string>> playerData =
            new Dictionary<string, Dictionary<string, string>>();

        private static Dictionary<string, Dictionary<string, double>> playerWins =
            new Dictionary<string, Dictionary<string, double>>();

        private static void Main()
        {
            ReadInput();
            PrintResults();
        }

        private static void PrintResults()
        {
            bool isDataRecovered = false;

            foreach (var c in colors)
            {
                opponents[c].Sort(StringComparer.Ordinal);

                if (string.IsNullOrEmpty(playerData[c]["name"]) ||
                    string.IsNullOrEmpty(playerData[c]["age"]))
                {
                    continue;
                }

                double rank = (playerWins[c]["win"] + 1) / (playerWins[c]["loss"] + 1);

                Console.WriteLine("Color: {0}", c);
                Console.WriteLine("-age: {0}", playerData[c]["age"]);
                Console.WriteLine("-name: {0}", playerData[c]["name"]);
                Console.WriteLine(
                    "-opponents: {0}"
                    , opponents[c].Count > 0 ? string.Join(", ", opponents[c]) : "(empty)");
                Console.WriteLine("-rank: {0:F2}", rank);

                isDataRecovered = true;
            }

            if (!isDataRecovered)
            {
                Console.WriteLine("No data recovered.");
            }
        }

        private static void ReadInput()
        {
            bool cycleData = true;

            while (cycleData)
            {
                string currentLine = Console.ReadLine();
                if (currentLine == "END")
                {
                    cycleData = false;
                }
                else
                {
                    ProccessCurrentLine(currentLine);
                }
            }
        }

        private static void ProccessCurrentLine(string currentLine)
        {
            string[] info = currentLine.Split('|');

            string key = info[1],
                value = info[2];

            string color = info[0];
            if (colors.Contains(color))
            {
                if (key == "win" || key == "loss")
                {
                    playerWins[color][key] += 1;
                    opponents[color].Add(value);
                }
                else
                {
                    playerData[color][key] = value;
                }
            }
            else
            {
                CreateNewEntry(color);
                ProccessCurrentLine(currentLine);
            }
        }

        private static void CreateNewEntry(string color)
        {
            colors.Add(color);

            opponents.Add(color, new List<string>());

            playerData.Add(color, new Dictionary<string, string>());
            playerData[color]["name"] = string.Empty;
            playerData[color]["age"] = string.Empty;

            playerWins.Add(color, new Dictionary<string, double>());
            playerWins[color]["win"] = 0;
            playerWins[color]["loss"] = 0;
        }
    }
}
