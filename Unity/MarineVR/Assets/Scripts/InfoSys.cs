using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class InfoSys : MonoBehaviour
{
    bool collided;
    bool vr = true;


    // Use this for initialization
    void Start()
    {
    }

    //Should update if colliding with anything
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("collision detected");
        if (collision.tag == "projectile")
        {

            collided = true;

        }
    }

    private void OnTriggerExit(Collider collision)
    {
        collided = false;
    }

    private void OnGUI()
    {
        if (collided)
            GUI.Box(new Rect(0, 0, 100, 100), Data.dictionaryData[gameObject.tag]);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
