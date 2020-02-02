using HalliHacks.Pulse;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PulseDemo : MonoBehaviour {

    public GameObject Room;
    public Camera Cam;

    public Rigidbody[] Explodable;
    public Transform ExplodePoint;

    [Range(0, 100)]
    public float ExplodeForce = 1f;

    [Range(0, 10)]
    public float ExplodeRadius = 1f;

    public Light MainLight;
    public Light Spotlight;
    public GameObject DiscoBall;


	// Use this for initialization
	void Start () {

        bool exploding = true;

        //Camera rotation - uses 0 as interval for smooth movement
        Pulse.Every(0).Do(() =>
        {
            Cam.transform.RotateAround(Room.transform.position, Vector3.up, 1f);
        });

        Pulse.Every(0).Do(() =>
        {
            Spotlight.transform.RotateAround(Room.transform.position, Vector3.up, -1f);
            DiscoBall.transform.RotateAround(Room.transform.position, Vector3.up, -0.5f);
        }).Until(() =>
        {
            return !exploding;
        }).Then(() =>
        {
            Spotlight.gameObject.SetActive(false);
            DiscoBall.SetActive(false);
            MainLight.gameObject.SetActive(true);
        });

        Pulse.Every(1).Do(() =>
        {
            if (DiscoBall.activeSelf)
            {
                DiscoBall.SetActive(false);
            }
            else
            {
                DiscoBall.SetActive(true);
            }
        }).For(20).Until(() =>
        {
            return !exploding;
        }).Then(() =>
        {
            DiscoBall.SetActive(false);
        });

        //Apply an explosive force every second
        Pulse.Every(1).Do(() =>
        {
            foreach (var rb in Explodable)
            {
                rb.AddExplosionForce(ExplodeForce, ExplodePoint.position, ExplodeRadius, 2f, ForceMode.Impulse);
            }
        }).For(15).Then(() =>
        {
            exploding = false;
        });

        //Move the explosive force every second
        Pulse.Every(1).Do(() =>
        {
            ExplodePoint.RotateAround(Room.transform.position, Vector3.up, 45f);
        });
    }
	
}
