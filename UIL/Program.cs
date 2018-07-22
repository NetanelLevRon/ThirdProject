using BLL;
using System;

namespace UIL
{
    class Program
    {

        static void Main(string[] args)
        {

            string[] start;
            FileManager.AddFileAction += ((string searchPattern, string searchDir, string foundPath) => { Console.WriteLine(foundPath); });

            do // as long as the input is not 'Exit'
            {
                start = FileManager.GetStart(); // start consist the user input 1, 2, 3 and the id of the last line in the DB for insert in it the new data.

                FileManager.DirSearch(start);
                
                FileManager.AddResult(start);

                FileManager.fileList.Clear();
                Console.Write("press any key to clear and continue");
                Console.ReadLine();
                Console.Clear();

            } while (start[0] != "3");

        }
    }
}

