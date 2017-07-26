using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiController : MonoBehaviour
{

    //Gui info
    public Text upvoteDisplay;
    public Text downvoteDisplay;
    public Text fameDisplay;
    float fame = 0f;

    //Gui stuff
    /*Vector2 fameBarPosition = new Vector2(100, 540);
    Vector2 fameBarSize = new Vector2(100, 20);
    Texture2D emptyBarTexture;
    Texture2D fullBarTexture;
    GUIStyle fullBarStyle = new GUIStyle();
    GUIStyle emptyBarStyle = new GUIStyle();
    Color barrFillColor = Color.green;
    Color emptyBarrFillColor = Color.red;*/

    /*private void Awake()
    {
        fullBarTexture = new Texture2D(1, 1);
        emptyBarTexture = new Texture2D(1, 1);
    }

    private void OnGUI()
    {
        // draw the background:
        emptyBarTexture.SetPixel(0, 0, emptyBarrFillColor);
        emptyBarTexture.Apply();
        emptyBarStyle.normal.background = emptyBarTexture;
        GUI.BeginGroup(new Rect(fameBarPosition.x, fameBarPosition.y, fameBarSize.x, fameBarSize.y));
        GUI.Box(new Rect(0, 0, fameBarSize.x, fameBarSize.y), new GUIContent(""), emptyBarStyle);

        // draw the filled-in part:
        fullBarTexture.SetPixel(0, 0, barrFillColor);
        fullBarTexture.Apply();
        fullBarStyle.normal.background = fullBarTexture;
        GUI.BeginGroup(new Rect(0, 0, fameBarSize.x * (fame / 100f), fameBarSize.y));
        GUI.Box(new Rect(0, 0, fameBarSize.x, fameBarSize.y), new GUIContent(""), fullBarStyle);
        GUI.EndGroup();

        GUI.EndGroup();
    }*/

    public void UpdateDisplays(float upvotes, float downvotes, float fame)
    {
        upvoteDisplay.text = "Upvotes: " + upvotes;
        downvoteDisplay.text = "Downvotes: " + downvotes;
        fameDisplay.text = "Fame: " + fame + "%";
    }
}
