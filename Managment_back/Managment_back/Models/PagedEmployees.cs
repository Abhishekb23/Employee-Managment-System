namespace Managment_back.Models
{
    public class PagedList<T>
    {

       public List<T> Items { get; set; }
      public  int PageIndex { get; set; }
      public  int TotalCount { get; set; }
      public  int PageSize { get; set; }

    }
}
