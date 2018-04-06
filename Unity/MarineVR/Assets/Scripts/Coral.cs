using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coral : MonoBehaviour
{

    public float offset = 0.0f;
    public float mindepth = 0.0f;
    public float maxdepth = 100.0f;
    public bool changeAngle;
    public float steepest = 90.0f;
    public float shallowest = 0.0f;
    public float rarity = 20;
    public bool overlap = false;
    public List<bool> overlaps = new List<bool>();
    public int groupMin = 0;
    public int groupMax = 0;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = new Vector3(0, 0, 0);
        if (overlaps.Count > 0)
        {
            pos.x = gameObject.transform.position.x + Random.Range(-1.0f, 1.0f);
            pos.z = gameObject.transform.position.z + Random.Range(-1.0f, 1.0f);
            //variables for raycasting
            Ray down = new Ray(pos, Vector3.down);
            RaycastHit hit;

            //check if above ground
            if (Physics.Raycast(down, out hit))
            {
                //if hits ground
                if (hit.collider.tag == "Ground")
                {
                    //find angle of rotation
                    float angle = Mathf.Acos(Vector3.Dot(Vector3.up, hit.normal));
                    angle = (angle * 180) / Mathf.PI;
                    Vector3 axis = new Vector3();
                    //convert to degrees
                    if (changeAngle)
                    {

                        //find axis of rotation
                        axis = Vector3.Normalize(Vector3.Cross(Vector3.up, hit.normal));
                        //multiply by angle
                        axis *= angle;
                    }

                    pos.y = hit.point.y;
                    pos += (offset * gameObject.transform.localScale.y) * hit.normal;

                    gameObject.GetComponent<Transform>().SetPositionAndRotation(pos, Quaternion.Euler(axis));
                }
            }
        }
    }
}
