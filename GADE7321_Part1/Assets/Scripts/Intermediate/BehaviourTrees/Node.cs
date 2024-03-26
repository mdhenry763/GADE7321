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
public abstract class Node //base class, foundation for behaviour tree
{
    protected NodeState _nodeState; //Used for the state of child nodes
    private Subject _subject; //composition with a observer pattern

    public Node()
    {
        _subject = new Subject();
    }

    public NodeState nodeState
    {
        get { return _nodeState; }
    }
    
    //Observer
    public void AddObserver(IBTObserver obs) => _subject.AddObserver(obs); // Add Listeners to node
    public void RemoveObserver(IBTObserver obs) => _subject.RemoveObserver(obs); //Remove Listeners from node
    protected void NotifyObservers(string msg, AIState state) => _subject.NotifyObservers(msg, state); // Fire msg and state to listeners
    
    public abstract NodeState Evaluate(); //Where nodes will be evaluated
}
