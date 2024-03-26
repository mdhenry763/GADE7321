using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Utils;

public class SpeedBoost : MonoBehaviour
{
    public bool isWorking = true;
    [Header("Settings: ")]
    public float speedBoostTime = 3f;
    public float speedBoostMultiplier;
    public float boostCooldownTime = 20f;
    public float moveSpeed;

    [Header("Visual")] [SerializeField] private GameObject visual;

    [Header("Waypoints")] [SerializeField] private Transform[] waypoints;

    private int index = 0;
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isWorking) return;
        
        if (other.TryGetComponent<EnemyAI>(out var enemy))
        {
            var agent = other.GetComponent<NavMeshAgent>();
            StartCoroutine(EffectEnemySpeed(agent));
        }

        if (other.TryGetComponent<PlayerMovement>(out var playerMovement))
        {
            StartCoroutine(EffectPlayerSpeed(playerMovement));
            
        }
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, waypoints[index].position);

        if (distance > 1) //Move coin in circle
        {
            transform.position = Vector3.Lerp(
                transform.position, waypoints[index].position, Time.deltaTime * moveSpeed);
        }
        else
        {
            index = (index + 1) % waypoints.Length;
        }
    }


    //Change player or enemy speed after trigger
    IEnumerator EffectEnemySpeed(NavMeshAgent enemyAI)
    {
        isWorking = false;
        visual.SetActive(false);
        Debug.Log("Speed Boost Enemy");
        WaitForSeconds wait = new WaitForSeconds(speedBoostTime);
        enemyAI.speed *= speedBoostMultiplier;
        yield return wait;
        enemyAI.speed /= speedBoostMultiplier;
        StartCoroutine(SpeedCooldown());
    }

    IEnumerator EffectPlayerSpeed(PlayerMovement playerMove)
    {
        isWorking = false;
        visual.SetActive(false);
        playerMove.playerSpeed *= speedBoostMultiplier;
        yield return new WaitForSeconds(speedBoostTime);
        playerMove.playerSpeed /= speedBoostMultiplier;
        Debug.Log("Speed Boost Player");
        StartCoroutine(SpeedCooldown());
    }

    //Switch off speed boost
    IEnumerator SpeedCooldown()
    {
        yield return new WaitForSeconds(boostCooldownTime);
        isWorking = true;
        visual.SetActive(true);
    }
}
