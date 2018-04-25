using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundry : MonoBehaviour
{
    void Start()
    {
        Physics.IgnoreLayerCollision(9, 8);
        Physics.IgnoreLayerCollision(9, 2);
        Physics.IgnoreLayerCollision(9, 10, false);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            GetComponent<MeshRenderer>().enabled = true;
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            StartCoroutine(DisableRenderer());
        }
    }
    
    IEnumerator DisableRenderer()
    {
        yield return new WaitForSeconds(2);
        GetComponent<MeshRenderer>().enabled = false;
    }
}
