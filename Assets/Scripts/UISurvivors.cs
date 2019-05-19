using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISurvivors : MonoBehaviour
{
    public Image image;
    public Text objectiveText;
    public float emptyAlpha;
    public float fullAlpha;

    private string getSurvivors = "Rescue the survivors!";
    private string returnHome = "Survivors onboard. Return home!";

    // Start is called before the first frame update
    void Start()
    {
        SetSurvivors(false);
    }

    // Set it to appear that we have suvivors on board
    public void SetSurvivors(bool gotSurvivors)
    {
        string newObjText = getSurvivors; 
        float newAlpha = emptyAlpha;
        if (gotSurvivors)
        {
            newObjText = returnHome;
            newAlpha = fullAlpha;
        }

        objectiveText.text = newObjText;

        var tempColor = image.color;
        tempColor.a = newAlpha;
        image.color = tempColor;
    }
}
