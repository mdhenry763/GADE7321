using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Utils
{
    public class Respawner : MonoBehaviour
    {
        [SerializeField] private GameObject _playerFlag;
        [SerializeField] private GameObject _enemyFlag;

        private void Start()
        {
            SpawnFlag(true, true);
            SpawnFlag(true, false);
        }

        public void SpawnFlag(bool spawn,bool isPlayer)
        {
            if (isPlayer != true)
                _enemyFlag.SetActive(spawn);
            else
                _playerFlag.SetActive(spawn);
        }
        
        
    }
}