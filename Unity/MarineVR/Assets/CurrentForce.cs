using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentForce : MonoBehaviour
{

    private List<Transform> waypoints = new List<Transform>();
    private PulseControl pc;
    [SerializeField]
    private float intensity = 0.2f;
    private float distance = 0.0f;
    private Vector3 previousFrame;
    private Vector3 force;

    public Vector3 Force
    {
        get { return force; }
    }

    // Use this for initialization
    void Start()
    {
        pc = gameObject.GetComponentInParent<PulseControl>();
        for (int i = 1; i < waypoints.Count; ++i)
        {
            distance += Vector3.Distance(waypoints[i - 1].position, waypoints[1].position);
        }
        previousFrame = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        force = Vector3.Normalize(transform.position - previousFrame) * Time.deltaTime * intensity * pc.Pulse;
        previousFrame = transform.position;
    }
}
