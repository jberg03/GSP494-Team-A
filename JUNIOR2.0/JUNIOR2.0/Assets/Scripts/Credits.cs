using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {

    void OnGUI()
    {
        GUI.Box(new Rect(Screen.width / 2 - 300, 30, 600, Screen.height - 60), "Credits");
        GUI.Label(new Rect(Screen.width / 2 - 40, 50, 120, 30), "Programmers");
        GUI.Label(new Rect(Screen.width / 2 - 110, 70, 400, 30), "John Berg                        Michael Moul");
        GUI.Label(new Rect(Screen.width / 2 - 45, 90, 120, 30), "Level Designer");
        GUI.Label(new Rect(Screen.width / 2 - 35, 110, 120, 30), "Eric Cowan");
        GUI.Label(new Rect(Screen.width / 2 - 20, 130, 120, 30), "Artist");
        GUI.Label(new Rect(Screen.width / 2 - 45, 150, 120, 30), "David Emerson");
        GUI.Label(new Rect(Screen.width / 2 - 20, 170, 120, 30), "Audio");
        GUI.Label(new Rect(Screen.width / 2 - 53, 190, 120, 30), "Orlando Camacho");
        GUI.Label(new Rect(Screen.width / 2 - 58, 290, 120, 30), "Special Thanks To");
        GUI.Label(new Rect(Screen.width / 2 - 110, 310, 220, 30), "GHAD for 52 special effects package");
        GUI.Label(new Rect(Screen.width / 2 - 155, 330, 310, 30), "https://www.assetstore.unity3d.com/#/content/10419");
        GUI.Label(new Rect(Screen.width / 2 - 110, Screen.height - 80, 230, 30), "Press 'ESC' to go back to Main Menu");

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.LoadLevel("MainMenu");
    }
}
