using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleForce : MonoBehaviour {

    ConstantForce force;
    static PulseControl pulse;

	// Use this for initialization
	void Start () {
        force = GetComponent<ConstantForce>();
        if (pulse == null)
        {
            pulse = FindObjectOfType<PulseControl>();
        }
	}
	
	// Update is called once per frame
	void Update () {
        force.force = pulse.WaveForce;
	}
}
