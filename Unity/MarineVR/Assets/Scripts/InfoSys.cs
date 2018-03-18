using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class InfoSys : MonoBehaviour {
    string information = "I am a fish";
    private SteamVR_TrackedController[] controller = new SteamVR_TrackedController[2];
    private SteamVR_TrackedObject[] trackedObj = new SteamVR_TrackedObject[2];
    // device 0 = left, device 1 = right
    private SteamVR_Controller.Device[] device = new SteamVR_Controller.Device[2];

    // Use this for initialization
    void Start () {
        //controller[0] = GetComponent<SteamVR_ControllerManager>().left.GetComponent<SteamVR_TrackedController>();
       // controller[1] = GetComponent<SteamVR_ControllerManager>().right.GetComponent<SteamVR_TrackedController>();
        //trackedObj[0] = controller[0].GetComponent<SteamVR_TrackedObject>();
        //trackedObj[1] = controller[1].GetComponent<SteamVR_TrackedObject>();
    }

    //Should update if colliding with anything
    private void OnCollisionTrigger(Collider collision)
    {

        Debug.Log("collision detected");
        if (collision.transform.gameObject.tag == "fish")
        {
            GUI.Box(new Rect(0, 0, 100, 100), information);
            
            
        }
        
        //if (collision.transform.gameObject == controller[0] && device[0].GetPressDown(EVRButtonId.k_EButton_A))
        //{
        //    GUI.Box(new Rect(0, 0, 100, 100), information);
        //}
    }


    // Update is called once per frame
    void Update () {
        //device[0] = SteamVR_Controller.Input((int)trackedObj[0].index);
        //device[1] = SteamVR_Controller.Input((int)trackedObj[1].index);

    }
}
