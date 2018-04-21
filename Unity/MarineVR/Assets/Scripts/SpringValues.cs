using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringValues : MonoBehaviour
{
    public static float strength;
    public static float damper;
    public static int updateValues = 0;
    private int myUpdates = 0;

    public float Strength { set { strength = value; } }
    public float Damper { set { damper = value; } }
    public void UpdateValues() { updateValues += 1; }

    private SpringJoint spring;

    // Use this for initialization
    void Start()
    {
        spring = gameObject.GetComponent<SpringJoint>();
        spring.spring = strength;
        spring.damper = damper;
    }

    // Update is called once per frame
    void Update()
    {
        if (myUpdates < updateValues)
        {
            spring.spring = strength;
            spring.damper = damper;
            myUpdates = updateValues;
        }
    }
}
