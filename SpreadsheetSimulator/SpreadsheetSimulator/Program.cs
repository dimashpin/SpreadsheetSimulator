using System;
using System.IO;
using System.Collections.Generic;

namespace SpreadsheetSimulator
{
    class Program
    {
        static void Main()
        {
            TableParse T1 = new TableParse();
            T1.tbParse(@"C:\Users\Dimas\Source\Repos\SpreadsheetSimulator\SpreadsheetSimulator\SpreadsheetSimulator\Source.txt");
            T1.PrintCell();
            Console.WriteLine();
            T1.PrintCell(1, 2);

        }
    }
}
