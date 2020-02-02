using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static AbstractSingleton<T> Instance;
    
    protected virtual void Awake()
    {
        //Check if instance already exists
        if (Instance == null)
        {
            //if not, set instance to this
            Instance = this;
        }
        //If instance already exists and it's not this:
        else if (Instance != this)
        {
            Destroy(gameObject);   
        } 
    }
    
}
