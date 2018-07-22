using DAL;

namespace BLL
{
    public class SearchManager
    {
        public static int InsertSearch(Search newSearch) // befor searching. get the name of the search input and the parent directory if exsist
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
        public static void InsertResult(Result newResult) // after. when the search is done. 
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
