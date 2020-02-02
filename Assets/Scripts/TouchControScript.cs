using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TouchControScript : MonoBehaviour
{
    public Text _tempText;

    // Start is called before the first frame update
    void Start()
    {
        //  _tempText.text = "Teste!";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit _objHitInfo;

            if (Physics.Raycast(ray, out _objHitInfo))
            {
                _tempText.text = "HitName: " + _objHitInfo.transform.name;
            }
        }

        if (Input.touchCount > 0)
        {
            Touch myTouch = Input.GetTouch(0);
            Vector3 touch_pos = Camera.main.ScreenToWorldPoint(myTouch.position);
            // Debug.Log("ToucPos: " + touch_pos);
        }

        //  _tempText.text = "Resolution: " + Screen.currentResolution.height;
    }
}
