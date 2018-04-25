using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorFixer : MonoBehaviour {

    SpringJoint spr;
    Rigidbody other;
    Transform myT;
    [SerializeField]
    Transform middleMan;

	// Use this for initialization
	void Start () {
        myT = transform;
        spr = GetComponent<SpringJoint>();
        other = spr.connectedBody;
        Vector3 newA = other.transform.localPosition + middleMan.localPosition;
        spr.connectedAnchor = -newA;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
