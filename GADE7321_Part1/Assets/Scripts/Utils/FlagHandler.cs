using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Utils;

public class FlagHandler : MonoBehaviour
{
    public FlagHolder flagHolder;
    public GameObject flagVisual;
    public Respawner flagSpawner;

    private Subject _subject;

    private void Start()
    {
        _subject = new Subject();
    }


    private void OnTriggerEnter(Collider other)
    {
        CheckFlagEvent(other);
    }

    private void OnCollisionEnter(Collision other)
    {
        CheckFlagEvent(other.collider);
    }

    private void CheckFlagEvent(Collider other)
    {
        
        if (other.TryGetComponent<FlagComponent>( out FlagComponent flag))
        {
            if (flag.FlagHolder == flagHolder)
            {
                PickUpFlag();
            }
        }

        if (other.TryGetComponent<ScoreDeposit>(out var scoreDepo))
        {
            if (scoreDepo.flagHolder == flagHolder)
            {
                flagVisual.SetActive(false);
            }
        }
        
    }
    
    //Test
   

    private void PickUpFlag()
    {
        FlagComponent component = GetComponent<FlagComponent>();
        component.isHolding = true;
        component.FlagHolder = flagHolder;
        flagVisual.SetActive(true);
        
        var isPlayer = flagHolder == FlagHolder.Player;
        flagSpawner.SpawnFlag(false, isPlayer);
    }

   
}
