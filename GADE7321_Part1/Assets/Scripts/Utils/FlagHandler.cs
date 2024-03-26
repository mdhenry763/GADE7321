using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Utils;

public class FlagHandler : MonoBehaviour
{
    public GameObject flagPrefab;
    public FlagHolder flagHolder;
    public GameObject flagVisual;
    public Respawner flagSpawner;
    
    private FlagComponent _flagComp;
    
    private void OnTriggerEnter(Collider other)
    {
        CheckFlagEvent(other);
    }

    private void OnCollisionEnter(Collision other)
    {
        CheckFlagEvent(other.collider);
    }

    private void CheckFlagEvent(Collider other) //Check to see if Entity is picking up their flag or depositing flag
    {
        //Flag Pick Up Check
        if (other.TryGetComponent<FlagComponent>( out FlagComponent flag))
        {
            if (flag.FlagHolder == flagHolder)
            {
                PickUpFlag();
            }
            else
            {
                PickUpOpponentFlag(flag);
            }
        }
        //Flag Drop check
        if (other.TryGetComponent<ScoreDeposit>(out var scoreDepo))
        {
            if (scoreDepo.flagHolder == flagHolder)
            {
                flagVisual.SetActive(false);
            }
        }
        
    }

    //Reset opponents flag if picked up
    private void PickUpOpponentFlag(FlagComponent comp)
    {
        var isPlayer = FlagHolder.Player == comp.FlagHolder;
        var trans = isPlayer ? flagSpawner.playerFlagSpawn : flagSpawner.enemyFlagSpawn;
        flagSpawner.SpawnFlag(true, isPlayer, trans.position);
    }
   

    private void PickUpFlag()
    {
        FlagComponent component = GetComponent<FlagComponent>();
        component.isHolding = true;
        component.FlagHolder = flagHolder;
        flagVisual.SetActive(true);
        
        var isPlayer = flagHolder == FlagHolder.Player;
        var trans = isPlayer ? flagSpawner.playerFlagSpawn : flagSpawner.enemyFlagSpawn;
        flagSpawner.SpawnFlag(false, isPlayer, trans.position);
    }

    public void DropFlag(Vector3 direction, float power)
    {
        FlagComponent component = GetComponent<FlagComponent>();
        component.isHolding = false;
        
        flagVisual.SetActive(false);
        Vector3 newPos = GetRandomPos(direction, power);
        
        var isPlayer = flagHolder == FlagHolder.Player;
        flagSpawner.SpawnFlag(true, isPlayer, newPos);
    }

    private Vector3 GetRandomPos(Vector3 direction, float power)
    {
        //Get right, left, forward and backwards direction from direction
        Vector3[] directions = new Vector3[]
        {
            new Vector3(-direction.z, direction.y, direction.x), //Left
            new Vector3(direction.z, direction.y, -direction.x) , //right
            direction.normalized, 
            -direction.normalized, //Right
        };

        bool foundPosition = false;
        Vector3 dropPosition = Vector3.zero;

        foreach (Vector3 dir in directions) //Try find a suitable position if not drop flag at entities feet
        {
            Vector3 attemptPosition = transform.position + (dir * power);
            if (NavMesh.SamplePosition(attemptPosition, out NavMeshHit hit, power, NavMesh.AllAreas))
            {
                dropPosition = hit.position;
                foundPosition = true;
                break; 
            }
        }

        if (!foundPosition)
        {
            Debug.LogWarning("No valid NavMesh position found in any direction, Dropping at player's current position.");
            dropPosition = transform.position; 
        }

        return dropPosition; //Return the drop position

    }

    public void ResetEntity()
    {
        _flagComp.isHolding = false;
        flagVisual.SetActive(false);
    }
    

   
}
