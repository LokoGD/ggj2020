using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

//public class LevelManager : DontDestroyOnLoadSingleton<SceneManager>
public class LevelManager : DontDestroyOnLoadSingleton<MonoBehaviour>
{

    [SerializeField] List<string> easyScenes = new List<string>();
    [SerializeField] List<string> mediumScenes = new List<string>();
    [SerializeField] List<string> hardScenes = new List<string>();

    public static int _currentDifficulty;

    static readonly List<List<string>> DifficultyList = new List<List<string>>();
    //  No momento em que o Mock funcionar, fazer virar Hash:
    //HashSet<List<Scene>> _allScenesOfAllDifficultyLevelsHashSet = new HashSet<List<Scene>>();

    static readonly Stack<string> UnlockedScenesStack = new Stack<string>();

    void Start()
    {
        _currentDifficulty = 0;
        //alimentar o mapa de dificuldade x level
        //usar isso garantir que não vai ter duas difíceis em seguida (só difícil mesmo)
        AddAllToMainList();

        RepopulateSceneStack();
    }

    void AddAllToMainList()
    {
        DifficultyList.Add(easyScenes);
        DifficultyList.Add(mediumScenes);
        DifficultyList.Add(hardScenes);
    }

    static void RepopulateSceneStack()
    {
        List<string> allPossibleScenes = new List<string>();
        for (int difficultyIndex = 0; difficultyIndex <= _currentDifficulty; difficultyIndex++)
        {
            allPossibleScenes.AddRange(DifficultyList[difficultyIndex]);
        }
        
        while (allPossibleScenes.Count > 0)
        {
            int randomIndex = Random.Range(0, allPossibleScenes.Count);
            UnlockedScenesStack.Push(allPossibleScenes[randomIndex]);
                
            allPossibleScenes.RemoveAt(randomIndex);
        }
        
    }

    static string PopSceneAndRepopulateSceneStackIfNecessary()
    {
        if (UnlockedScenesStack.Count == 0)
        {
            int nextDifficulty = _currentDifficulty + 1;
            int maxDifficulty = DifficultyList.Count - 1;
            _currentDifficulty = Mathf.Min(nextDifficulty, maxDifficulty);
            RepopulateSceneStack();
        }

        return UnlockedScenesStack.Pop();
    }

    public static void EndCurrentSceneAndLoadNextLevelByItsScene()
    {
        var foo = PopSceneAndRepopulateSceneStackIfNecessary();
        //  Debug.Log("Keanu Reeves {foo}: " + foo);
        SceneManager.LoadScene(foo);
    }

}