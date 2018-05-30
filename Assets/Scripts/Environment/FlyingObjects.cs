using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingObjects : MonoBehaviour {

    public bool rotate;
    public float RotationSpeed = 10f;

    public bool movement;
    public Vector3[] localWaypoints;
    private Vector3[] globalWaypoints;
    public float speed;
    public bool cyclic;
    public float waitTime;
    [Range(0, 2)]
    public float easeAmount;

    private int fromWaypointIndex;
    private float percentBetweenWaypoints;
    private float nextMoveTime;

    void Start()
    {
        if(movement == true)
        {
            globalWaypoints = new Vector3[localWaypoints.Length];

            for (int i = 0; i < globalWaypoints.Length; i++)
            {
                globalWaypoints[i] = localWaypoints[i] + transform.position;
            }
        }   
    }

    void Update()
    {
        if(rotate == true)
        {
            transform.Rotate(Vector3.left, RotationSpeed * Time.deltaTime);
            transform.Rotate(Vector3.up, RotationSpeed * Time.deltaTime);
        }
        
        if(movement == true)
        {
            Vector3 velocity = CalculatePlatformMovement();
            transform.Translate(velocity);
        }
    }

    private float Ease(float x)
    {
        float a = easeAmount + 1f;
        return Mathf.Pow(x, a) / (Mathf.Pow(x, a) + Mathf.Pow(1 - x, a));
    }

    private Vector3 CalculatePlatformMovement()
    {
        if (Time.time < nextMoveTime)
        {
            return Vector3.zero;
        }

        fromWaypointIndex %= globalWaypoints.Length;
        int toWaypointIndex = (fromWaypointIndex + 1) % globalWaypoints.Length;
        float distanceBetweenWaypoints = Vector3.Distance(globalWaypoints[fromWaypointIndex], globalWaypoints[toWaypointIndex]);
        percentBetweenWaypoints += Time.deltaTime * speed / distanceBetweenWaypoints;

        percentBetweenWaypoints = Mathf.Clamp01(percentBetweenWaypoints);
        float easedPercentBetweenWaypoints = Ease(percentBetweenWaypoints);

        Vector3 newPos = Vector3.Lerp(globalWaypoints[fromWaypointIndex], globalWaypoints[toWaypointIndex], easedPercentBetweenWaypoints);

        if (percentBetweenWaypoints >= 1)
        {
            percentBetweenWaypoints = 0f;
            fromWaypointIndex++;

            if (!cyclic)
            {
                if (fromWaypointIndex >= globalWaypoints.Length - 1)
                {
                    fromWaypointIndex = 0;
                    System.Array.Reverse(globalWaypoints);
                }
            }

            nextMoveTime = Time.time + waitTime;
        }

        return newPos - transform.position;
    }

    private void OnDrawGizmos()
    {
        if (localWaypoints != null)
        {
            Gizmos.color = Color.red;
            float size = 2.5f;

            for (int i = 0; i < localWaypoints.Length; i++)
            {
                Vector3 globalWaypointPos = (Application.isPlaying) ? globalWaypoints[i] : localWaypoints[i] + transform.position;
                Gizmos.DrawLine(globalWaypointPos - Vector3.up * size, globalWaypointPos + Vector3.up * size);
                Gizmos.DrawLine(globalWaypointPos - Vector3.left * size, globalWaypointPos + Vector3.left * size);
                Gizmos.DrawLine(globalWaypointPos - Vector3.right * size, globalWaypointPos + Vector3.right * size);
                Gizmos.DrawLine(globalWaypointPos - Vector3.down * size, globalWaypointPos + Vector3.down * size);
            }
        }
    }
}
