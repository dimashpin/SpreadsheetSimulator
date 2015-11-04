using System;

namespace SpreadsheetSimulator
{
    class Program
    {
        static void Main()
        {
            string[,] grid;
            string input = @"C:\Users\Dimas\Source\Repos\SpreadsheetSimulator\SpreadsheetSimulator\SpreadsheetSimulator\Source.txt";
            string output = @"C:\Users\Dimas\Source\Repos\SpreadsheetSimulator\SpreadsheetSimulator\SpreadsheetSimulator\Output.txt";
            TableParse T1 = new TableParse();
            grid = T1.MatrixCreation(input);

            T1.MatrixData(input, ref grid);
            T1.PrintData(grid);
            Console.WriteLine();
            T1.MatrixParse(ref grid);
            T1.PrintData(grid);
            T1.PrintData(grid, output);
            
            Console.WriteLine();
        }
    }
}
