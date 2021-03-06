﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseControl : MonoBehaviour {

    private float pulse = 0.0f;
    [SerializeField]
    private float period = 3.0f;
	
    public float Pulse { get { return pulse; } }
    public float Period { get { return period; } }

    public Vector3 WaveForce;

    // Update is called once per frame
    void Update () {
        // calculate the sin value relating to a period of seconds
        pulse = Mathf.Sin(Time.time * Mathf.PI * 2.0f / period);
        WaveForce = new Vector3(1.0f, 0.0f, 0.0f) * pulse * 0.5f;
	}
}
