using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Weapon : MonoBehaviour
{
	protected float elapsedTime = 0.0f;
	public GameObject bullet;
	private AudioSource audio;

	void Start()
	{
				audio = bullet.audio;
	}

	void Update()
	{
		elapsedTime += Time.deltaTime;
	}

	public void Fire()
	{
		//get the bullet spawn points
		//shoot from the spawn points
		if(elapsedTime > bullet.GetComponent<Bullet>().shootRate)
		{
			IEnumerable<Transform> spawnPoints = this.transform.GetComponentsInChildren<Transform> ().Where (child => child.name == "BulletSpawnPoint");
			foreach(Transform spawnPoint in spawnPoints)
			{
				if(audio.clip.isReadyToPlay)
				{
					AudioSource.PlayClipAtPoint(audio.clip, spawnPoint.position);
				}
				Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
			}
			elapsedTime = 0.0f;
		}
	}

	public void ChangeAmmo(GameObject newBullet)
	{
		bullet = newBullet;
	}
}

