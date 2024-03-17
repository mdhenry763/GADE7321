using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagHandler : MonoBehaviour
{
    public FlagHolder _flagHolder;

    private void Start()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<FlagComponent>( out FlagComponent flag))
        {
            
        }
    }
}
