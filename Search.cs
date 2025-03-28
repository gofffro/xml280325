using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xml280325
{
  public class Search
  {
    public string DirectoryPath { get; set; }
    public string Keyword { get; set; }

    public Search(string keyword, string directoryPath)
    {
      DirectoryPath = directoryPath;
      Keyword = keyword;
    }

  }
}
