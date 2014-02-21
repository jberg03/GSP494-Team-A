using UnityEngine;
using System.Collections;

public class platformMove : MonoBehaviour {

	public float distance = 15.0f;
	public PlatformDirection direction;
	public PlatformRotation rotation;
	public Vector3 startPosition;
	public float speed = 3.0f;
	public float traveledDistance;

	void Start () 
	{
		startPosition = this.transform.position;
		
		if(rotation != PlatformRotation.none)
		{

			switch(direction)
			{
			case PlatformDirection.up:
				startPosition += Vector3.up * distance;
				break;
			case PlatformDirection.down:
				startPosition += Vector3.down * distance;
				break;
			case PlatformDirection.left:
				startPosition += Vector3.left * distance;
				break;
			case PlatformDirection.right:
				startPosition += Vector3.right * distance;
				break;
			case PlatformDirection.backward:
				startPosition += Vector3.back * distance;
				break;
			case PlatformDirection.foward:
				startPosition += Vector3.forward * distance;
				break;
			}
		}

	
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		traveledDistance = Vector3.Distance (this.startPosition, this.transform.position);
		if(rotation == PlatformRotation.none)
		{
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
			}
		}
		else
		{
			switch(rotation)
			{
			case PlatformRotation.clockwise:
				RotateClockwise();
				break;
			case PlatformRotation.counterClockwise:
				RotateCounterClockwise();
				break;
			}
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
		//this.transform.RotateAround (this.transform.position - (Vector3.back * distance), Vector3.up, speed * Time.deltaTime);
		//this.transform.RotateYToward (startPosition, speed);
		switch(direction)
		{
		case PlatformDirection.up:
		case PlatformDirection.down:
			this.transform.RotateAround(startPosition, Vector3.forward, speed * Time.deltaTime);
			break;
		case PlatformDirection.left:
		case PlatformDirection.right:
			this.transform.RotateAround(startPosition, Vector3.up, speed * Time.deltaTime);
			break;
		case PlatformDirection.backward:
		case PlatformDirection.foward:
			this.transform.RotateAround(startPosition, Vector3.left, speed * Time.deltaTime);
			break;

		}

	}

	protected void RotateCounterClockwise()
	{
		//this.transform.RotateAround (this.transform.position - (Vector3.back * distance), Vector3.up, speed * Time.deltaTime);
		//this.transform.RotateYToward (startPosition, speed);
		switch(direction)
		{
		case PlatformDirection.up:
		case PlatformDirection.down:
			this.transform.RotateAround(startPosition, Vector3.forward, (-1.0f) * speed * Time.deltaTime);
			break;
		case PlatformDirection.left:
		case PlatformDirection.right:
			this.transform.RotateAround(startPosition, Vector3.up, (-1.0f) * speed * Time.deltaTime);
			break;
		case PlatformDirection.backward:
		case PlatformDirection.foward:
			this.transform.RotateAround(startPosition, Vector3.left, (-1.0f) * speed * Time.deltaTime);
			break;
			
		}
		
	}
}
