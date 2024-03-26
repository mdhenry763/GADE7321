using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Utils
{
    public class Respawner : MonoBehaviour //Flag Respawner and player respanwer
    {
        [Header("Spawn References:")]
        public Transform playerFlagSpawn;
        public Transform enemyFlagSpawn;
        
        [SerializeField] private GameObject playerFlag;
        [SerializeField] private GameObject enemyFlag;
 
        
        [Header("Entity References:")]
        [SerializeField] Transform player;
        [SerializeField] Transform enemy;
        [SerializeField] private Transform playerSpawn;
        [SerializeField] private Transform enemySpawn;
        
        
        private void Start()
        {
            //Spawn flags on game start
            SpawnFlag(true, true, playerFlagSpawn.position);
            SpawnFlag(true, false, enemyFlagSpawn.position);
            
            //Subscription
            ScoreDeposit.OnScored += RespawnPlayers;

        }

        private void RespawnPlayers()
        {
            //Respawn players between rounds
            player.position = playerSpawn.position;
            enemy.position = enemySpawn.position;
            
            SpawnFlag(true, true, playerFlagSpawn.position);
            SpawnFlag(true, false, enemyFlagSpawn.position);
            
            Reset();
        }

        private void Reset()
        {
            //Reset players after respawn
            FlagHandler playerHandler = player.GetComponent<FlagHandler>();
            FlagHandler enemyHandler = enemy.GetComponent<FlagHandler>();
            playerHandler.ResetEntity();
            enemyHandler.ResetEntity();
        }

        public void SpawnFlag(bool spawn,bool isPlayer, Vector3 pos) //Spawn or despawn flag based on isPlayer and use a position
        {
            if (isPlayer != true)
            {
                enemyFlag.transform.position = pos;
                enemyFlag.SetActive(spawn);
            }

            else
            {
                playerFlag.transform.position = pos;
                playerFlag.SetActive(spawn);
            }
        }
        
        
        
        
    }
}