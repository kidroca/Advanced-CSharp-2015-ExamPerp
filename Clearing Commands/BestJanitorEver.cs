namespace SoftUni.CSharp.ExamPreparation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security;
    using System.Text;

    /// <summary>
    /// This assignment is from the Advanced C# Exam Preparation 2015 September.
    /// You can check out the description and submit your solution here
    /// https://judge.softuni.bg/Contests/Practice/Index/84
    /// </summary>
    class BestJanitorEver
    {
        delegate void RefOperation(ref int i, ref int j);

        private static char[] unwanted =
                {
                    '>', 'v', '<', '^'
                };

        private static int rowsLength;

        private static int colsLength;

        static void Main()
        {
            List<char[]> dataForCleaning = ReadInput();

            colsLength = dataForCleaning[0].Length;
            rowsLength = dataForCleaning.Count;

            MoveThroughMatrixAndClean(dataForCleaning);

            var resultBuilder = new StringBuilder();

            foreach (var line in dataForCleaning)
            {
                resultBuilder.AppendFormat("<p>{0}</p>", SecurityElement.Escape(new string(line)));
                resultBuilder.AppendLine();
            }

            Console.WriteLine(resultBuilder.ToString());
        }

        private static void MoveThroughMatrixAndClean(List<char[]> dataForCleaning)
        {
            for (int i = 0; i < dataForCleaning.Count; i++)
            {
                for (int j = 0; j < dataForCleaning[i].Length; j++)
                {
                    switch (dataForCleaning[i][j])
                    {
                        case '>':
                            Clean(dataForCleaning, i, j, (ref int row, ref int col) => col++);
                            break;
                        case 'v':
                            Clean(dataForCleaning, i, j, (ref int row, ref int col) => row++);
                            break;
                        case '<':
                            Clean(dataForCleaning, i, j, (ref int row, ref int col) => col--);
                            break;
                        case '^':
                            Clean(dataForCleaning, i, j, (ref int row, ref int col) => row--);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private static void Clean(List<char[]> dataForCleaning, int i, int j, RefOperation operation)
        {
            operation(ref i, ref j);

            while (IsWithinRange(i, j))
            {
                char currentSymbol = dataForCleaning[i][j];

                if (unwanted.Any(c => c == currentSymbol))
                {
                    return;
                }

                dataForCleaning[i][j] = ' ';

                operation(ref i, ref j);
            }
        }

        private static List<char[]> ReadInput()
        {
            var matrixWannabe = new List<char[]>();

            string currentLine;
            while ((currentLine = Console.ReadLine()) != "END")
            {
                matrixWannabe.Add(currentLine.ToCharArray());
            }

            return matrixWannabe;
        }

        private static bool IsWithinRange(int i, int j)
        {
            bool result =
                (0 <= i && i < rowsLength) &&
                (0 <= j && j < colsLength);

            return result;
        }
    }
}
