using UnityEngine;
using System.Collections;

public class platformMove : MonoBehaviour {

	public Vector3 endPosition;
	public float speed;
	public Vector3 startPosition;


	void Start () {
		startPosition = transform.position;
		Debug.Log(startPosition);
	
	}
	
	// Update is called once per frame
	void Update () {


	
	}
}
