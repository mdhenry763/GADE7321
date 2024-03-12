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

public enum FlagHolder
{
    Enemy,
    Player
}

namespace Utils
{
    public static class HelperMethods
    {
        public static bool IsDistanceLessThan(Transform entityA, Transform entityB, float distance)
        {
            float calcDistance = Vector3.Distance(entityA.position, entityB.position);
            return calcDistance < distance;
        }

        public static bool IsCarryFlag(Transform entity)
        {
            if(entity.TryGetComponent<FlagComponent>(out FlagComponent flag))
            {
                return true;
            }

            return false;
        }
    }
}



    
   
