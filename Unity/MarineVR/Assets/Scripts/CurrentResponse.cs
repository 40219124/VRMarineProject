using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentResponse : MonoBehaviour {
    
    private int currentCount = 0;
    private List<CurrentForce> currentForces = new List<CurrentForce>();
    private Vector3 floatVal = new Vector3(0.0f, 0.3f, 0.0f);
    private ConstantForce cf;
    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        cf = gameObject.GetComponent<ConstantForce>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 newForce = floatVal;
        if (currentCount > 0)
        {
            rb.drag = 0.2f;
            foreach(CurrentForce c in currentForces)
            {
                newForce += c.Force;
            }
        }
        else
        {
            rb.drag = 1.0f;
        }
        cf.force = newForce;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Current")
        {
            currentCount++;
            currentForces.Add(other.GetComponent<CurrentForce>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Current")
        {
            currentCount--;
            currentForces.Remove(other.GetComponent<CurrentForce>());
        }
    }
}
