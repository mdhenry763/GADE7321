using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FlagComponent
{
    public GameObject FlagPrefab;
    public Transform FlagParent;
    public FlagHolder FlagHolder;

}

public static class CheckFlag
{

    public static bool IsCarryFlag(Transform entity)
    {
        if(entity.TryGetComponent<FlagComponent>(out FlagComponent flag))
        {
            return true;
        }

        return false;
    }
}

public enum FlagHolder
{
    Enemy,
    Player
}
    
   
