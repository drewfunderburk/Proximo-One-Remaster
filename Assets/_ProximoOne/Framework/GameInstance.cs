using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstance : MonoBehaviour
{
    public GameInstance Instance {get; private set;}

    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }
}
