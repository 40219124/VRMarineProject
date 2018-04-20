using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentResponse : MonoBehaviour {
    
    private int currentCount = 0;
    private List<CurrentForce> currentForces = new List<CurrentForce>();
    private Vector3 floatVal = new Vector3(0.0f, 0.2f, 0.0f);
    private ConstantForce cf;

	// Use this for initialization
	void Start () {
        cf = gameObject.GetComponent<ConstantForce>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 newForce = floatVal;
        if (currentCount > 0)
        {
            foreach(CurrentForce c in currentForces)
            {
                newForce += c.Force;
            }
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
