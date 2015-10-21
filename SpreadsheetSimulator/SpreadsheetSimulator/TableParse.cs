using System;
using System.IO;
using System.Collections.Generic;

namespace SpreadsheetSimulator
{
    class TableParse
    {
        string line = "";
        string cell = null;
        public int row { get; set; }
        public int column { get; set; }
        List<List<object>> grid = new List<List<object>>();

        //parsing through the output
        public void tbParse(string path)
        {
            StreamReader MyStreamReader = new StreamReader(path);
            row = 0;
            column = 0;
            while (line != null)
            {
                line = MyStreamReader.ReadLine();
                grid.Add(new List<object>());
                if (line != null)
                {

                    foreach (char ch in line)
                    {
                        if (ch != '\t') cell += ch;     
                        else
                        {
                            grid[column].Add(cell);
                            row++;
                            //Console.WriteLine(cell);
                            cell = null;
                        }
                    }
                    grid[column].Add(cell);
                    row++;
                    //Console.WriteLine(cell);
                    cell = null;
                }
                column++;
            }
        }

        //printing all cell from the output
        public void PrintCell()
        {
            foreach (List<object> r in grid)
            {
                foreach (object c in r)
                {
                    Console.Write(c+"\t");
                }
                Console.WriteLine();
            }
        }

        //printing one cell from the output
        public void PrintCell(int row, int column)
        {
            Console.WriteLine("The value of cell with the adress row = {0}, column = {1} is {2}",
                row, column, grid[row][column]);
        }
    }
}
