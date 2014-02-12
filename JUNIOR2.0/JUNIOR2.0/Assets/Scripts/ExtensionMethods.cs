using UnityEngine;
using System.Collections;

public static class ExtensionMethods
{
	public static Heading GetDirection(this Transform trans)
	{
		Vector3 rotation = trans.rotation.eulerAngles;
		if(rotation.y > 359.8f || rotation.y < 0.2f)
		{
			return Heading.north;
		}
		else if(rotation.y >= 0.2f && rotation.y < 89.8f)
		{
			return Heading.northeast;
		}
		else if(rotation.y >= 89.8f && rotation.y < 90.2f)
		{
			return Heading.east;
		}
		else if(rotation.y >= 90.2f && rotation.y < 179.8f)
		{
			return Heading.southeast;
		}
		else if(rotation.y >= 179.8f && rotation.y < 180.2f)
		{
			return Heading.south;
		}
		else if(rotation.y >= 180.2f && rotation.y < 269.8f)
		{
			return Heading.southwest;
		}
		else if(rotation.y >= 269.8f && rotation.y < 270.2f)
		{
			return Heading.west;
		}
		else
		{
			return Heading.northwest;
		}
	}

	public static void RotateYToward(this Transform trans, Transform other)
	{
		Quaternion rotateSelf = Quaternion.LookRotation (trans.position - other.position);
		trans.rotation = Quaternion.Slerp(trans.rotation, rotateSelf, trans.GetComponent<EnemyInfo> ().rotationSpeed * Time.deltaTime);
		trans.rotation = new Quaternion (0, trans.rotation.y, 0, trans.rotation.w);
	}

	public static void RotateYAway(this Transform trans, Transform other)
	{
		Quaternion rotateSelf = Quaternion.LookRotation (other.position - trans.position);
		trans.rotation = Quaternion.Slerp(trans.rotation, rotateSelf, trans.GetComponent<EnemyInfo> ().rotationSpeed * Time.deltaTime);
		trans.rotation = new Quaternion (0, trans.rotation.y, 0, trans.rotation.w);
	}

	public static void RotateToward(this Transform trans, Transform other)
	{
		Quaternion rotateSelf = Quaternion.LookRotation (trans.position - other.position);
		trans.rotation = Quaternion.Slerp(trans.rotation, rotateSelf, trans.GetComponent<EnemyInfo> ().rotationSpeed * Time.deltaTime);
	}
	
	public static void RotateAway(this Transform trans, Transform other)
	{
		Quaternion rotateSelf = Quaternion.LookRotation (other.position - trans.position);
		trans.rotation = Quaternion.Slerp(trans.rotation, rotateSelf, trans.GetComponent<EnemyInfo> ().GetRotationSpeed() * Time.deltaTime);
	}

	public static void MoveFoward(this Transform trans, float speed)
	{
		trans.Translate (Vector3.forward * speed * Time.deltaTime);
	}

	public static void MoveBack(this Transform trans, float speed)
	{
		trans.Translate (Vector3.back * speed * Time.deltaTime);
	}

	public static void MoveLeft(this Transform trans, float speed)
	{
		trans.Translate (Vector3.left * speed * Time.deltaTime);
	}

	public static void MoveRight(this Transform trans, float speed)
	{
		trans.Translate (Vector3.right * speed * Time.deltaTime);
	}

	public static void MoveUp(this Transform trans, float speed)
	{
		trans.Translate (Vector3.up * speed * Time.deltaTime);
	}

	public static void MoveDown(this Transform trans, float speed)
	{
		trans.Translate (Vector3.down * speed * Time.deltaTime);
	}

	public static void RotateClockwise(this Transform trans, float speed)
	{
		trans.RotateAround (trans.position - (Vector3.back * 20), Vector3.up, speed * Time.deltaTime);
	}

	public static void RotateCounterClockwise(this Transform trans)
	{
		trans.Translate (Vector3.back * trans.GetComponent<EnemyInfo> ().GetMovementSpeed() * Time.deltaTime);
	}
}

