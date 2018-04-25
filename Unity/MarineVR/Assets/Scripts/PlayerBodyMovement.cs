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
    private bool turnBody = true;
    private Vector3 spawn = new Vector3(-29.0f, -3.0f, -27.0f);

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
        if (transform.position.y < -18.0f)
        {
            transform.position = spawn;
        }
        device[0] = SteamVR_Controller.Input((int)trackedObj[0].index);
        device[1] = SteamVR_Controller.Input((int)trackedObj[1].index);

        // Toggle move-camera lock on left menu button
        if (device[0].GetPressDown(EVRButtonId.k_EButton_Grip))
        {
            if (turnBody)
            {
                turnBody = false;
                clock = 0.0f;
            }
            else
            {
                turnBody = true;
            }
        }
        // Turn the body to follow view
        if (turnBody)
        {
            bearing = camTrans.eulerAngles.y;
            clock += Time.deltaTime;
        }
        // If held down rather than toggled
        if (device[0].GetPressUp(EVRButtonId.k_EButton_Grip) && clock >= holdTime)
        {
            turnBody = false;
            clock = 0.0f;
        }

        // Movement on left trackpad
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

        /// old up/down method on left trigger/grip
        //if (device[0].GetPress(EVRButtonId.k_EButton_SteamVR_Trigger))
        //{
        //    transform.Translate(Vector3.up * ySpeed * Time.deltaTime);
        //}
        //if (device[0].GetPress(EVRButtonId.k_EButton_Grip))
        //{
        //    transform.Translate(Vector3.down * ySpeed * Time.deltaTime);
        //}

        // If left trigger go up/down
        if (device[0].GetPress(EVRButtonId.k_EButton_SteamVR_Trigger))
        {
            float zRot = controller[0].GetComponent<Transform>().eulerAngles.z;
            // if hand prone go down
            if (zRot > deadZone && zRot < 180.0f - deadZone)
            {
                transform.Translate(Vector3.up * ySpeed * Time.deltaTime);
            }
            // if hand supine go up
            else if (zRot > 180 + deadZone && zRot < 360.0f - deadZone)
            {
                transform.Translate(Vector3.down * ySpeed * Time.deltaTime);
            }
        }

           
        GetComponent<CapsuleCollider>().center = camTrans.localPosition;

    }
}
