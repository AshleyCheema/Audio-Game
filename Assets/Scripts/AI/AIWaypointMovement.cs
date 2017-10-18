using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWaypointMovement : MonoBehaviour
{
	public GameObject aiCharacter;

	public float minDistance;
	public float moveSpeed;
	public GameObject goWayPointParent;
	Transform[] waypoints;
	int waypointIndex;

	// Use this for initialization
	void Start ()
	{
		waypointIndex = 0;

		waypoints = new Transform[goWayPointParent.transform.childCount];
		for ( int i = 0; i < goWayPointParent.transform.childCount; ++i )
		{
			waypoints[i] = goWayPointParent.transform.GetChild(i).transform;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Vector3.Distance(aiCharacter.transform.position, waypoints[waypointIndex].position) < minDistance)
		{
			if ( waypointIndex < waypoints.Length - 1 )
			{
				waypointIndex++;
			}
			else
			{
				waypointIndex = 0;
			}
		}
		else
		{
			aiCharacter.transform.position = Vector3.MoveTowards(aiCharacter.transform.position, waypoints[waypointIndex].position, moveSpeed * Time.deltaTime);
			aiCharacter.transform.LookAt(waypoints[waypointIndex].position);
		}
	}
}
