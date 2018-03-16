using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonVRMovement : MonoBehaviour {

	private float movSpeed = 5.0f;

    private float speedX = 2.0f;
    private float speedY = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;
	
	// Update is called once per frame
	void Update () {
        Vector3 mov = new Vector3(0.0f, 0.0f, 0.0f);
        if (Input.GetKey(KeyCode.W))
        {
            mov += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            mov += Vector3.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            mov += Vector3.back;
        }
        if (Input.GetKey(KeyCode.D))
        {
            mov += Vector3.right;
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            mov += Vector3.down;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            mov += Vector3.up;
        }
		mov = mov.normalized * Time.deltaTime;
		if (Input.GetKey (KeyCode.LeftShift)) {
			mov *= movSpeed * 0.5f;
		} else {
			mov *= movSpeed;
		}
        transform.Translate(mov);

        yaw += speedX * Input.GetAxis("Mouse X");
        pitch -= speedY * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }
}
