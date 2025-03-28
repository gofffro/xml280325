using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace xml280325
{
  [Serializable]
   public class FileSer
  {
    public string FileName { get; set; }
    public string Content {  get; set; }

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

    public void SerializeBinary(FileStream fs)
    {
      JsonSerializer.Serialize(fs, this);
      fs.Flush();
      fs.Close();
    }

    public static FileSer DeserializeBinary(FileStream fs)
    {
      FileSer deserialized = JsonSerializer.Deserialize<FileSer>(fs);
      fs.Close();
      return deserialized;
    }

    public void SerializeXML(FileStream fs)
    {
      var serializer = new XmlSerializer(typeof(FileSer));
      serializer.Serialize(fs, this);
      fs.Flush();
      fs.Close();
    }

    public static FileSer DeserializeXML(FileStream fs)
    {
      var serializer = new XmlSerializer(typeof(FileSer));
      FileSer deserialized = (FileSer)serializer.Deserialize(fs);
      fs.Close();
      return deserialized;
    }
  }
}
