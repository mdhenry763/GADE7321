using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BasicBehaviourTree
{
    public enum BasicNodeStates
    {
        Running,
        Success,
        Failure
    }
    
    public class BasicNode
    {
        protected BasicNodeStates state;

        public BasicNode parent;
        protected List<BasicNode> children;
        
        //Take care of shared data
        private Dictionary<string, object> dataContext = new();

        public BasicNode()
        {
            parent = null;
        }

        public BasicNode(List<BasicNode> children)
        {
            foreach (BasicNode child in children)
                Attach(child);
        }

        private void Attach(BasicNode node)
        {
            node.parent = this;
            children.Add(node);
        }

        public virtual BasicNodeStates Evaluate() => BasicNodeStates.Failure;



    }
}
        

