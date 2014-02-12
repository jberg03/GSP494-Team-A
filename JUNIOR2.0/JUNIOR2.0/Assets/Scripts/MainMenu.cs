using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width / 2 - 75, 160, 150, 75), "PLAY"))
            Application.LoadLevel("Level01");
        
        if (GUI.Button(new Rect(Screen.width / 2 - 75, 250, 150, 75), "QUIT"))
            Application.Quit();
    }
}

