using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonVRMovement : MonoBehaviour {

    private float speedX = 2.0f;
    private float speedY = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;
	
	// Update is called once per frame
	void Update () {
        Vector3 mov = new Vector3(0.0f, 0.0f, 0.0f);
        if (Input.GetKey(KeyCode.W))
        {
            mov += Vector3.forward * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            mov += Vector3.left * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            mov += Vector3.back * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            mov += Vector3.right * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            mov += Vector3.down * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            mov += Vector3.up * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            mov *= 3.0f;
        }

        transform.Translate(mov);

        yaw += speedX * Input.GetAxis("Mouse X");
        pitch -= speedY * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }
}
