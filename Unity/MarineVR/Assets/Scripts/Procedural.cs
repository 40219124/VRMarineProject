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
    private List<int> total = new List<int>();

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < coral.Count; i++)
        {
            total.Add(0);
        }
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
            gameObject.SetActive(true);
        }
    }

    public void Remove()
    {
        gameObject.SetActive(false);
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
                    if (coral.Count == 0)
                    {
                        return;
                    }

                    int j = Random.Range(0, coral.Count);
                    Coral theCoral = coral[j].GetComponent<Coral>();
                    //find angle of rotation
                    float angle = Mathf.Acos(Vector3.Dot(Vector3.up, hit.normal));
                    //convert to degrees
                    angle = (angle * 180) / Mathf.PI;


                    for (int tries = 2; tries > 0; tries--)
                    {

                        if (!Test(theCoral, hit, angle))
                        {
                            j = Random.Range(0, coral.Count);
                            theCoral = coral[j].GetComponent<Coral>();
                        }
                        else
                        {
                            break;
                        }
                    }

                    GameObject c = CreateObject(theCoral, hit, angle, j);

                    if (c != null)
                    {
                        int amount = 0;
                        if (theCoral.groupMax > 0)
                        {
                            amount = Cluster(c, j);
                            i += amount;
                        }
                        total[j] += (1 + amount);
                        if (total[j] >= (theCoral.rarity * (float)(objects / 100)))
                        {
                            coral.RemoveAt(j);
                            total.RemoveAt(j);
                        }
                    }

                }
            }

        }
    }

    int Cluster(GameObject c, int j)
    {
        //choose random point
        Vector3 pos = c.transform.localPosition;
        pos.y = max_height;
        Coral theCoral = coral[j].GetComponent<Coral>();
        int amount = Random.Range(theCoral.groupMin, theCoral.groupMax);
        int made = 0;

        //loop for every object
        for (int i = 0; i < amount; i++)
        {
            pos.x = c.transform.position.x + Random.Range(-1.0f, 1.0f);
            pos.z = c.transform.position.z + Random.Range(-1.0f, 1.0f);
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
                    //convert to degrees
                    angle = (angle * 180) / Mathf.PI;

                    if (CreateObject(theCoral, hit, angle, j) != null)
                    {
                        made++;
                    }
                }
            }
        }
        return made;
    }

    void testCollide(GameObject c)
    {
        Coral theCoral = c.GetComponent<Coral>();
        Vector3 pos= new Vector3(0.0f, 0.0f, 0.0f);
        while (theCoral.overlap)
        {
            pos.x = c.transform.position.x + Random.Range(-1.0f, 1.0f);
            pos.z = c.transform.position.z + Random.Range(-1.0f, 1.0f);
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
                    if (theCoral.changeAngle)
                    {
                        hit.point += (theCoral.offset * c.transform.localScale.y) * hit.normal;

                        //find axis of rotation
                        axis = Vector3.Normalize(Vector3.Cross(Vector3.up, hit.normal));
                        //multiply by angle
                        axis *= angle;
                    }

                    pos.y = hit.point.y;

                    c.GetComponent<Transform>().SetPositionAndRotation(pos, Quaternion.Euler(axis));
                }
            }
        }
    }

    bool Test(Coral theCoral, RaycastHit hit, float angle)

    {
        if (theCoral.mindepth < hit.point.y || theCoral.maxdepth > hit.point.y || theCoral.steepest < angle || theCoral.shallowest > angle)
        {
            return false;
        }
        return true;
    }


    GameObject CreateObject(Coral theCoral, RaycastHit hit, float angle, int j)
    {
        if (Test(theCoral, hit, angle))
        {
            Vector3 axis = new Vector3();

            if (theCoral.changeAngle)
            {
                hit.point += (theCoral.offset * coral[j].transform.localScale.y) * hit.normal;

                //find axis of rotation
                axis = Vector3.Normalize(Vector3.Cross(Vector3.up, hit.normal));
                //multiply by angle
                axis *= angle;
            }
            //create coral
            GameObject c = Instantiate(coral[j], hit.point, Quaternion.Euler(axis));
            float scale = c.transform.localScale.y * Random.Range(0.8f, 1.2f);
            c.transform.localScale += new Vector3(scale, scale, scale);
            c.transform.Rotate(new Vector3(0.0f, Random.Range(-180, 180), 0.0f));
            c.transform.parent = this.GetComponent<Transform>();
            c.GetComponentInChildren<Renderer>().material.color *= Random.Range(0.8f, 1.8f);
            corals.Add(c);
            testCollide(c);
            return c;
        }
        return null;
    }

    void OnDrawGizmosSelected()
    {
        //draw cube showing affected area
        Gizmos.color = new Color(0.5f, 0.0f, 0.5f, 0.2f);
        Gizmos.DrawCube(transform.localPosition, new Vector3(size, 2, size));
    }
}
