using UnityEngine;
using System.Collections;
using System.Linq;

public class crosshairs : MonoBehaviour {

	public Texture2D crosshairImage;

	void OnGUI()
	{
        Screen.showCursor = false;
		float xMin = (Screen.width / 2) - (crosshairImage.width / 2);
		float yMin = (Screen.height / 2) - (crosshairImage.height / 2);
		GUI.DrawTexture(new Rect(xMin, yMin, crosshairImage.width, crosshairImage.height), crosshairImage);
	}

	void FixedUpdate()
	{
		foreach(Transform spawnPoint in this.transform.GetComponentsInChildren<Transform>().Where(point => point.name.ToLower() == "bulletspawnpoint"))
		{
			Transform[] mainCamera = this.transform.parent.GetComponentsInChildren<Transform>().Where(child => child.name == "Main Camera").ToArray();
			spawnPoint.rotation = mainCamera[0].rotation;
		}
	}
}
