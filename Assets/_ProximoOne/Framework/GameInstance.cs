using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstance : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private PlayerHealthBehaviour _playerHealthBehaviour;

    public static GameInstance Instance {get; private set;}
    public PlayerController PlayerController { get => _playerController; set => _playerController = value; }
    public PlayerHealthBehaviour PlayerHealthBehaviour { get => _playerHealthBehaviour; set => _playerHealthBehaviour = value; }

    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        // If PlayerController or PlayerHealthBehaviour is null, attempt to find them
        if (!PlayerController || !PlayerHealthBehaviour)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player)
            {
                if (!PlayerController)
                    PlayerController = player.GetComponent<PlayerController>();
                if (!PlayerHealthBehaviour)
                    PlayerHealthBehaviour = player.GetComponent<PlayerHealthBehaviour>();
            }
            else
                Debug.LogError("GameInstance: Could not find object with the tag \"Player\"");

            if (!PlayerController)
                Debug.LogError("GameInstance: PlayerController not found");
            if (!PlayerHealthBehaviour)
                Debug.LogError("GameInstance: PlayerHealthBehaviour not found");
        }
    }
}
