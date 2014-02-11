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

	public static void MoveFoward(this Transform trans)
	{
		trans.Translate (Vector3.back * trans.GetComponent<EnemyInfo> ().GetMovementSpeed() * Time.deltaTime);
	}
}
