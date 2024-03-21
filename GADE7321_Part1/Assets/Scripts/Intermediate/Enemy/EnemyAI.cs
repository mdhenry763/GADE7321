using System;
using System.Collections;
using System.Collections.Generic;
using Intermediate.Enemy;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour, IBTObserver
{
    [SerializeField] private Transform punchRaycastPoint1;
    [SerializeField] private Transform punchRaycastPoint2;
    
    [SerializeField] private Subject _playerSubject;
    [SerializeField] private AIAnimController AIAnim;

    private AIState currentPlayerState;
    
    //Construction of BTree

    private void Awake()
    {
        currentPlayerState = AIState.Idle;
    }
    

    public void OnNotify(string msg)
    {
        Debug.Log("message: " + msg);
    }

    public void OnAIStateChange(AIState state)
    {
        Debug.Log("!!!State Changed!!!");
        if(state == currentPlayerState) return;
        
        AIAnim.PlayAttackingAnim(false);
        AIAnim.PlayEvadeAnim(false);
        AIAnim.PlayRunningAnim(0);
        
        
        currentPlayerState = state;

        switch (state)
        {
            case AIState.Attacking:
                AIAnim.PlayAttackingAnim(true);
                break;
            case AIState.Evading:
                AIAnim.PlayEvadeAnim(true);
                break;
            case AIState.Running:
                AIAnim.PlayRunningAnim(1);
                break;
            default:
                AIAnim.PlayRunningAnim(0);
              break;  
        }
    }
}

public enum AIState
{
    Running,
    Attacking,
    Idle,
    Evading
}
