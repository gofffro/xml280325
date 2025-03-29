﻿using System;
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
            return;

          case "1":
            Console.Write("Директория: ");
            string dir = Console.ReadLine();
            Console.Write("Ключевое слово: ");
            string keyword = Console.ReadLine();
            search = new Search(keyword, dir);
            search.Result();
            break;

          case "2":
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
            break;

          case "3":
            if (string.IsNullOrEmpty(currentFilePath))
            {
              Console.WriteLine("Сначала откройте файл (пункт 2)");
              break;
            }
            textManager.SaveState();
            Console.Write("Текст для записи: ");
            string text = Console.ReadLine();
            File.AppendAllText(currentFilePath, text + Environment.NewLine);
            textManager.LoadFromFile(currentFilePath);
            Console.WriteLine("Изменения сохранены");
            break;

          case "4":
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
            break;

          case "5":
            if (string.IsNullOrEmpty(currentFilePath))
            {
              Console.WriteLine("Сначала откройте файл (пункт 2)");
              break;
            }
            var fileSer = new FileSer(currentFilePath, textManager.GetCurrentText());
            byte[] binaryData = fileSer.SerializeBinary();
            File.WriteAllBytes(currentFilePath + ".bin", binaryData);
            Console.WriteLine("Файл сохранён в бинарном формате");
            break;

          case "6":
            Console.Write("Путь к .bin файлу: ");
            string binPath = Console.ReadLine();
            if (File.Exists(binPath))
            {
              byte[] data = File.ReadAllBytes(binPath);
              FileSer loadedFile = FileSer.DeserializeBinary(data);
              Console.WriteLine($"Загружен файл: {loadedFile.FileName}\n{loadedFile.Content}");
            }
            else
            {
              Console.WriteLine("Файл не найден");
            }
            break;

          case "7":
            if (string.IsNullOrEmpty(currentFilePath))
            {
              Console.WriteLine("Сначала откройте файл (пункт 2)");
              break;
            }
            var fileSerXml = new FileSer(currentFilePath, textManager.GetCurrentText());
            using (var fs = File.Create(currentFilePath + ".xml"))
            {
              fileSerXml.SerializeXML(fs);
            }
            Console.WriteLine("Файл сохранён в XML");
            break;

          case "8":
            Console.Write("Путь к .xml файлу: ");
            string xmlPath = Console.ReadLine();
            if (File.Exists(xmlPath))
            {
              using (var fs = File.OpenRead(xmlPath))
              {
                FileSer loadedFile = FileSer.DeserializeXML(fs);
                Console.WriteLine($"Загружен файл: {loadedFile.FileName}\n{loadedFile.Content}");
              }
            }
            else
            {
              Console.WriteLine("Файл не найден");
            }
            break;

          default:
            Console.WriteLine("Неверный ввод");
            break;
        }
      }
    }
  }
}