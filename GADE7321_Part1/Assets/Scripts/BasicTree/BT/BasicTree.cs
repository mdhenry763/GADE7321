using System.Collections;
using System.Collections.Generic;
using BasicBehaviourTree;
using UnityEngine;

public abstract class BasicTree : MonoBehaviour
{
    private BasicNode _root = null;

    protected void Start()
    {
        _root = SetupTree();
    }

    private void Update()
    {
        if (_root != null)
            _root.Evaluate();
    }

    protected abstract BasicNode SetupTree();
}
