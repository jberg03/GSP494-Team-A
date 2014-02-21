using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Waypoints : MonoBehaviour
{
	private IEnumerable<Transform> waypoints;

	// Use this for initialization
	void Start ()
	{
		waypoints = this.transform.GetComponentsInChildren<Transform> ().Where (waypoint => waypoint.tag.ToLower ().Equals ("waypoint"));
		waypoints.OrderBy (waypoint => waypoint.GetComponent<Waypoint> ().waypointNum);
	}

	public Transform GetNextPoint(Transform currentPoint)
	{
		if(currentPoint.GetComponent<Waypoint>().waypointNum == waypoints.Count())
		{
			return waypoints.First();
		}
		return waypoints.ElementAt (currentPoint.GetComponent<Waypoint> ().waypointNum);
	}

	public Transform GetFirstPoint()
	{
		return waypoints.First ();
	}
}

