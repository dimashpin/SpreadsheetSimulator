using System;
using System.IO;

namespace SpreadsheetSimulator
{
    class Program
    {
        static void Main()
        {
            string[,] grid;
            Console.WriteLine("This program allows you to process .txt files with tab separated data");
            Console.WriteLine("Please type the output file path and press Enter...");
            //string input = @"C:\Users\Dimas\Source\Repos\SpreadsheetSimulator\SpreadsheetSimulator\SpreadsheetSimulator\Source.txt";

            string input = Console.ReadLine();
            string answer = "";

            if (!File.Exists(input))
            {
                Console.WriteLine("File does not exist. Do you want to change the path? Y/N");
            }

            do
            {
                    do
                    {
                        answer = Console.ReadLine();

                        if (answer.ToUpper() == "Y")
                        {
                            Console.WriteLine("Please type the input file path and press Enter...");
                            input = Console.ReadLine();
                        }
                        else if (answer.ToUpper() == "N")
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("You typed a wrong key, please Enter Yes (Y) or No (N)");
                            continue;
                        }

                    if (!File.Exists(input))
                    {
                        Console.WriteLine("File does not exist. Do you want to change the path? Y/N");
                    }
                }
                while (!File.Exists(input));
            }

            while (answer.ToUpper() != "N");

                if (File.Exists(input))
            {
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
    }
