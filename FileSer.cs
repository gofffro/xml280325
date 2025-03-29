using System;
using System.IO;
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
      using var memoryStream = new MemoryStream();
      using var binaryWriter = new BinaryWriter(memoryStream);

      if (FileName == null)
      {
        binaryWriter.Write(string.Empty);
      }
      else
      {
        binaryWriter.Write(FileName);
      } 

      return memoryStream.ToArray();
    }

    public static FileSer DeserializeBinary(byte[] data)
    {
      using var memoryStream = new MemoryStream(data);
      using var binaryReader = new BinaryReader(memoryStream);

      var fileName = binaryReader.ReadString();
      var content = binaryReader.ReadString();

      return new FileSer(fileName, content);
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