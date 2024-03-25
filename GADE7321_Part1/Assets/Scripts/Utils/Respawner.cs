using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Utils
{
    public class Respawner : MonoBehaviour
    {
        [SerializeField] private GameObject _playerFlag;
        [SerializeField] private GameObject _enemyFlag;
        [SerializeField] private Transform playerSpawn;
        [SerializeField] private Transform enemySpawn;
        
        public Transform playerFlagSpawn;
        public Transform enemyFlagSpawn;
        

        private void Start()
        {
            SpawnFlag(true, true, playerFlagSpawn.position);
            SpawnFlag(true, false, enemyFlagSpawn.position);
        }

        public void SpawnFlag(bool spawn,bool isPlayer, Vector3 pos)
        {
            if (isPlayer != true)
            {
                _enemyFlag.transform.position = pos;
                _enemyFlag.SetActive(spawn);
            }

            else
            {
                _playerFlag.transform.position = pos;
                _playerFlag.SetActive(spawn);
            }
        }
        
        
    }
}