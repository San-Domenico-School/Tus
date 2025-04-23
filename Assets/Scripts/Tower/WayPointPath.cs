using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Tuan Le
//1/24/2025
//Fetch next waypoint path
public class WayPointPath : MonoBehaviour
{
    public Transform GetWayPoint(int waypointIndex)
    {
        return transform.GetChild(waypointIndex);
    }

    public int GetNextWayPointIndex(int currentWayPointIndex)
    {
        int nextWayPointIndex = currentWayPointIndex + 1;

        if (nextWayPointIndex == transform.childCount)
        {
            nextWayPointIndex = 0;
        }
        return nextWayPointIndex;
    }
}
