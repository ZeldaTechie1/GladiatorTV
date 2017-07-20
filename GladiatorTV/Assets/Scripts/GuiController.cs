using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiController : MonoBehaviour {

    public Text upvoteDisplay;
    public Text downvoteDisplay;
    public Text fameDisplay;
    float fame = 0f;
    Vector2 pos = new Vector2(80, 540);
    Vector2 size = new Vector2(100,20);
    Texture2D emptyBarTexture;
    Texture2D fullBarTexture;
    GUIStyle fullBarStyle = new GUIStyle();
    GUIStyle emptyBarStyle = new GUIStyle();
    Color barrFillColor = Color.green;
    Color emptyBarrFillColor = Color.red;

    private void Awake()
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
        GUI.BeginGroup(new Rect(pos.x, pos.y, size.x, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), new GUIContent(""), emptyBarStyle);

        // draw the filled-in part:
        fullBarTexture.SetPixel(0, 0, barrFillColor);
        fullBarTexture.Apply();
        fullBarStyle.normal.background = fullBarTexture;
        GUI.BeginGroup(new Rect(0, 0, size.x * (fame/100f), size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), new GUIContent(""),fullBarStyle);
        GUI.EndGroup();

        GUI.EndGroup();
    }

    public void UpdateDisplays(float upvotes, float downvotes, float fame)
    {
        upvoteDisplay.text = "Upvotes: " + upvotes;
        downvoteDisplay.text = "Downvotes: " + downvotes;
        this.fame = fame;
    }
}
