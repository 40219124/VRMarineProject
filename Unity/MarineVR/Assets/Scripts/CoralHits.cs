using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoralHits : MonoBehaviour {

    Coral ohCoralMyCoral;

	// Use this for initialization
	void Start () {
        ohCoralMyCoral = GetComponentInParent<Coral>();
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Ground")
        {
            ohCoralMyCoral.overlaps.Add(true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag != "Ground")
        {
            ohCoralMyCoral.overlaps.RemoveAt(0);
        }
    }
}
