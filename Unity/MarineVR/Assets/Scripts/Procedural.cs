using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Procedural : MonoBehaviour {
    [SerializeField]
    private float size;
    [SerializeField]
    public GameObject coral;

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
        Vector3 pos = transform.localPosition + new Vector3(Random.Range(-size/2, size/2), Random.Range(-size/2, size/2), Random.Range(-size/2, size/2));

        Instantiate(coral, pos, Quaternion.identity);
    }

    void OnDrawGizmosSelected()
    {
        //draw range sphere
        Gizmos.color = new Color(0.5f, 0.0f, 0.5f, 0.2f);
        Gizmos.DrawCube(transform.localPosition, new Vector3(size, size, size));
    }
}
