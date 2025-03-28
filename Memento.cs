using System;

namespace xml280325
{
  public class Memento
  {
    public string State { get; private set; }

    public Memento(string state)
    {
      State = state;
    }
  }

  public class Caretaker
  {
    public Memento Memento { get; set; }
  }

  public class Originator
  {
    public string State { get; set; }

    public void SetMemento(Memento memento)
    {
      State = memento.State;
    }

    public Memento CreateMemento()
    {
      return new Memento(State);
    }
  }
}