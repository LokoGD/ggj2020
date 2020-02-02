using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LapisManagerScript : MonoBehaviour
{
    public GameObject lapis_prefarb;

    //public GameObject canvasToParent;
    //public GameObject buttonPrefab;

    public int randomedLapisID;    

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 14; i++)
        {
            GameObject _temp = Instantiate(lapis_prefarb, new Vector3(-4.4f + (i * 0.68f), -8.5f, -0.001f), Quaternion.identity);
            //  _temp.GetComponent<LapisButtonInteract>().enabled = false;
            _temp.name = "lapis" + i;
        }

        randomedLapisID = Random.Range(0, 13);

        GameObject _tempRandomedLapis = GameObject.Find("lapis" + randomedLapisID);
        //  Destroy(_tempRandomedLapis.gameObject);
        _tempRandomedLapis.transform.position = new Vector3(_tempRandomedLapis.transform.position.x, -5.75f, -0.003f);

        //USAR ESTE  GameObject _tempButton = Instantiate(buttonPrefab, new Vector3(-88f + (randomedLapisID * 60f), 15f, -0.001f), Quaternion.identity);
        //  GameObject _tempButton = Instantiate(buttonPrefab, new Vector3(180.5f + (-184.9f + (randomedLapisID * 0.682225f)), 15f, -0.001f), Quaternion.identity);
        //  _tempButton.GetComponent<LapisButtonInteract>().enabled = true;
        //  _tempButton.GetComponent<Button>().enabled = true;
        //  _tempButton.transform.parent = canvasToParent.transform;
        //  _tempButton.name = "lapisButton" + randomedLapisID;


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
