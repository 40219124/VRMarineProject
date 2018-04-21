using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPath : MonoBehaviour
{

    public enum PathTypes { loop, linear };

    public PathTypes pathType;
    public int movementDirection = 1;
    public int movingTo = 0;
    public Transform[] pathSequence;

    public void OnDrawGizmos()
    {
        if (pathSequence == null || pathSequence.Length < 2)
        {
            return;
        }

        for (int i = 1; i < pathSequence.Length; ++i)
        {
            Gizmos.DrawLine(pathSequence[i - 1].position, pathSequence[1].position);
        }

        if (pathType == PathTypes.loop)
        {
            Gizmos.DrawLine(pathSequence[0].position, pathSequence[pathSequence.Length - 1].position);
        }
    }

    public IEnumerable<Transform> GetNextPathPoint()
    {
        if (pathSequence == null || pathSequence.Length < 1)
        {
            yield break;
        }

        while (true)
        {
            yield return pathSequence[movingTo];
            if (pathSequence.Length < 1)
            {
                continue;
            }

            movingTo += movementDirection;
            if (pathType == PathTypes.linear)
            {
                if (movingTo >= pathSequence.Length)
                {
                    movingTo = 0;
                }
            }

            if (pathType == PathTypes.loop)
            {
                if (movingTo >= pathSequence.Length)
                {
                    movingTo = 0;
                }
                if (movingTo < 0)
                {
                    movingTo = pathSequence.Length - 1;
                }
            }
        }
        
    }
}