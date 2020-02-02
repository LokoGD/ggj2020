using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapisPuzzleFinish : MonoBehaviour
{
    bool doFinish;
    public float unidadesQueRecolhe;
    //  float transformBase;

    // Start is called before the first frame update
    void Start()
    {
        unidadesQueRecolhe = 3;
        doFinish = true;
        //  transformBase = gameObject.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (doFinish)
        {
            if (unidadesQueRecolhe > 0)
            {
                //  gameObject.transform.position.y -= 0.1;
            }
            else
            {
                doFinish = false;
            }
        }
    }

    public void MakeItFinish(bool param)
    {
        doFinish = param;
    }
}
