using DAL;
using System;
using System.Collections.Generic;
using System.IO;

namespace BLL
{
    /// <summary>
    /// deligate
    /// </summary>
    /// <param name="searchPattern">the string that the user want to search</param>
    /// <param name="searchDir">directory</param>
    /// <param name="foundPath">the new path that found</param>
    public delegate void AddFilesHeandeler(string searchPattern, string searchDir, string foundPath);

    public static class FileManager
    {

        public static List<string> fileList = new List<string>();

        public static event AddFilesHeandeler AddFileAction;

        public static Search SearchObj;


        /// <summary>
        /// geting start with 3 options
        /// </summary>
        /// <returns>the user choice</returns>
        public static string[] GetStart()
        {
            Console.WriteLine("1. Enter file name to search.\n2. Enter file name to search + " +
                              "parent directory to search in.\n" +
                              "3. Exit.");

            string[] temp = new string[4];
            bool flag;


            do
            {
                flag = false;
                temp[0] = Console.ReadLine();

                switch (temp[0])
                {
                    case "1": Console.Write("Enter file name: "); temp[1] = Console.ReadLine(); SearchObj = new Search() { SearchPattern = temp[1] }; break; // insert into 'SearchObj' the Pattern input
                    case "2":
                        Console.Write("Enter file name: "); temp[1] = Console.ReadLine();
                        Console.Write("Enter diractory to search in: ");
                        temp[2] = Console.ReadLine();
                        while (!Directory.Exists(temp[2]))
                        {
                            Console.Write("your directory input is unexist! please try again: ");
                            temp[2] = Console.ReadLine();
                        }
                        SearchObj = new Search() { SearchPattern = temp[1], SearchDirectory = temp[2] }; // insert into 'SearchObj' the Pattern and the Directory inputs
                        break;
                    case "3": break;
                    default: Console.WriteLine("Please insert 1, 2 or 3 only!"); flag = true; break;
                }
            } while (flag);



            if (temp[0] != "3")
            {
                Console.WriteLine("\nstart searching...\n");
                SearchObj.SearchID = SearchManager.InsertSearch(SearchObj);
                temp[3] = SearchObj.SearchID.ToString();
            }
            return temp;
        }


        /// <summary>
        /// searching for files in the current computer
        /// </summary>
        /// <param name="lFile">get the name or part of name, of the diractive to search for</param>
        /// <returns></returns>
        public static void DirSearch(string[] lFile)
        {


            if (lFile[0].ToString() == "3") // Exit
            {

            }
            else
            {
                if (lFile[1].ToString() != null) // search pattern
                {
                    string dirPath;


                    try
                    {
                        if (lFile[2] != null) // directory
                        {
                            dirPath = lFile[2].ToString();
                        }
                        else
                        {
                            dirPath = @"c:\"; // if choose option 1
                        }

                        //for root directory
                        foreach (string f in Directory.GetFiles(dirPath))
                        {
                            string fn = Path.GetFileName(f);
                            if (fn.ToLower().Contains(lFile[1].ToString().ToLower()))
                            {
                                fileList.Add(f);
                                AddFileAction(lFile[1], lFile[2], f);
                            }
                        }

                        // for all sub directories
                        foreach (string dr in Directory.GetDirectories(dirPath))
                        {
                            try
                            {
                                foreach (string f in Directory.GetFiles(dr))
                                {
                                    string fn = Path.GetFileName(f);
                                    if (fn.ToLower().Contains(lFile[1].ToString().ToLower()))
                                    {
                                        fileList.Add(f);
                                        AddFileAction(lFile[1], lFile[2], f);
                                    }
                                }
                                foreach (string directory in Directory.GetDirectories(dr))
                                {
                                    string[] tmp = lFile;
                                    tmp[2] = directory;
                                    DirSearch(tmp);
                                }
                            }
                            catch (Exception ex)
                            {
                                // Console.WriteLine(ex.Message);  
                            }
                        }
                    }
                    catch (System.Exception ex)
                    {

                        //Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        public static void AddResult(string[] start)
        {

            foreach (var item in FileManager.fileList) // adding to 'results' DB the paths that has be found after representing to the client.
            {
                SearchManager.InsertResult(new Result() { ResultPath = item, SearchID = Convert.ToInt32(start[3]) });
            }
        }
    }
}
