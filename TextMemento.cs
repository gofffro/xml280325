using System;

namespace xml280325
{
  public class TextMemento
  {
    public string State { get; private set; }

    public TextMemento(string state)
    {
      State = state;
    }
  }

  public class TextHistory
  {
    private TextMemento _memento;

    public void SaveMemento(TextMemento memento)
    {
      _memento = memento;
    }

    public TextMemento GetMemento()
    {
      if (_memento != null)
      {
        return _memento;
      }
      throw new InvalidOperationException("Нет состояний для отката");
    }
  }

  public class TextEditor
  {
    public string State { get; set; }

    public void SetMemento(TextMemento memento)
    {
      State = memento.State;
    }

    public TextMemento CreateMemento()
    {
      return new TextMemento(State);
    }
  }
}