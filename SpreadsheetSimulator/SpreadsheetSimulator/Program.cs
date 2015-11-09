using System;
using System.IO;

namespace SpreadsheetSimulator
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("This program allows you to process .txt files with tab separated data");
            Console.WriteLine("Please type the input file path and press Enter...");
            string[,] grid;

            string output = "";           
            string answer = "";
            string input = Console.ReadLine();


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
                        Console.WriteLine("You typed a wrong key, please Enter Y (Yes) or N (No)");
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
                DataParse T1 = new DataParse();
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
                        Console.WriteLine("You typed a wrong key, please Enter Y (Yes) or N (No)");
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
