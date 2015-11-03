using System;
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
        public int row { get; set; } = 0;
        public int column { get; set; } = 0;
        static int tbRow;
        static int tbColumn;

        // determine the size of array using the first line
        public string[,] TableSize(string path)
        {
            StreamReader MyStreamReader = new StreamReader(path);
            {
                line = MyStreamReader.ReadLine();
                if (line != null)
                {
                    foreach (char ch in line)
                    {
                        if (ch != '\t') cell += ch;
                        else
                        {
                            tbRow = Int32.Parse(cell);
                            cell = null;
                        }
                    }
                    // the rest converted into an amount of columns
                    tbColumn = Int32.Parse(cell);
                }
                Console.WriteLine("Rows = {0}, Columns = {1}", tbRow, tbColumn);
                return (new string[tbRow, tbColumn]);
            }
        }

        //parsing through the output and putting the chunks into created array
        public void TbParse(string path, ref string[,] grid)
        {
            bool firstLine = true;
            cell = null;
            StreamReader MyStreamReader = new StreamReader(path);
            column = 0;
            row = 0;
            while (line != null)
            {
                line = MyStreamReader.ReadLine();
                if (firstLine)
                {
                    firstLine = false;
                    line = "";
                    continue;
                }

                if (line != null)
                {
                    foreach (char ch in line)
                    {
                        if (ch != '\t') cell += ch;
                        else
                        {
                            grid[row, column] = cell;
                            column++;
                            cell = null;
                        }
                    }
                    // the rest converted into the last cell of the current row
                    grid[row, column] = cell;
                    row++;
                    column = 0;
                    cell = null;
                }
            }
            MyStreamReader.Close();
        }

        //printing all cell from the output
        public void PrintCell(object[,] matrix)
        {
            for (int i = 0; i <= matrix.GetLength(0) - 1; i++)
            {
                for (int j = 0; j <= matrix.GetLength(1) - 1; j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }
        public void PrintCell(object[,] matrix, int row, int column)
        {
            Console.WriteLine("The value of cell with the adress row = {0}, column = {1} is {2}",
                row, column, matrix[row - 1, column - 1]);
        }

        // calculating a string of arithmetic expression
        // need to check out the datetable.compute method 
        public void CalculateExpresion(ref string exp)
        {
            exp = Convert.ToString(new DataTable().Compute(exp, null));
        }

        public void GridFormating(ref string[,] grid)
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    string str = grid[i, j];
                    // getting rid of "'"
                    if (str.StartsWith("'"))
                        grid[i, j] = str.Substring(1, str.Length - 1);


                    else if (str.StartsWith("="))
                    {
                        //str = str.Substring(1, str.Length - 1);
                        //grid[i, j] = str.Substring(1, str.Length - 1);
                        foreach (char ch in str)
                        {
                            if (ch == '+' || ch == '-' || ch == '*' || ch == '/')
                            {
                                if (Regex.IsMatch(str, @"[a-zA-Z]+"))
                                {
                                    str = CellReference(str, grid);
                                    CalculateExpresion(ref str);
                                    grid[i, j] = str;
                                    break;
                                }
                                else
                                {
                                    CalculateExpresion(ref str);
                                    grid[i, j] = str;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }


        public string CellReference(string exp, string[,] grid)
        {
            exp = exp.Substring(1, exp.Length - 1);
            int innerColumn = 0, innerRow = 0;
            string[] split = exp.Split(new char[] { '+', '-', '*', '/' });

            // Go through the split cell references
            for (int i = 0; i < split.Length; i++)
            {
                string chunk = split[i];
                // Go through the chunks
                for (int j = 0; j < chunk.Length; j++)
                {
                    if (chunk[j] >= 'A' && chunk[j] <= 'Z')
                    {
                        innerColumn = (int)((chunk[j] - 'A' + 1));
                        innerRow = (int)Char.GetNumericValue(chunk[j + 1]); //doen't read 2 digits adress
                        
                        if ((grid[innerRow - 1, innerColumn]).StartsWith("="))
                        {
                            string temp = CellReference((grid[innerRow - 1, innerColumn]), grid);
                            exp = exp.Replace(chunk, temp);
                            //chunk = CellReference((grid[innerRow - 1, innerColumn]), grid);
                            //str.Substring(1, str.Levngth - 1);
                        }
                        exp = exp.Replace(chunk, grid[innerRow - 1, innerColumn]);
                        break;

                    }
                }
            }
            return exp;
        }

    }
}






//cell reference
/*
else if (str[0] == '=' && char.IsLetter(str[1]))
{
     if 
    int i = 0;
    for (char ch = 'A'; c[1] >= ch; ch++) i++;
    Console.Write((grid[Convert.ToInt32(c.Substring(2, c.Length - 2))][i - 1]) + "\t");
}

//parsing the expression
else if (c[0] == '=' && char.IsDigit(c[1])) CalculateString(c);

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
*/



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
