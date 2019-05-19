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

    // Start is called before the first frame update
    void Start()
    {
        SetSurvivors(false, false);
    }

    // Set it to appear that we have suvivors on board
    public void SetSurvivors(bool gotSurvivors, bool objectiveSet)
    {
        // Set the transparency of the survivors icon
        float newAlpha = emptyAlpha;
        if (gotSurvivors)
        {
            newAlpha = fullAlpha;
        }

        var tempColor = image.color;
        tempColor.a = newAlpha;
        image.color = tempColor;


        // Set the objective text beside the survivors icon
        if (objectiveSet)
        {
            if (gotSurvivors)
            {
                objectiveText.text = Strings.str[Globals.lang,5];
            } else
            {
                objectiveText.text = Strings.str[Globals.lang,4];
            }
        } else
        {
            objectiveText.text = Strings.str[Globals.lang,3];
        }
    }
}
