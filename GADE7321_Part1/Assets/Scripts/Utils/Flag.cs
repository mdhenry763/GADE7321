using System;
using System.Collections.Generic;
using UnityEngine;


public class FlagComponent : MonoBehaviour
{
    public Transform FlagParent;
    public FlagHolder FlagHolder;
    public bool isHolding;

}

public enum FlagHolder
{
    Enemy,
    Player,
    None
}

namespace Utils
{
   
}



    
   
