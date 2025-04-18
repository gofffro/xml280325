using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace xml280325
{
  public class Search
  {
    public string DirectoryPath { get; set; }
    public Dictionary<string, List<string>> Index { get; private set; }

    public Search(string directoryPath)
    {
      DirectoryPath = directoryPath;
      Index = new Dictionary<string, List<string>>();
    }

    public void IndexWordsFiles()
    {
      Index.Clear();
      try
      {
        string[] allFiles = Directory.GetFiles(DirectoryPath, "*.*", SearchOption.AllDirectories);

        foreach (string file in allFiles)
        {
          try
          {
            string fileContent = File.ReadAllText(file, Encoding.Default);
            string[] words = fileContent.Split(new[] { ' ', '\t', '\n', '\r', '.', ',', ';', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string word in words)
            {
              if (!Index.ContainsKey(word))
              {
                Index[word] = new List<string>();
              }

              if (!Index[word].Contains(file))
              {
                Index[word].Add(file);
              }
            }
          }
          catch (Exception ex)
          {
            Console.WriteLine($"Ошибка при индексации файла {file}: {ex.Message}");
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Ошибка при доступе к директории {DirectoryPath}: {ex.Message}");
      }
    }

    public List<string> FindFilesWithKeyword(string keyword)
    {
      if (Index.Count == 0)
      {
        IndexWordsFiles();
      }

      if (Index.TryGetValue(keyword, out List<string> files))
      {
        return files;
      }

      return new List<string>();
    }

    public void Result(string keyword)
    {
      Console.WriteLine($"Поиск файлов, содержащих ключевое слово '{keyword}' в директории '{DirectoryPath}':");

      List<string> foundFiles = FindFilesWithKeyword(keyword);

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
  }
}