using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Procedural : MonoBehaviour
{
    public float size = 10;
    [SerializeField]
    private List<GameObject> coral = new List<GameObject>();
    [SerializeField]
    private int objects = 100;
    [SerializeField]
    private int max_height = 0;
    private bool visited;
    private List<GameObject> corals = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        //call generate to generate coral on load
        Create();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Create()
    {
        if (!visited)
        {
            Generate();
            visited = true;
        }
        else
        {
            foreach (GameObject c in corals)
            {
                c.SetActive(true);
            }
        }
    }

    public void Remove()
    {
        foreach (GameObject c in corals)
        {
            c.SetActive(false);
        }
    }

    //method to generate sea life
    private void Generate()
    {
        //choose random point
        Vector3 pos = transform.localPosition;
        pos.y = max_height;
        //loop for every object
        for (int i = 0; i < objects; i++)
        {
            pos.x = transform.localPosition.x + Random.Range(-size / 2.0f, size / 2.0f);
            pos.z = transform.localPosition.z + Random.Range(-size / 2.0f, size / 2.0f);
            //variables for raycasting
            Ray down = new Ray(pos, Vector3.down);
            RaycastHit hit;

            //check if above ground
            if (Physics.Raycast(down, out hit))
            {
                //if hits ground
                if (hit.collider.tag == "Ground")
                {
                    int j = Random.Range(0, coral.Count);
                    Vector3 axis = new Vector3();

                    if (coral[j].GetComponent<Coral>().changeAngle)
                    {
                        hit.point += (coral[j].GetComponent<Coral>().offset * coral[j].transform.localScale.y) * hit.normal;
                        //find angle of rotation
                        float angle = Mathf.Acos(Vector3.Dot(Vector3.up, hit.normal));
                        //convert to degrees
                        angle = (angle * 180) / Mathf.PI;
                        //find axis of rotation
                        axis = Vector3.Normalize(Vector3.Cross(Vector3.up, hit.normal));
                        //multiply by angle
                        axis *= angle;
                    }
                    //create coral
                    GameObject c = Instantiate(coral[j], hit.point, Quaternion.Euler(axis));
                    c.transform.Rotate(new Vector3(0.0f, Random.Range(-180, 180), 0.0f));
                    c.transform.parent = this.GetComponent<Transform>();
                    corals.Add(c);
                }
            }

        }
    }

    void OnDrawGizmosSelected()
    {
        //draw cube showing affected area
        Gizmos.color = new Color(0.5f, 0.0f, 0.5f, 0.2f);
        Gizmos.DrawCube(transform.localPosition, new Vector3(size, 2, size));
    }
}
