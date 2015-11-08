using System;
using System.IO;

namespace SpreadsheetSimulator
{
    class Program
    {
        string[,] grid;
    
            string output = "";
        string input = Console.ReadLine();
        string answer = "";

        private bool void KeysLogic(out string path)
        {
            string answer = "";
            string path = "";
            bool noKey = false;
            answer = Console.ReadLine();
            do
            {
                if (answer.ToUpper().Equals("Y"))
                {
                    Console.WriteLine("Please type the input file path and press Enter...");
                    return true;
                    path = Console.ReadLine();
                }
                else if (answer.ToUpper().Equals("N"))
                    path = null;
                    return false;
                       
                else
                {
                    Console.WriteLine("You typed a wrong key, please Enter Yes (Y) or No (N)");
                    continue;
                }
            }

        }

        static void Main()
        {
            Console.WriteLine("This program allows you to process .txt files with tab separated data");
            Console.WriteLine("Please type the output file path and press Enter...");
            //string input = @"C:\Users\Dimas\Source\Repos\SpreadsheetSimulator\SpreadsheetSimulator\SpreadsheetSimulator\Source.txt";
            //string output = @"C:\Users\Dimas\Source\Repos\SpreadsheetSimulator\SpreadsheetSimulator\SpreadsheetSimulator\Output.txt";


            if (!File.Exists(input))
            {
                Console.WriteLine("File does not exist. Do you want to change the path? Y/N");
                do
                {
                    answer = Console.ReadLine();
                    if (answer.ToUpper().Equals("Y"))
                    {
                        Console.WriteLine("Please type the input file path and press Enter...");
                        input = Console.ReadLine();
                    }
                    else if (answer.ToUpper().Equals("N"))
                        break;
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

            //Displaying data
            if (File.Exists(input))
            {
                TableParse T1 = new TableParse();
                grid = T1.MatrixCreation(input);
                T1.MatrixData(input, ref grid);
                Console.WriteLine("\n--- INPUT DATA ---");
                T1.PrintData(grid);

                T1.MatrixParse(ref grid);
                Console.WriteLine("\n--- OUTPUT DATA --");
                T1.PrintData(grid);

              //Save data
                Console.WriteLine("\nDo you want to save your data? Y/N");
                
                do
                {
                    answer = Console.ReadLine();
                    if (answer.ToUpper().Equals("Y"))
                    {
                        Console.WriteLine("Please type the output file path and press Enter...");
                        output = Console.ReadLine();
                    }
                    else if (answer.ToUpper().Equals("N"))
                        break;
                    else
                    {
                        Console.WriteLine("You typed a wrong key, please Enter Yes (Y) or No (N)");
                        continue;
                    }                   
                }
                while (answer.ToUpper() != "Y");

                if (!Directory.Exists(output))
                {
                    Directory.CreateDirectory(output);
                    output += "\\Output.txt";
                    Console.WriteLine("New path {0}", output);
                }

                if (answer.ToUpper().Equals("Y"))
                {
                    T1.SaveData(grid, output);
                    Console.WriteLine("Done! The file {0} is successfully created!", output);
                }
            }

        }
    }
}
