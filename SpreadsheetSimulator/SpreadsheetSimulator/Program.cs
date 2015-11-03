using System;
using System.IO;
using System.Collections.Generic;

namespace SpreadsheetSimulator
{
    class Program
    {
        static void Main()
        {
            string[,] grid;
            string path = @"C:\Users\Dimas\Source\Repos\SpreadsheetSimulator\SpreadsheetSimulator\SpreadsheetSimulator\Source.txt";
            TableParse T1 = new TableParse();
            grid = T1.TableSize(path);

            T1.TbParse(path, ref grid);
            T1.GridFormating(ref grid);
            T1.PrintCell(grid);


            

            //T1.PrintCell();
            Console.WriteLine();
            
            //T1.PrintCell(1, 2);

        }
    }


}
