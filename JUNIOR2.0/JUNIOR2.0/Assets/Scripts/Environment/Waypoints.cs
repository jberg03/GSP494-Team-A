using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Waypoints : MonoBehaviour
{
	public Transform[] waypoints;

	// Use this for initialization
	void Start ()
	{
		waypoints = this.transform.GetComponentsInChildren<Transform> ().Where (waypoint => waypoint.tag.ToLower ().Equals ("waypoint")).ToArray();
		waypoints.OrderBy (waypoint => waypoint.GetComponent<Waypoint> ().waypointNum);
	}

	public Transform GetNextPoint(Transform currentPoint)
	{
		int point = currentPoint.GetComponent<Waypoint>().waypointNum + 1;
		if(point > waypoints.Count())
		{
			point = 1;
		}
		return GetPoint (point);

	}

	public Transform GetFirstPoint()
	{
		return GetPoint (1);
	}

	private Transform GetPoint(int point)
	{
		foreach(Transform waypoint in waypoints)
		{
			if(waypoint.GetComponent<Waypoint>().waypointNum == point)
			{
				return waypoint;
			}
		}
		return waypoints[0];
	}
}

