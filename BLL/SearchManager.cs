using DAL;

namespace BLL
{
    public class SearchManager
    {
        /// <summary>
        /// befor searching. get the name of the search input and the parent directory if exsist
        /// <summary>
        public static int InsertSearch(Search newSearch) 
        {
            using (SearchProjEntities ef = new SearchProjEntities())
            {
                Search srcObj = new Search
                {
                    SearchPattern = newSearch.SearchPattern,
                    SearchDirectory = newSearch.SearchDirectory
                };
                ef.Searches.Add(srcObj);
                ef.SaveChanges();

                return srcObj.SearchID;
            }
        }
        /// <summary>
        /// gets the result after the search is done,
        /// and insert it to the DB.
        /// <summary>
        public static void InsertResult(Result newResult)  
        {
            using (SearchProjEntities ef = new SearchProjEntities())
            {
                ef.Results.Add(new Result
                {
                    SearchID = newResult.SearchID,
                    ResultPath = newResult.ResultPath
                });
                ef.SaveChanges();
            }
        }
    }
}
