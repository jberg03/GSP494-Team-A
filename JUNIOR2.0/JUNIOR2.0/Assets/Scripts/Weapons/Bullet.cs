using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float Speed = 100.0f;
	public float LifeTime = 3.0f;
	public int damage = 3;
	public float shootRate = 1.0f;
	public StatusCondition condition;

	
	void Start()
	{
		//remove the object after so long
		Destroy(gameObject, LifeTime);
		Vector3 forward = transform.TransformDirection (Vector3.forward);
		this.rigidbody.AddForce (forward * Speed);

	}
	
	void Update()
	{
		transform.position += transform.forward * Speed * Time.deltaTime;       
	}
	
	void OnCollisionEnter(Collision other)
	{
		//ContactPoint contact = collision.contacts[0];
		//Instantiate(Explosion, contact.point, Quaternion.identity);
		Destroy(gameObject);
	}

	public StatusCondition GetCondition()
	{
		return condition;
	}
}
