using System;
using System.IO;

namespace SpreadsheetSimulator
{
    class Program
    {
        static void Main()
        {
            //if (!File.Exists("Source.txt"))
            //{
            //    throw new Exception("The file path doesn't exist");
            //}

            StreamReader MyStreamReader = new StreamReader(@"C:\Users\Dimas\Source\Repos\SpreadsheetSimulator\SpreadsheetSimulator\SpreadsheetSimulator\Source.txt");

            string temp = "";
            int i = 0;
            while (temp != null)
            {
                temp = MyStreamReader.ReadLine();
                if (temp != null)
                {
                    foreach (char s in temp)
                    {
                       Console.WriteLine(s);
                    }
                }
                //Console.WriteLine(temp);
                //Console.WriteLine(i);
            }

        }
    }
}
