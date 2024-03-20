using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public enum PlayerMovementState
{
    Attacking,
    Running,
    Idle,
}

public class PlayerMovement : MonoBehaviour
{

    [Header("References: ")] 
    public PAnimController animControl;
    public NavMeshAgent agent;
    
    [Header("Settings: ")] 
    public float turnRate = 45;
    public float playerSpeed = 7;
    
    
    private Controls playerActions;
    private PlayerMovementState _playerMovementState;
    
    private void Awake()
    {
        if (playerActions == null)
        {
            playerActions = new Controls();
        }
    }

    private void OnEnable()
    {
        playerActions.Player.Enable();
        playerActions.Player.Fire.performed += HandlePunch;
    }

    private void HandlePunch(InputAction.CallbackContext obj)
    {
        _playerMovementState = PlayerMovementState.Attacking;
        animControl.PunchAnim();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = playerActions.Player.Move.ReadValue<Vector2>();
        MovePlayer(input);
        RotatePlayer(input);
    }

    void MovePlayer(Vector2 input)
    {
        Vector3 moveDir = new Vector3(0, 0, input.y);
        Vector3 movePos = transform.forward * input.y;

        if (moveDir.sqrMagnitude != 0)
        {
            _playerMovementState = PlayerMovementState.Running;
            //transform.Translate(movePos * Time.deltaTime * playerSpeed, Space.World);
            agent.Move(movePos * Time.deltaTime * playerSpeed);
        }

        if (_playerMovementState == PlayerMovementState.Running)
        {
            animControl.RunAnim(input.y);
        }
        
    }

    void RotatePlayer(Vector2 input)
    {
        Vector3 moveDir = new Vector3(0, input.x, 0);
        transform.Rotate(moveDir * turnRate);
    }
    
}
