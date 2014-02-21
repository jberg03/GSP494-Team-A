using UnityEngine;
using System.Collections;

public class EffectScript : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
		AudioSource.PlayClipAtPoint (audio.clip, this.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
