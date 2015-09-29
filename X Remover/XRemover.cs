namespace SoftUni.CSharp.ExamPreparation
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// This assignment is from the Advanced C# Exam Preparation 2015 September.
    /// You can check out the description and submit your solution here
    /// https://judge.softuni.bg/Contests/Practice/Index/84
    /// </summary>
    class XRemover
    {
        static void Main()
        {
            List<char[]> blocks = ReadInput();

            var mask = new bool[blocks.Count][];

            MarkXesForRemoval(blocks, mask);

            for (int i = 0; i < blocks.Count; i++)
            {
                for (int j = 0; j < blocks[i].Length; j++)
                {
                    if (mask[i][j])
                    {
                        continue;
                    }
                    else
                    {
                        Console.Write(blocks[i][j]);
                    } 
                }
                Console.WriteLine();
            }
        }

        private static void MarkXesForRemoval(List<char[]> blocks, bool[][] mask)
        {
            for (int i = 0; i < blocks.Count; i++)
            {
                mask[i] = new bool[blocks[i].Length];
            }

            for (int i = 1; i < blocks.Count - 1; i++)
            {
                for (int j = 1; j < blocks[i].Length; j++)
                {
                    if (isCross(blocks, i, j))
                    {
                        MarkCross(mask, i, j);
                    }
                }
            }
        }

        private static void MarkCross(bool[][] mask, int i, int j)
        {
            mask[i][j] = true;
            mask[i - 1][j - 1] = true;
            mask[i - 1][j + 1] = true;
            mask[i + 1][j - 1] = true;
            mask[i + 1][j + 1] = true;
        }

        private static bool isCross(List<char[]> blocks, int i, int j)
        {
            if (j >= blocks[i - 1].Length - 1 || 
                j >= blocks[i + 1].Length - 1)
            {
                return false;
            }

            char current = char.ToLower(blocks[i][j]);

            if (char.ToLower(blocks[i - 1][j - 1]) == current &&
                char.ToLower(blocks[i - 1][j + 1]) == current &&
                char.ToLower(blocks[i + 1][j - 1]) == current &&
                char.ToLower(blocks[i + 1][j + 1]) == current)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static List<char[]> ReadInput()
        {
            var blocks = new List<char[]>();

            string currentLine = String.Empty;

            while (currentLine != "END")
            {
                currentLine = Console.ReadLine();
                blocks.Add(currentLine.ToCharArray());
            }

            blocks.RemoveAt(blocks.Count - 1);

            return blocks;
        }
    }
}
