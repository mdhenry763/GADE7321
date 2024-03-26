using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subject //Class is type of event system for the enemy nodes, to notify the EnemyAI class
{
    private List<IBTObserver> _observers = new List<IBTObserver>();

    public void AddObserver(IBTObserver ibtObserver) //Add listener
    {
        _observers.Add(ibtObserver);
    }

    public void RemoveObserver(IBTObserver ibtObserver) //Remove listener
    {
        _observers.Remove(ibtObserver);
    }

    public void NotifyObservers(string msg, AIState state) //Notify Listeners of AIState
    {
        foreach (var observer in _observers)
        {
            observer.OnNotify(msg);
            observer.OnAIStateChange(state);
        }
    }
}