using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public enum NodeState
{
    Running,
    Success,
    Failure,
}

[System.Serializable]
public abstract class Node
{
    protected NodeState _nodeState;
    private Subject _subject;

    public Node()
    {
        _subject = new Subject();
    }

    public NodeState nodeState
    {
        get { return _nodeState; }
    }
    
    //Observer
    public void AddObserver(IObserver obs) => _subject.AddObserver(obs);
    public void RemoveObserver(IObserver obs) => _subject.RemoveObserver(obs);
    protected void NotifyObservers(string msg) => _subject.NotifyObservers(msg);
    
    public abstract NodeState Evaluate();
}
