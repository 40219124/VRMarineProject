using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PlayerBodyMovement : MonoBehaviour
{

    private SteamVR_TrackedController[] controller = new SteamVR_TrackedController[2];
    private SteamVR_TrackedObject[] trackedObj = new SteamVR_TrackedObject[2];
    // device 0 = left, device 1 = right
    private SteamVR_Controller.Device[] device = new SteamVR_Controller.Device[2];
    private Transform camTrans;
    private float bearing = 0.0f;
    private float deadZone = 7.0f;
    private float ySpeed = 0.8f;
    private float holdTime = 0.5f;
    private float clock = 0.0f;
    private bool turnBody = false;

    // Use this for initialization
    void Start()
    {
        Transform[] transs = GetComponentsInChildren<Transform>();
        foreach (Transform t in transs)
        {
            if (t.name.Contains("eye"))
            {
                camTrans = t;
                break;
            }
        }
        controller[0] = GetComponent<SteamVR_ControllerManager>().left.GetComponent<SteamVR_TrackedController>();
        controller[1] = GetComponent<SteamVR_ControllerManager>().right.GetComponent<SteamVR_TrackedController>();
        trackedObj[0] = controller[0].GetComponent<SteamVR_TrackedObject>();
        trackedObj[1] = controller[1].GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update()
    {
        device[0] = SteamVR_Controller.Input((int)trackedObj[0].index);
        device[1] = SteamVR_Controller.Input((int)trackedObj[1].index);
        
        if (device[1].GetPressDown(EVRButtonId.k_EButton_SteamVR_Touchpad) && !turnBody)
        {
            turnBody = true;
        }
        else if (device[1].GetPressDown(EVRButtonId.k_EButton_SteamVR_Touchpad) && turnBody)
        {
            turnBody = false;
            clock = 0.0f;
        }
        if (turnBody) { 
            bearing = camTrans.eulerAngles.y;
            clock += Time.deltaTime;
        }
        if (device[1].GetPressUp(EVRButtonId.k_EButton_SteamVR_Touchpad) && clock >= holdTime)
        {
            turnBody = false;
            clock = 0.0f;
        }
        

        if (device[0].GetPress(EVRButtonId.k_EButton_SteamVR_Touchpad))
        {
            Vector2 touchpad = (device[0].GetAxis(EVRButtonId.k_EButton_Axis0));
            float sqLen = touchpad.sqrMagnitude;
            if (sqLen > 0.25f)
            {
                touchpad = touchpad.normalized;
            }
            else if (sqLen > 0.0f)
            {
                touchpad = touchpad.normalized / 2.0f;
            }
            transform.Translate(Quaternion.Euler(0.0f, bearing, 0.0f) * new Vector3(touchpad.x, 0.0f, touchpad.y) * Time.deltaTime);
        }

        if (device[0].GetPress(EVRButtonId.k_EButton_SteamVR_Trigger))
        {
            transform.Translate(Vector3.up * ySpeed * Time.deltaTime);
        }
        if (device[0].GetPress(EVRButtonId.k_EButton_Grip))
        {
            transform.Translate(Vector3.down * ySpeed * Time.deltaTime);
        }
        if (device[1].GetPress(EVRButtonId.k_EButton_SteamVR_Trigger))
        {
            float zRot = controller[1].GetComponent<Transform>().eulerAngles.z;
            if (zRot > deadZone && zRot < 180.0f - deadZone)
            {
                transform.Translate(Vector3.down * ySpeed * Time.deltaTime);
            }
            else if (zRot > 180 + deadZone && zRot < 360.0f - deadZone)
            {
                transform.Translate(Vector3.up * ySpeed * Time.deltaTime);
            }
        }
    }
}
