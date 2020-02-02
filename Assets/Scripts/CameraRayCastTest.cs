using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class CameraRayCastTest : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject _tempLapisManager;
    public GameObject _tempLapisSorteado;
    int _tempLapisSorteadoID;

    void Start()
    {
        //  GameObject _tempRandomedLapis = GameObject.Find("lapis" + randomedLapisID);
        _tempLapisSorteadoID = _tempLapisManager.GetComponent<LapisManagerScript>().randomedLapisID;
        _tempLapisSorteado = GameObject.Find("lapis" + _tempLapisSorteadoID);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 1000f))
            {
                if (hit.transform != null)
                {
                    //  print(hit.transform.name);
                    _tempLapisSorteado.GetComponent<LapisButtonInteract>().Clicked();
                }
            }
        }
    }
}
