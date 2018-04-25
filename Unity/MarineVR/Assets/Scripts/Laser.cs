//======= Copyright (c) Valve Corporation, All rights reserved. ===============
using UnityEngine;
using System.Collections;
using Valve.VR;
using UnityEngine.UI;



public class Laser : MonoBehaviour
{
    public bool active = true;
    public Color color;
    public float thickness = 0.002f;
    public GameObject holder;
    public GameObject pointer;
    bool isActive = false;
    public bool addRigidBody = false;
    public Transform reference;
    public event PointerEventHandler PointerIn;
    public event PointerEventHandler PointerOut;
    RaycastHit collided;
    private SteamVR_TrackedController controller;
    private SteamVR_TrackedObject trackedObj;
    // device 0 = left, device 1 = right
    private SteamVR_Controller.Device device;
    bool dispLaser = false;
    bool dispText = false;
    [SerializeField]
    float dist = 0.5f;
    
    Transform previousContact = null;
    Text canvas;
    


    // Use this for initialization
    void Start()
    {
        canvas = GameObject.Find("infoCanvas").GetComponent<Text>();
        if (canvas != null)

        holder = new GameObject();
        holder.transform.parent = this.transform;
        holder.transform.localPosition = Vector3.zero;
        holder.transform.localRotation = Quaternion.identity;

        pointer = GameObject.CreatePrimitive(PrimitiveType.Cube);
        pointer.transform.parent = holder.transform;
        pointer.transform.localScale = new Vector3(thickness, thickness, 100f);
        pointer.transform.localPosition = new Vector3(0f, 0f, 50f);
        pointer.transform.localRotation = Quaternion.identity;
        BoxCollider collider = pointer.GetComponent<BoxCollider>();
        if (addRigidBody)
        {
            if (collider)
            {
                collider.isTrigger = true;
            }
            Rigidbody rigidBody = pointer.AddComponent<Rigidbody>();
            rigidBody.isKinematic = true;
        }
        else
        {
            if (collider)
            {
                Object.Destroy(collider);
            }
        }
        Material newMaterial = new Material(Shader.Find("Unlit/Color"));
        newMaterial.SetColor("_Color", color);
        pointer.GetComponent<MeshRenderer>().material = newMaterial;


        controller = GetComponent<SteamVR_TrackedController>();
        trackedObj = controller.GetComponent<SteamVR_TrackedObject>();
    }

    public virtual void OnPointerIn(PointerEventArgs e)
    {
        if (PointerIn != null)
            PointerIn(this, e);
    }

    public virtual void OnPointerOut(PointerEventArgs e)
    {
        if (PointerOut != null)
            PointerOut(this, e);
    }


    // Update is called once per frame
    void Update()
    {
        if (!isActive)
        {
            isActive = true;
            this.transform.GetChild(0).gameObject.SetActive(true);
        }


        device = SteamVR_Controller.Input((int)trackedObj.index);
    

        Ray raycast = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        bool bHit = Physics.Raycast(raycast, out hit);

        if (previousContact && previousContact != hit.transform)
        {
            PointerEventArgs args = new PointerEventArgs();
            if (controller != null)
            {
                args.controllerIndex = controller.controllerIndex;
            }
            args.distance = 0f;
            args.flags = 0;
            args.target = previousContact;
            OnPointerOut(args);
            previousContact = null;
        }
        if (bHit && previousContact != hit.transform)
        {
            PointerEventArgs argsIn = new PointerEventArgs();
            if (controller != null)
            {
                argsIn.controllerIndex = controller.controllerIndex;
            }
            argsIn.distance = hit.distance;
            argsIn.flags = 0;
            argsIn.target = hit.transform;
            OnPointerIn(argsIn);
            previousContact = hit.transform;
            
        }
        if (!bHit)
        {
            previousContact = null;
           
        }
        if (bHit && hit.distance <= dist)
        {
            if (dispLaser)
                
                if (!dispText)
                {
                    if (controller != null && device.GetPressDown(EVRButtonId.k_EButton_ApplicationMenu))
                    {
                        if (Data.dictionaryData.ContainsKey(collided.collider.tag))
                        {
                            canvas.transform.localPosition = new Vector3(-0.2f, 0.1f, 0.1f);
                            canvas.text = Data.dictionaryData[collided.collider.tag];
                            dispText = true;
                        }
                    }
                }
                else
                {

                    if (controller != null && device.GetPressDown(EVRButtonId.k_EButton_ApplicationMenu))
                        dispText = false;
                }
            collided = hit;
        }
        else
        {
            if (controller != null && device.GetPressDown(EVRButtonId.k_EButton_ApplicationMenu))
            {
                dispText = false;
                
            }
        }

        if (controller != null && device.GetPressDown(EVRButtonId.k_EButton_SteamVR_Trigger))
        {
            dispLaser = !dispLaser;
            
        }
        if (dispLaser)
            dist = 0.5f;

        if (dispText)
        {
            if (device.GetPress(EVRButtonId.k_EButton_SteamVR_Touchpad))
            {
                Vector2 touchpad = (device.GetAxis(EVRButtonId.k_EButton_Axis0));
                if (touchpad.y < 0)
                {
                    canvas.transform.localPosition += new Vector3(0.0f, 0.01f, 0.01f);
                }
                else if (touchpad.y > 0)
                {
                    canvas.transform.localPosition -= new Vector3(0.0f, 0.01f, 0.01f);
                }
            }
        }
        if (bHit && hit.distance < dist)
        {
            dist = hit.distance;
        }
        if (dispText)
        {
            
            canvas.enabled = true;
        }
        else
            canvas.enabled = false;


        if (!dispLaser)
            dist = 0.0f;
        if (controller == null)
            Debug.Log("controller null");
        pointer.transform.localScale = new Vector3(thickness, thickness, dist);
        pointer.transform.localPosition = new Vector3(0f, 0f, dist / 2f);
    }
}
