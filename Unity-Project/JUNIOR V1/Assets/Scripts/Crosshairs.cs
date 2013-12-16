using UnityEngine;
using System.Collections;

/****************************************************************************************
 * How to use this script
 * 
 * 1. make two cubes one called raycaster and the other one endPoint
 * 2. remove the mesh render and collider from both
 * 3. make both cubes a child of gun_blaster
 * 4. rotate each block 90 in the y coordinate
 * 5. put this script on the raycaster
 * 6. place the raycaster front/center of player
 * 7. place the endPoint 20 units further than the raycaster in the x direction
 * 8. create another cube called crosshairs
 * 9. put a crosshair texture on it and scale Z coordinate to 0
 * 10. select the raycaster and in the components section for this script drag the 
 * crosshair to the crosshair variable and endpoint to the endpoint variable
 * **************************************************************************************/

public class Crosshairs : MonoBehaviour {
    public Transform crosshair;
    public Transform endPoint;
    public string tagCheck = "";
    public bool foundHit = false;

	// Update is called once per frame
	void Update () 
    {
        RaycastHit[] hits;
        RaycastHit hitUse = new RaycastHit();
        foundHit = false;
        float shortestDist;
        shortestDist = 20f;
        hits = Physics.RaycastAll(transform.position, transform.forward, shortestDist);
        
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.tag == tagCheck && hits[i].distance < shortestDist)
            {
                hitUse = hits[i];
                shortestDist = hits[i].distance;
                foundHit = true;
            }
        }

        if (foundHit)
        {
            crosshair.position = hitUse.point;
            crosshair.rotation = Quaternion.LookRotation(hitUse.normal * -90f);
        }
        else
        {
            crosshair.position = endPoint.position;
            crosshair.rotation = endPoint.rotation;
        }
	}
}
