using System;
using System.IO;

namespace xml280325
{
  internal class Program
  {
    static void Main(string[] args)
    {
      string menuChoice;
      string currentFilePath = null;
      Search search = null;
      TextManager textManager = new TextManager();

      while (true)
      {
        Console.WriteLine(
            "Меню:\n" +
            "0) Выход\n" +
            "1) Поиск файлов по ключевому слову\n" +
            "2) Прочитать файл\n" +
            "3) Записать в файл\n" +
            "4) Откат изменений\n" +
            "5) Сохранить файл (бинарный формат)\n" +
            "6) Загрузить файл (бинарный формат)\n" +
            "7) Сохранить файл (XML)\n" +
            "8) Загрузить файл (XML)\n" +
            "Ввод: ");

        menuChoice = Console.ReadLine();
        Console.Clear();

        switch (menuChoice)
        {
          case "0":
            ExitProgram();
            return;

          case "1":
            PerformSearch(ref search);
            break;

          case "2":
            ReadFile(ref currentFilePath, textManager);
            break;

          case "3":
            WriteToFile(currentFilePath, textManager);
            break;

          case "4":
            RollbackChanges(textManager, currentFilePath);
            break;

          case "5":
            SaveBinaryFile(currentFilePath, textManager);
            break;

          case "6":
            LoadBinaryFile();
            break;

          case "7":
            SaveXmlFile(currentFilePath, textManager);
            break;

          case "8":
            LoadXmlFile();
            break;

          default:
            Console.WriteLine("Неверный ввод");
            break;
        }
      }
    }

    private static void ExitProgram()
    {
      Console.WriteLine("Выход из программы.");
    }

    private static void PerformSearch(ref Search search)
    {
      Console.Write("Директория: ");
      string dir = Console.ReadLine();

      if (search == null || search.DirectoryPath != dir)
      {
        search = new Search(dir);
        Console.WriteLine("Индексация файлов");
        search.IndexWordsFiles();
      }

      Console.Write("Ключевое слово: ");
      string keyword = Console.ReadLine();

      search.Result(keyword);
    }

    private static void ReadFile(ref string currentFilePath, TextManager textManager)
    {
      Console.Write("Путь к файлу: ");
      currentFilePath = Console.ReadLine();
      try
      {
        textManager.LoadFromFile(currentFilePath);
        Console.WriteLine(textManager.GetCurrentText());
      }
      catch (FileNotFoundException)
      {
        Console.WriteLine("Файл не найден");
      }
    }

    private static void WriteToFile(string currentFilePath, TextManager textManager)
    {
      if (string.IsNullOrEmpty(currentFilePath))
      {
        Console.WriteLine("Сначала откройте файл (пункт 2)");
        return;
      }

      textManager.SaveState();
      Console.Write("Текст для записи: ");
      string text = Console.ReadLine();
      File.AppendAllText(currentFilePath, text + Environment.NewLine);
      textManager.LoadFromFile(currentFilePath);
      Console.WriteLine("Изменения сохранены");
    }

    private static void RollbackChanges(TextManager textManager, string currentFilePath)
    {
      try
      {
        textManager.RestoreState();
        textManager.SaveToFile(currentFilePath);
        Console.WriteLine("Откат выполнен");
      }
      catch (InvalidOperationException ex)
      {
        Console.WriteLine(ex.Message);
      }
    }

    private static void SaveBinaryFile(string currentFilePath, TextManager textManager)
    {
      if (string.IsNullOrEmpty(currentFilePath))
      {
        Console.WriteLine("Сначала откройте файл (пункт 2)");
        return;
      }

      var fileSer = new FileSer(currentFilePath, textManager.GetCurrentText());
      byte[] binaryData = fileSer.SerializeBinary();
      File.WriteAllBytes(currentFilePath + ".bin", binaryData);
      Console.WriteLine("Файл сохранён в бинарном формате");
    }

    private static void LoadBinaryFile()
    {
      Console.Write("Путь к .bin файлу: ");
      string binPath = Console.ReadLine();
      if (File.Exists(binPath))
      {
        byte[] data = File.ReadAllBytes(binPath);
        FileSer loadedFile = FileSer.DeserializeBinary(data);
        loadedFile.Print();
      }
      else
      {
        Console.WriteLine("Файл не найден");
      }
    }

    private static void SaveXmlFile(string currentFilePath, TextManager textManager)
    {
      if (string.IsNullOrEmpty(currentFilePath))
      {
        Console.WriteLine("Сначала откройте файл (пункт 2)");
        return;
      }

      var fileSerXml = new FileSer(currentFilePath, textManager.GetCurrentText());
      using (var fs = File.Create(currentFilePath + ".xml"))
      {
        fileSerXml.SerializeXML(fs);
      }
      Console.WriteLine("Файл сохранён в XML");
    }

    private static void LoadXmlFile()
    {
      Console.Write("Путь к .xml файлу: ");
      string xmlPath = Console.ReadLine();
      if (File.Exists(xmlPath))
      {
        using (var fs = File.OpenRead(xmlPath))
        {
          FileSer loadedFile = FileSer.DeserializeXML(fs);
          loadedFile.Print();
        }
      }
      else
      {
        Console.WriteLine("Файл не найден");
      }
    }
  }
}