using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button playButton;
    public Button optionsButton;
    public Button quitButton;
    public Text playButtonText;
    public Text optionsButtonText;
    public Text quitButtonText;
    
    // Start is called before the first frame update
    void Start()
    {
        //playButton.enabled = true;
        //optionsButton.enabled = true;
        playButton.GetComponentInChildren<Text>().text = Strings.str[Globals.lang, 0];
        optionsButtonText.text = Strings.str[Globals.lang, 1];
        quitButtonText.text = Strings.str[Globals.lang, 2];
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("HIT");
    }
    
    void doExitGame()
    {
        Application.Quit();
    }
    
    void OnGUI () {
      
      // another code above...
      
        bool quitGame = GUI.Button(new Rect(Screen.width/2 - 100, Screen.height - 200, 200, 20), "Quit Game");
      
        if(quitGame) {
      
            Application.Quit(); // As far as I know, this only works in the compiled game (.exe)
      
        }
    }
}
