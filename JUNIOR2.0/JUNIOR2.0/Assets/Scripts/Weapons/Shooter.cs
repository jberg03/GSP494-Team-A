using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {

	// Use this for initialization
	public GameObject bullet;

	void Start()
	{

	}

	// Update is called once per frame
	void Update () 
	{
		//transform.Translate(h , v, 0);

		if (Input.GetButtonUp ("Fire1"))
		{
			Weapon[] weapons = this.GetComponentsInChildren<Weapon>();
			foreach(Weapon weapon in weapons)
			{
				weapon.Fire();
			}
		}
	
	}
}
