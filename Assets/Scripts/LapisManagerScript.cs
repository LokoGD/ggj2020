using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LapisManagerScript : MonoBehaviour
{
    public GameObject lapis_prefarb;
    public int randomedLapisID;

    int numberOfRands;
    List<int> lapisJaSorteados = new List<int>();


    // Start is called before the first frame update
    void Start()
    {
        numberOfRands = LevelManager._currentDifficulty + 1;

        for (int i = 0; i < 14; i++)
        {
            GameObject _temp = Instantiate(lapis_prefarb, new Vector3(-4.4f + (i * 0.68f), -8.5f, -0.001f), Quaternion.identity);
            
            //  _temp.GetComponent<LapisButtonInteract>().enabled = false;
            _temp.name = "lapis" + i;
        }

        do
        {
            randomedLapisID = Random.Range(0, 13);
            if (!lapisJaSorteados.Contains(randomedLapisID))
            {
                GameObject _tempRandomedLapis = GameObject.Find("lapis" + randomedLapisID);
                _tempRandomedLapis.AddComponent<BoxCollider>();
                _tempRandomedLapis.transform.position = new Vector3(_tempRandomedLapis.transform.position.x, -5.75f, -0.003f);

                numberOfRands--;
            }
        } while (numberOfRands > 0);

        //randomedLapisID = Random.Range(0, 13);

        //GameObject _tempRandomedLapis = GameObject.Find("lapis" + randomedLapisID);
        //_tempRandomedLapis.AddComponent<BoxCollider>();
        ////  Destroy(_tempRandomedLapis.gameObject);
        //_tempRandomedLapis.transform.position = new Vector3(_tempRandomedLapis.transform.position.x, -5.75f, -0.003f);


        //  17.5 / 15 / -0.001
        //  67.5 / 15 / -0.001


        //  166
        //  187
        //  208
        // 21 intervalo entre 1 e outro

        // Configs do Shader
        // Intensity = 0.15
        // Spread = 0.015
        // Offset X -0.004, Y = 0.004
        // Cutoff = 0.75
        // Threshold = minimum     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
