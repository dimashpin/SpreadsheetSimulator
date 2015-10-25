﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Data;

namespace SpreadsheetSimulator
{
    class TableParse
    {
        string line = "";
        string cell = null;
        public int row { get; set; }
        public int column { get; set; }
        List<List<string>> grid = new List<List<string>>();

        //parsing through the output
        public void tbParse(string path)
        {
            StreamReader MyStreamReader = new StreamReader(path);
            row = 0;
            column = 0;
            while (line != null)
            {
                line = MyStreamReader.ReadLine();
                grid.Add(new List<string>());
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

        public void TextFormating()
        {
            foreach (List<string> r in grid)
            {
                foreach (string c in r)
                {
                    // getting rid of "'"
                    if (c.StartsWith("'"))
                        Console.Write(c.TrimStart('\'') + "\t");

                    //cell reference
                    else if (c[0] == '=' && char.IsLetter(c[1]))
                    {
                        int i = 0;
                        for (char ch = 'A'; c[1] >= ch; ch++) i++;
                        Console.Write((grid[Convert.ToInt32(c.Substring(2, c.Length - 2))][i - 1]) + "\t");
                    }

                    //parsing the expression
                    else if (c[0] == '=' && char.IsDigit(c[1]))
                    {
                        // need to check out the datetable.compute method
                        var result = new DataTable().Compute(c.TrimStart('='), null);
                        Console.Write(result+ "\t");
                    }

                    //cell reference arithmetic operations
                    //recursion method???
                    else if (c[0] == '=' && char.IsLetter(c[1]) && c.Length > 3)
                    {
                        //first cell reference
                        int firstNumber;
                        int i = 0;
                        for (char ch = 'A'; c[1] >= ch; ch++) i++;
                        firstNumber = Int32.Parse(grid[Convert.ToInt32(c.Substring(2, c.Length - 2))][i - 1]);

                        int i = 0;
                        for (char ch = 'A'; c[1] >= ch; ch++) i++;
                        Console.Write((grid[Convert.ToInt32(c.Substring(2, c.Length - 2))][i - 1]) + "\t");
                    }

                    else
                        Console.Write(c + "\t");
                }
                Console.WriteLine();
            }
        }
        /*
                public void CellReference()
                {
                    foreach (List<object> r in grid)
                    {
                        foreach (object c in r)
                        {
                            if (c.ToString().StartsWith("="))
                            {
                                int i = 0;
                                string syl = c.ToString();
                                for (char ch = 'A'; syl[1] >= ch; ch++) i++;                          
                                Console.Write(grid[Convert.ToInt32(syl.Substring(2, syl.Length - 2))][i-1]);
                            }
                        }
                        Console.WriteLine();
                    }
                }
                */


        //printing all cell from the output
        public void PrintCell()
        {
            foreach (List<string> r in grid)
            {
                foreach (string c in r)
                {
                    Console.Write(c + "\t");
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
