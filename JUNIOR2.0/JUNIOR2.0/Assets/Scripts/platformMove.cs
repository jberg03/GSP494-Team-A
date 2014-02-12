using UnityEngine;
using System.Collections;

public class platformMove : MonoBehaviour {

	public float distance = 15.0f;
	public PlatformDirection direction;
	protected Vector3 startPosition;
	public float speed = 3.0f;
	protected float traveledDistance;

	void Start () 
	{
		startPosition = transform.position;
		Debug.Log(startPosition);
	
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		traveledDistance = Vector3.Distance (this.startPosition, this.transform.position);
		switch(direction)
		{
		case PlatformDirection.up:
		case PlatformDirection.down:
			MoveUpDown();
			break;
		case PlatformDirection.left:
		case PlatformDirection.right:
			MoveLeftRight();
			break;
		case PlatformDirection.foward:
		case PlatformDirection.backward:
			MoveForwardBackward();
			break;
		case PlatformDirection.clockwise:
			RotateClockwise();
			break;
		}
	}

	protected void MoveForwardBackward()
	{
		switch(direction)
		{
		case PlatformDirection.foward:
			if(traveledDistance > this.distance)
		   	{
				this.transform.MoveBack(speed);
				direction = PlatformDirection.backward;
			}
			else
			{
				this.transform.MoveFoward(speed);
			}
			break;
		case PlatformDirection.backward:
			if(traveledDistance > this.distance)
			{
				this.transform.MoveFoward(speed);
				direction = PlatformDirection.foward;
			}
			else
			{
				this.transform.MoveBack(speed);
			}
			break;
		}
	}

	protected void MoveUpDown()
	{
		switch(direction)
		{
		case PlatformDirection.up:
			if(traveledDistance > this.distance)
			{
				this.transform.MoveDown(speed);
				direction = PlatformDirection.down;
			}
			else
			{
				this.transform.MoveUp(speed);
			}
			break;
		case PlatformDirection.down:
			if(traveledDistance > this.distance)
			{
				this.transform.MoveUp(speed);
				direction = PlatformDirection.up;
			}
			else
			{
				this.transform.MoveDown(speed);
			}
			break;
		}
	}

	protected void MoveLeftRight()
	{		
		switch(direction)
		{
		case PlatformDirection.left:
			if(traveledDistance > this.distance)
			{
				this.transform.MoveRight(speed);
				direction = PlatformDirection.right;
			}
			else
			{
				this.transform.MoveLeft(speed);
			}
			break;
		case PlatformDirection.right:
			if(traveledDistance > this.distance)
			{
				this.transform.MoveLeft(speed);
				direction = PlatformDirection.left;
			}
			else
			{
				this.transform.MoveRight(speed);
			}
			break;
		}
	}

	protected void RotateClockwise()
	{
		this.transform.RotateAround (this.transform.position - (Vector3.back * distance), Vector3.up, speed * Time.deltaTime);
	}
}
