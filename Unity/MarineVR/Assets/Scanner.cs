using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float reach;
    Transform parent;
    bool hasParent = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!hasParent)
        {
            parent = GameObject.Find("Controller (right)").GetComponent<Transform>();
            if (parent)
            {
                hasParent = true;
            }
        }
        else
        {
            gameObject.transform.position = parent.transform.position + parent.transform.rotation * (Vector3.forward * reach);
        }
    }
} // /Model/tip/attach
