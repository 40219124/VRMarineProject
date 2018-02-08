using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PlayerBodyMovement : MonoBehaviour
{

    private SteamVR_TrackedController[] controller = new SteamVR_TrackedController[2];
    private SteamVR_TrackedObject trackedObj;
    // device 0 = left, device 1 = right
    private SteamVR_Controller.Device[] device = new SteamVR_Controller.Device[2];

    // Use this for initialization
    void Start()
    {
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Time.time == Time.deltaTime)
        {
            controller[0] = GetComponent<SteamVR_ControllerManager>().left.GetComponent<SteamVR_TrackedController>();
            controller[1] = GetComponent<SteamVR_ControllerManager>().right.GetComponent<SteamVR_TrackedController>();
            trackedObj = controller[0].GetComponent<SteamVR_TrackedObject>();
        }
        device[0] = SteamVR_Controller.Input((int)trackedObj.index);
        device[1] = SteamVR_Controller.Input((int)controller[1].GetComponent<SteamVR_TrackedObject>().index);
        if (device[0].GetPress(EVRButtonId.k_EButton_SteamVR_Touchpad))
        {
            Vector2 touchpad = (device[0].GetAxis(EVRButtonId.k_EButton_Axis0));
            float sqLen = touchpad.sqrMagnitude;
            if (sqLen > 0.25f)
            {
                touchpad = touchpad.normalized;
            }
            else if(sqLen > 0.0f)
            {
                touchpad = touchpad.normalized / 2.0f;
            }
            transform.Translate(new Vector3(touchpad.x, 0.0f, touchpad.y) * Time.deltaTime);
        }
        if (device[0].GetPress(EVRButtonId.k_EButton_SteamVR_Trigger))
        {
            transform.Translate(Vector3.down * 0.8f * Time.deltaTime);
        }
        if (device[1].GetPress(EVRButtonId.k_EButton_SteamVR_Trigger))
        {
            transform.Translate(Vector3.up * 0.8f * Time.deltaTime);
        }
    }
}
