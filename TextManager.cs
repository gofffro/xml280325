using System;
using System.IO;

namespace xml280325
{
  public class TextManager
  {
    private readonly TextEditor _editor = new TextEditor();
    private readonly TextHistory _history = new TextHistory();

    public void SaveState()
    {
      _history.SaveMemento(_editor.CreateMemento());
    }

    public void RestoreState()
    {
      TextMemento memento = _history.GetMemento();
      _editor.SetMemento(memento);
    }

    public string GetCurrentText()
    {
      return _editor.State;
    }

    public void UpdateText(string newText)
    {
      _editor.State = newText;
    }

    public void SaveToFile(string filePath)
    {
      File.WriteAllText(filePath, _editor.State);
    }

    public void LoadFromFile(string filePath)
    {
      if (File.Exists(filePath))
      {
        _editor.State = File.ReadAllText(filePath);
      }
      else
      {
        throw new FileNotFoundException("Файл не найден", filePath);
      }
    }
  }
}