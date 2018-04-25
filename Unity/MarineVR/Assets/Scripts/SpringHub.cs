using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringHub : MonoBehaviour {

    public float strength = 50;
    public float damper = 50;
    private SpringValues aSpring;

	// Use this for initialization
	void Start () {
        aSpring = FindObjectOfType<SpringValues>();
        if (aSpring != null)
        {
            OnValidate();
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (aSpring == null)
        {
            aSpring = FindObjectOfType<SpringValues>();
            if (aSpring != null)
            {
                OnValidate();
            }
        }
	}

    private void OnValidate()
    {
        if (aSpring != null)
        {
            aSpring.Strength = strength;
            aSpring.Damper = damper;
            aSpring.UpdateValues();
        }
    }
}
