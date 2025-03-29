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
    public TextMemento Memento { get; set; }
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