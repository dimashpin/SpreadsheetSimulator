using System;
using System.IO;
using System.Data;

namespace SpreadsheetSimulator
{
    class DataParse
    {
        string line = "";
        string cell;
        static int height { get; set; }
        static int width { get; set; }

        public string[,] MatrixCreation(string path)
        {
            // determine the size of array using the first line

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
                            height = Int32.Parse(cell);
                            cell = null;
                        }
                    }
                    // the rest converted into an amount of columns
                    width = Int32.Parse(cell);
                }
                MyStreamReader.Close();
                return (new string[height, width]);
            }

        }
        public void MatrixData(string path, ref string[,] grid)
        {
            //parsing through the output and putting the chunks into created array
            int row = 0, column = 0;
            bool firstLine = true;
            cell = null;
            StreamReader MySR = new StreamReader(path);
            while (line != null)
            {
                line = MySR.ReadLine();
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
            MySR.Close();
        }
        public void PrintData(string[,] matrix)
        {
            //printing all cell from the output

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }        

        public void MatrixParse(ref string[,] matrix)
        {
            //formating text, caclulation of all expressions

            // loop through all cells
            try
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        string str = matrix[i, j];
                        // getting rid of "'".
                        if (str.StartsWith("'"))
                            matrix[i, j] = str.Substring(1);
                        else if (str.StartsWith("="))
                        {
                            foreach (char ch in str)
                            {
                                str = CellParse(str, matrix);
                                CalcExpr(ref str);
                                matrix[i, j] = str;
                                break;
                            }
                        }
                    }
                }
            }
            catch (NullReferenceException)
            {
                Console.Write("#Null exc");
            }
        }

        public string CellParse(string exp, string[,] matrix)
        {
            int column, row = 0;
            string chunk;
            string[] split;

            try
            {
                if (exp.StartsWith("="))
                    exp = exp.Substring(1);
                split = exp.Split(new char[] { '+', '-', '*', '/' });
                // Go through the split cell references
                for (int i = 0; i < split.Length; i++)
                {
                    chunk = split[i];
                    // Go through the chunks
                    for (int j = 0; j < chunk.Length; j++)
                    {
                        if (chunk[j] >= 'A' && chunk[j] <= 'Z')
                        {
                            column = (int)((chunk[j] - 'A'));
                            row = Int32.Parse(chunk.Substring(j + 1)); 
                            if ((matrix[row - 1, column]).StartsWith("="))
                            {
                                //recursion until valid value reached
                                string temp = CellParse((matrix[row - 1, column]), matrix);
                                exp = exp.Replace(chunk, temp);
                            }
                            exp = exp.Replace(chunk, matrix[row - 1, column]);
                            break;
                        }
                    }
                }
                return exp;
            }
            catch (IndexOutOfRangeException)
            {
                return exp = "#Indexoutofbounds";
            }
        }

        public void CalcExpr(ref string exp)
        {
            // calculate an arithmetic expression.

            try
            {
                exp = Convert.ToString(new DataTable().Compute(exp, null));
            }
            catch (FormatException)
            {
                exp = "#Wrong format";
            }
            catch (DataException)
            {
                exp = "#Can't find data";
            }
        }

        public void SaveData(string[,] matrix, string path)
        {
            StreamWriter mySW = new StreamWriter(path, false);
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    mySW.Write(matrix[i, j] + "\t");
                }
                mySW.WriteLine();
            }
            mySW.Close();
        }
    }
}
