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
    Vector2 fameBarPosition = new Vector2(80, 540);
    Vector2 fameBarSize = new Vector2(100, 20);
    Texture2D emptyBarTexture;
    Texture2D fullBarTexture;
    GUIStyle fullBarStyle = new GUIStyle();
    GUIStyle emptyBarStyle = new GUIStyle();
    Color barrFillColor = Color.green;
    Color emptyBarrFillColor = Color.red;

    //Timer stuff
    [SerializeField]
    Text timer;
    float time;
    public class TimerEvent : UnityEngine.Events.UnityEvent { };
    TimerEvent TimerEnded = new TimerEvent();

    private void Awake()
    {
        fullBarTexture = new Texture2D(1, 1);
        emptyBarTexture = new Texture2D(1, 1);
        time = 0f;
        timer.enabled = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            //Debug.Log("Creating a Timer!");
            CreateTimer(10f);
        }
        if (timer.enabled)//only counts down if timer is enabled
        {
            //Debug.Log("Decreasing time!");
            if (time > 0)
            {
                //Debug.Log(time);
                time -= Time.deltaTime;
                timer.text = Mathf.Ceil(time) + " seconds left!";
            }
            else
            {
                time = 0;
                timer.enabled = false;
                TimerEnded.Invoke();//hey this event happened, tell the whole world!!!!
            }
        }
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
    }

    public void UpdateDisplays(float upvotes, float downvotes, float fame)
    {
        upvoteDisplay.text = "Upvotes: " + upvotes;
        downvoteDisplay.text = "Downvotes: " + downvotes;
        this.fame = fame;
    }

    public void CreateTimer(float timeToSet)//creates a timer(to be used for the objective system)
    {
        if (!timer.enabled)//only make a new timer if one isn't already active!
        {
            //Debug.Log("I should be working!");
            time = timeToSet;
            timer.enabled = true;
            Debug.Log(time + " " + timer.enabled);
        }
        else
        {
            Debug.LogError("Mah nigs, you got a timer already foo!");
        }
    }
}
