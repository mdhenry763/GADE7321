using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subject
{
    private List<IBTObserver> _observers = new List<IBTObserver>();

    public void AddObserver(IBTObserver ibtObserver)
    {
        _observers.Add(ibtObserver);
    }

    public void RemoveObserver(IBTObserver ibtObserver)
    {
        _observers.Remove(ibtObserver);
    }

    public void NotifyObservers(string msg)
    {
        foreach (var observer in _observers)
        {
            observer.OnNotify(msg);
        }
    }
}