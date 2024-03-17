using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subject
{
    private List<IObserver> _observers = new List<IObserver>();

    public void AddObserver(IObserver observer)
    {
        _observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        _observers.Remove(observer);
    }

    public void NotifyObservers(string msg)
    {
        foreach (var observer in _observers)
        {
            observer.OnNotify(msg);
        }
    }
}