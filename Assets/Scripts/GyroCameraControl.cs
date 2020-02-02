using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GyroCameraControl : MonoBehaviour
{
    public bool activateGyroCameraControl;
    public float gyroCameraControlInfluence;

    // Start is called before the first frame update
    void Start()
    {
        activateGyroCameraControl = true;
        gyroCameraControlInfluence = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(-(Input.gyro.rotationRateUnbiased.x * gyroCameraControlInfluence), -(Input.gyro.rotationRateUnbiased.y * gyroCameraControlInfluence), -(Input.gyro.rotationRateUnbiased.z * gyroCameraControlInfluence));
    }
}
