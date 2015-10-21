using System;
using System.IO;

namespace SpreadsheetSimulator
{
    class TableParse
    {
        string line = "";
        string cell = null;

        public void tbParse(string path)
        {
            StreamReader MyStreamReader = new StreamReader(path);
            while (line != null)
            {
                line = MyStreamReader.ReadLine();
                if (line != null)
                {

                    foreach (char ch in line)
                    {
                        if (ch != '\t') cell += ch;     
                        else
                        {
                            Console.WriteLine(cell);
                            cell = null;
                        }
                    }
                    Console.WriteLine(cell);
                    cell = null;
                }
            }
        }
    }
}
