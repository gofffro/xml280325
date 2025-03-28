using System;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;

namespace xml280325
{
  [Serializable]
  public class FileSer
  {
    public string FileName { get; set; }
    public string Content { get; set; }

    public FileSer() { }

    public FileSer(string filename, string content)
    {
      FileName = filename;
      Content = content;
    }

    public void Print()
    {
      Console.WriteLine($"Имя файла: {FileName}");
      Console.WriteLine($"Контент: {Content}");
    }

    public byte[] SerializeBinary()
    {
      return JsonSerializer.SerializeToUtf8Bytes(this);
    }

    public static FileSer DeserializeBinary(byte[] data)
    {
      return JsonSerializer.Deserialize<FileSer>(data);
    }

    public void SerializeXML(FileStream fs)
    {
      var serializer = new XmlSerializer(typeof(FileSer));
      serializer.Serialize(fs, this);
      fs.Flush();
    }

    public static FileSer DeserializeXML(FileStream fs)
    {
      var serializer = new XmlSerializer(typeof(FileSer));
      return (FileSer)serializer.Deserialize(fs);
    }
  }
}