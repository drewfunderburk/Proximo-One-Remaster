using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveSpawnerBehaviour : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        [Tooltip("Invoked when all enemies have been spawned")]
        public UnityEvent OnWaveFinishedSpawning;
        [Tooltip("Invoked when all enemies have been destroyed")]
        public UnityEvent OnWaveCompleted;

        private float _duration = 10;
        private float _spawnDelay = 1;
        private int _count = 1;
        private GameObject _enemy;
        [Space]
        private bool _spawning;
        private bool _finished;
        private List<EnemyHealthBehaviour> _enemyList;
    }

}
