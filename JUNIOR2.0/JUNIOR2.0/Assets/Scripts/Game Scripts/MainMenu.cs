using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    void OnGUI()
    {
        Screen.showCursor = true;

        if (GUI.Button(new Rect(Screen.width / 2 - 75, 160, 150, 75), "PLAY"))
            Application.LoadLevel("Level01");
        
        if (GUI.Button(new Rect(Screen.width / 2 - 75, 250, 150, 75), "CREDITS"))
            Application.LoadLevel("Credits");

        if (GUI.Button(new Rect(Screen.width / 2 - 75, 340, 150, 75), "QUIT"))
            Application.Quit();
    }
}

