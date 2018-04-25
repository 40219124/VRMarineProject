using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentForce : MonoBehaviour
{
    public MovementPath myPath;
    public float speed = 2.0f;
    public float maxDistanceToGoal = 0.1f;
    public bool reverse = false;

    private IEnumerator<Transform> pointInPath;

    private PulseControl pc;
    [SerializeField]
    private float intensity = 2.0f;
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
        if (myPath == null)
        {
            return;
        }

        pointInPath = myPath.GetNextPathPoint().GetEnumerator();
        pointInPath.MoveNext();
        if (pointInPath.Current == null)
        {
            return;
        }
        transform.position = pointInPath.Current.position;

        if (reverse)
        {
            bool onIndex = false;
            if (myPath.pathSequence.Length % 2 == 1)
            {
                if (myPath.pathType == MovementPath.PathTypes.linear)
                {
                    onIndex = true;
                }
            }
            else
            {
                if(myPath.pathType == MovementPath.PathTypes.loop)
                {
                    onIndex = true;
                }
            }

            int nextIdx = 0;
            if (onIndex)
            {
                transform.position = myPath.pathSequence[myPath.pathSequence.Length / 2].position;
                nextIdx = (myPath.pathSequence.Length / 2) + 1;
            }
            else
            {
                int index = (myPath.pathSequence.Length / 2) - 1;
                nextIdx = index + 1;
                Vector3 pos1 = myPath.pathSequence[index].position;
                Vector3 pos2 = myPath.pathSequence[index + 1].position;
                Vector3 p1top2 = pos2 - pos1;
                transform.position = pos1 + p1top2 / 2.0f;
            }
            while (myPath.movingTo < nextIdx)
            {
                pointInPath.MoveNext();
            }
        }

        for(int i = 1; i < myPath.pathSequence.Length; ++i)
        {
            distance += Vector3.Distance(myPath.pathSequence[i - 1].position, myPath.pathSequence[1].position);
        }
        if(myPath.pathType == MovementPath.PathTypes.loop)
        {
            distance += Vector3.Distance(myPath.pathSequence[0].position, myPath.pathSequence[myPath.pathSequence.Length - 1].position);
        }


        pc = gameObject.GetComponentInParent<PulseControl>();
        speed = distance / pc.Period;
        intensity = intensity / 10.0f;
        previousFrame = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (pointInPath == null || pointInPath.Current == null)
        {
            return;
        }

        if (myPath.movingTo == 0 && myPath.pathType == MovementPath.PathTypes.linear)
        {
            transform.position = myPath.pathSequence[0].transform.position;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, pointInPath.Current.position, Time.deltaTime * speed);
        }

        float distSq = (transform.position - pointInPath.Current.position).sqrMagnitude;

        if (distSq < maxDistanceToGoal * maxDistanceToGoal)
        {
            pointInPath.MoveNext();
        }

        force = Vector3.Normalize(transform.position - previousFrame) * intensity * (reverse ? -1 : 1);
        previousFrame = transform.position;
    }
}
