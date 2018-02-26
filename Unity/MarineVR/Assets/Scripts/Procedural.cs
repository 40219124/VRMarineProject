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

	// Use this for initialization
	void Start () {
        Generate();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.P))
        {
            Generate();
        }
	}

    private void Generate()
    {
        for (int i = 0; i < objects; i++)
        {
            Vector3 pos = transform.localPosition + new Vector3(Random.Range(-width / 2, width / 2), 0, Random.Range(-length / 2, length / 2));
            Ray down = new Ray(pos, Vector3.down);
            RaycastHit hit;

            if (Physics.Raycast(down, out hit))
            {
                if (hit.collider.tag == "Ground")
                {
                    Debug.Log("Ground");
                    pos.y = hit.point.y;
                    Instantiate(coral, pos, Quaternion.identity);

                }
            }

        }
    }

    void OnDrawGizmosSelected()
    {
        //draw range sphere
        Gizmos.color = new Color(0.5f, 0.0f, 0.5f, 0.2f);
        Gizmos.DrawCube(transform.localPosition, new Vector3(width, 2, length));
    }
}
