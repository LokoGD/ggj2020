using UnityEngine;

public class DontDestroyOnLoadSingleton<T> : AbstractSingleton<T> where T : MonoBehaviour
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
}
