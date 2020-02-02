using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
//  using UnityEngine.SceneManagement;
using UnityEngine;

public class LapisButtonInteract : MonoBehaviour
{
    float unitsToTranslate = 2.75f;
    float translateSpeed = 0.125f;
    bool shouldTranslate = false;

    //public Scene menu;
    //public Scene puzzle01;
    //public Scene puzzle02;
    //public Scene credits;

    // Start is called before the first frame update
    void Start()
    {
        //  SceneManager.LoadScene(menu.name, LoadSceneMode.Single);
        //  unitsToTranslate = 3f;
        //  translateSpeed = 0.01f;
        //  shouldTranslate = false;
    }

    // Update is called once per frame
    void Update()
    {

        if ((shouldTranslate) && (unitsToTranslate > 0f))
        {
            gameObject.transform.position += new Vector3(0f, -translateSpeed, -0.001f);
            unitsToTranslate -= translateSpeed;
        }
        else if (unitsToTranslate <= 0f)
        {
            // AVISA O EMPTY QUE TRANSLADOU TUDO!
            Debug.Log("AVISA O EMPTY QUE TRANSLADOU TUDO!");
        }
    }

    public void Clicked()
    {
        gameObject.GetComponent<AudioSource>().Play();
        shouldTranslate = true;
        //gameObject.GetComponent<BoxCollider>().enabled = false;
    }
}
