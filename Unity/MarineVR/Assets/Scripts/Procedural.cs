using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Procedural : MonoBehaviour {
    [SerializeField]
    private float width = 10;
    [SerializeField]
    private float length = 10;
    [SerializeField]
    private GameObject coral;
    [SerializeField]
    private int objects = 100;
    [SerializeField]
    private int max_height = 0;
    //I SHOULD TRY AND MAKE THIS LESS HARD CODED
    [SerializeField]
    private float offset = 0.5f;

	// Use this for initialization
	void Start () {
        //call generate to generate coral on load
        Generate();
	}
	
	// Update is called once per frame
	void Update () {
        //if P is pressed generate more *****FOR DEBUG******
		if (Input.GetKey(KeyCode.P))
        {
            Generate();
        }
	}

    //method to generate sea life
    private void Generate()
    {
        //loop for every object
        for (int i = 0; i < objects; i++)
        {
            //choose random point
            Vector3 pos = transform.localPosition + new Vector3(Random.Range(-width / 2, width / 2), max_height, Random.Range(-length / 2, length / 2));

            //variables for raycasting
            Ray down = new Ray(pos, Vector3.down);
            RaycastHit hit;

            //check if above ground
            if (Physics.Raycast(down, out hit))
            {
                //if hits ground
                if (hit.collider.tag == "Ground")
                {
                    //set y position to hit point
                    pos.y = hit.point.y + offset;
                    Vector3 up = new Vector3(0.0f, 1.0f, 0.0f);
                    //find angle of rotation
                    float angle = Mathf.Acos(Vector3.Dot(up, hit.normal));
                    //convert to degrees
                    angle = (angle * 180) / Mathf.PI;
                    //find axis of rotation
                    Vector3 axis = Vector3.Normalize(Vector3.Cross(up, hit.normal));
                    //multiply by angle
                    axis *= angle;
                    //create coral
                    Instantiate(coral, pos, Quaternion.Euler(axis));
                }
            }

        }
    }

    void OnDrawGizmosSelected()
    {
        //draw cube showing affected area
        Gizmos.color = new Color(0.5f, 0.0f, 0.5f, 0.2f);
        Gizmos.DrawCube(transform.localPosition, new Vector3(width, 2, length));
    }
}
