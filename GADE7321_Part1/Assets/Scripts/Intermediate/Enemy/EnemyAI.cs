using System;
using System.Collections;
using System.Collections.Generic;
using Intermediate.Enemy;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour, IBTObserver
{
    [SerializeField] private Subject _playerSubject;
    [SerializeField] private AIAnimController AIAnim;
    [SerializeField] private TMP_Text statusText;

    private AIState currentPlayerState;
    private Attack _attack;
    
    //Construction of BTree

    private void Awake()
    {
        currentPlayerState = AIState.Idle;
        _attack = GetComponent<Attack>();
    }
    

    public void OnNotify(string msg)
    {
        Debug.Log("message: " + msg);
    }

    private void Update()
    {
        if (currentPlayerState == AIState.Attacking)
        {
            //Use a sphere cast
        }
    }

    public void OnAIStateChange(AIState state) //Perform various actions based one enemy state
    {
        if(state == currentPlayerState) return;
        
        //Reset animations
        AIAnim.PlayAttackingAnim(false);
        AIAnim.PlayEvadeAnim(false);
        AIAnim.PlayRunningAnim(0);
        
        
        currentPlayerState = state;

        switch (state) //state based animations
        {
            case AIState.Attacking:
                AIAnim.PlayAttackingAnim(true);
                _attack.AttackOpponent();
                break;
            case AIState.Evading:
                AIAnim.PlayEvadeAnim(true);
                break;
            case AIState.Running:
                AIAnim.PlayRunningAnim(1);
                break;
            case AIState.Chasing:
                AIAnim.PlayRunningAnim(1);
                break;
            default:
                AIAnim.PlayRunningAnim(0);
                break;  
        }

        statusText.text = state.ToString();
        
        Debug.Log($"!!!State Changed - to - {state}!!!");
    }
}

public enum AIState
{
    Running,
    Attacking,
    Idle,
    Evading,
    Chasing,
    ResetFlag
}
