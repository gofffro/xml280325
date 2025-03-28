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

    public List<string> FindFilesWithKeyword()
    {
      List<string> resultFiles = new List<string>();

      try
      {
        string[] allFiles = Directory.GetFiles(DirectoryPath, "*.*", SearchOption.AllDirectories);

        foreach (string file in allFiles)
        {
          try
          {
            string fileContent = File.ReadAllText(file, Encoding.Default);

            if (fileContent.IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) >= 0)
            {
              resultFiles.Add(file);
            }
          }
          catch (Exception ex)
          {
            Console.WriteLine($"Ошибка при чтении файла {file}: {ex.Message}");
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Ошибка при поиске файлов в директории {DirectoryPath}: {ex.Message}");
      }

      return resultFiles;
    }

    public void Result()
    {
      Console.WriteLine($"Поиск файлов, содержащих ключевое слово '{Keyword}' в директории '{DirectoryPath}':");

      List<string> foundFiles = FindFilesWithKeyword();

      if (foundFiles.Count > 0)
      {
        Console.WriteLine("Найденные файлы:");
        foreach (string file in foundFiles)
        {
          Console.WriteLine(file);
        }
      }
      else
      {
        Console.WriteLine("Файлы, содержащие ключевое слово, не найдены");
      }
    }

    public class Indexes
    {
      private readonly List<Search> _searches = new();

      public void AddSearch(Search search)
      {
        _searches.Add(search);
      }

      public void PerformAllSearches()
      {
        Console.WriteLine("Выполнение всех сохраненных поисков:");

        foreach (var search in _searches)
        {
          Console.WriteLine($"\nПоиск для ключевого слова '{search.Keyword}' в директории '{search.DirectoryPath}':");
          search.Result();
        }
      }
    }
  }
}
