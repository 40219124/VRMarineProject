using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class infoSysPlant : MonoBehaviour
{
    string informationPlant = "I am a plant";
    private SteamVR_TrackedController[] controller = new SteamVR_TrackedController[2];
    private SteamVR_TrackedObject[] trackedObj = new SteamVR_TrackedObject[2];
    // device 0 = left, device 1 = right
    private SteamVR_Controller.Device[] device = new SteamVR_Controller.Device[2];
    bool collidedPlant;

    // Use this for initialization
    void Start()
    {
        //controller[0] = GetComponent<SteamVR_ControllerManager>().left.GetComponent<SteamVR_TrackedController>();
        // controller[1] = GetComponent<SteamVR_ControllerManager>().right.GetComponent<SteamVR_TrackedController>();
        //trackedObj[0] = controller[0].GetComponent<SteamVR_TrackedObject>();
        //trackedObj[1] = controller[1].GetComponent<SteamVR_TrackedObject>();
    }

    //Should update if colliding with anything
    private void OnTriggerEnter(Collider collision)
    {

        Debug.Log("collision detected");
        if (collision.transform.gameObject.tag == "fish")
        {

            collidedPlant = true;

        }
        //if (collision.transform.gameObject == controller[0] && device[0].GetPressDown(EVRButtonId.k_EButton_A))
        //{
        //    GUI.Box(new Rect(0, 0, 100, 100), information);
        //}
    }

    private void OnTriggerExit(Collider collision)
    {
        collidedPlant = false;
    }

    private void OnGUI()
    {
        if (collidedPlant)
            GUI.Box(new Rect(0, 0, 100, 100), informationPlant);
    }

    // Update is called once per frame
    void Update()
    {
        //device[0] = SteamVR_Controller.Input((int)trackedObj[0].index);
        //device[1] = SteamVR_Controller.Input((int)trackedObj[1].index);

    }
}
