using UnityEngine;
using System.Collections;

public class FSM : MonoBehaviour {
	// Keep track of the player
	protected Transform playerTransform;

	//Distance to next destination
	protected Vector3 destPos;

	//collection of points for patrolling
	protected Transform[] waypoints;

	//methods
	protected virtual void Initialize () { }
	protected virtual void FSMUpdate () { }
	protected virtual void FSMFixedUpdate () { }

	// Use this for initialization
	void Start () 
	{
		Initialize ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		FSMUpdate ();
	}

	void FixedUpdate()
	{
		FSMFixedUpdate ();
	}
}
