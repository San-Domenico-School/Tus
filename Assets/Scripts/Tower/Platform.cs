using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Tuan Le
//1/24/2025
//Move object to next way point
public class Platform : MonoBehaviour
{
    [SerializeField] private WayPointPath _waypointPath;
    [SerializeField] private float _speed;
    
    private int _targetWaypointIndex;
    private Transform _previousWaypoint;
    private Transform _targetWaypoint;
    private float _timeToWayPoint;
    private float _elapsedTime;
    private bool activated = false;


    //Call method for check next wayPoint
    void Start()
    {
        TargetNextWaypoint();
    }

    // Update is called once per frame
    void Update()
    {
        if(activated)
        {
            _elapsedTime += Time.deltaTime;

            float elapsedPercentage = _elapsedTime / _timeToWayPoint;
            transform.position = Vector3.Lerp(_previousWaypoint.position, _targetWaypoint.position, elapsedPercentage);
        }        
    }

    //Read
    private void TargetNextWaypoint()
    {
        _previousWaypoint = _waypointPath.GetWayPoint(_targetWaypointIndex);
        _targetWaypointIndex = _waypointPath.GetNextWayPointIndex(_targetWaypointIndex);
        _targetWaypoint = _waypointPath.GetWayPoint(_targetWaypointIndex);

        _elapsedTime = 0;

        float distanceTowayPoint = Vector3.Distance(_previousWaypoint.position, _targetWaypoint.position);
        _timeToWayPoint = distanceTowayPoint / _speed;
    }

    //Activate the sequence
    public void ActivatePath()
    {
        activated = true;
    }
}
