using UnityEngine;
using System.Collections;
using System.Linq;

public class EnemyShoot : MonoBehaviour
{

	public void shoot(Transform target)
	{
		//determine weapons
		Weapon[] weapons = this.gameObject.GetComponentsInChildren<Weapon> ();

		foreach (Weapon weapon in weapons)
		{
			//aim weapons
			weapon.transform.RotateToward(target);
			weapon.Fire();
			//shoot weapons
		}

	}
}

