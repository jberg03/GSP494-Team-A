using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour 
{
	public int health = 100;
	public int maxHealth = 100;
	public float regenRate = 10.0f;
	public int regenAmount = 1;
	private float elapsedTime = 0.0f;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		elapsedTime += Time.deltaTime;
		if(elapsedTime >= this.regenRate)
		{
			elapsedTime = 0.0f;
			if(health < maxHealth)
			{
				health += regenAmount;
			}
		}
	}

	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "Bullet")
		{
			health -= other.gameObject.GetComponent<Bullet>().damage;
		}
	}
}
