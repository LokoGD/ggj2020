using System;
using UnityEngine;

[Serializable]
public class SceneReference : MonoBehaviour {
#pragma warning disable 0649
#if UNITY_EDITOR
    public UnityEditor.SceneAsset scene;
#endif

    [SerializeField]
    private string sceneName;
#pragma warning restore 0649

    public string SceneToLoadName {
        get {
            return sceneName;
        }
        set {
            sceneName = value;
        }
    }
}