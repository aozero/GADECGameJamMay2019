using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Text gameOverText;
    public Text gameOverReason;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            Application.Quit();
        }
    }
    
    void Awake() {
        gameOverText.text = Strings.str[Globals.lang, 6];
        gameOverReason.text = Strings.str[Globals.lang, 7];    
    }
}
