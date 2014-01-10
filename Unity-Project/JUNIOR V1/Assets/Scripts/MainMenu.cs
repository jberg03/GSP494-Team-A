using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    

    void OnGUI()
    {
        GUI.BeginGroup(new Rect(Screen.width / 2 - 64, Screen.height / 2 - 128, 256, 250));

        GUI.Box(new Rect(0, 0, 256, 512), "J.U.N.I.O.R.");

        if (GUI.Button(new Rect(256 / 2 - 40, 40, 80, 30), "Play"))
        {
            Application.LoadLevel("Grounds001");
        }
        if (GUI.Button(new Rect(256 / 2 - 40, 80, 80, 30), "Credits"))
        {
            //add credits here
        }
        if (GUI.Button(new Rect(256 / 2 - 40, 120, 80, 30), "Exit"))
        {
            Application.Quit();
        }

        GUI.EndGroup();
    }
}
