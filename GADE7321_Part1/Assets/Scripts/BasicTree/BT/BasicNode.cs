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
        private Dictionary<string, object> _dataContext = new();

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

        public void SetData(string key, object value)
        {
            _dataContext[key] = value;
        }

        public object GetData(string key)
        {
            object value = null;
            if (_dataContext.TryGetValue(key, out value))
                return value;

            BasicNode node = parent;
            while (node != null)
            {
                value = node.GetData(key);
                if (value != null)
                    return value;
                node = node.parent;
            }
            return null;
        }

        public bool ClearData(string key)
        {
            if (_dataContext.ContainsKey(key))
            {
                _dataContext.Remove(key);
                return true;
            }

            BasicNode node = parent;
            while (node != null)
            {
                bool cleared = node.ClearData(key);
                if (cleared)
                    return true;
                node = node.parent;
            }
            return false;
        }

    }
}
        

