using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scanner : MonoBehaviour
{
    public float reach;
    Transform parent;
    bool hasParent = false;
    [SerializeField]
    Text canvas;

    // Use this for initialization
    void Start()
    {
        canvas = GameObject.Find("infoCanvas").GetComponent<Text>();
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

    //Should update if colliding with anything
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("collision detected");
        canvas.text = Data.dictionaryData[collision.tag];
    }

    private void OnTriggerExit(Collider collision)
    {
        canvas.text = "";
    }
} // /Model/tip/attach
