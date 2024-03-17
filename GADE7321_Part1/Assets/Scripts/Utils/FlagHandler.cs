using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlagHandler : MonoBehaviour
{
    public FlagHolder _flagHolder;

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
            if (flag.FlagHolder == _flagHolder)
            {
                PickUpFlag();
            }
            else
            {
                ReturnFlagToEnemyBase();
            }
        }
    }

    private void PickUpFlag()
    {
        FlagComponent component = transform.AddComponent<FlagComponent>();
        component.FlagHolder = _flagHolder;
        component.FlagParent = transform;
        _subject.NotifyObservers("Flag Pick-Up");
        
    }

    private void ReturnFlagToEnemyBase()
    {
        _subject.NotifyObservers("Enemy Flag pick-up");
    }
}
