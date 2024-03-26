using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("Attacking Settings: ")] 
    [SerializeField] private float attackDistance;

    [SerializeField] private float power = 2f;
    [SerializeField] private AudioSource audio;
    [SerializeField] private AudioClip clip;
    [SerializeField] private Transform opponent;

    private FlagHandler _flag;

    private void Start()
    {
        _flag = GetComponent<FlagHandler>();
    }

    public void AttackOpponent()
    {
        //Play Punch sound
        if(!audio.isPlaying)
            audio.PlayOneShot(clip);

        //Attack is distance based, almost like a radius
        float distance = Vector3.Distance(transform.position, opponent.position);
        if (distance <= attackDistance) //Entity will drop their flag if they were within a certain distance
        {
            FlagHandler flagHandler = opponent.GetComponent<FlagHandler>();
            var flagComponent = opponent.GetComponent<FlagComponent>();
            if (flagHandler.flagHolder != _flag.flagHolder && flagComponent.isHolding)
            {
                Vector3 direction = transform.position - opponent.transform.position;
                flagHandler.DropFlag(direction, power);
            }
        }
    }
    
    
    /*Debug.Log("hit");
        RaycastHit hit;

        if (Physics.SphereCast(transform.position, attackRadius, transform.forward, out hit, 10))
        {
            Debug.Log($"Somethings: {hit.collider.name}");
            if (hit.collider.TryGetComponent<FlagHandler>(out var comp))
            {
                var test = comp.GetComponent<FlagComponent>();
                if (comp.flagHolder != _flag.flagHolder && test.isHolding == true)
                {
                    Debug.Log($"Hit Opponent: {hit.collider.name}");
                    Vector3 direction = transform.position - hit.collider.transform.position;
                    comp.DropFlag(direction, power);
                }
            }
        }*/
}
