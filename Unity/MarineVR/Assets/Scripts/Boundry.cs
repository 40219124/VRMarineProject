using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundry : MonoBehaviour
{
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
