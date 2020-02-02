using System.Collections;
using System.Collections.Generic;
using HalliHacks.Pulse;
using UnityEngine;

public class TimedLevelChangeManager : DontDestroyOnLoadSingleton<TimedLevelChangeManager>
{
    [SerializeField] private float timeToChangeLevel;
    void Start()
    {
        Pulse.Every(timeToChangeLevel).Do
        (() => { LevelManager.EndCurrentSceneAndLoadNextLevelByItsScene(); }
        );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
