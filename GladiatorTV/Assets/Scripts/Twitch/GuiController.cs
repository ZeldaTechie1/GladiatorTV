﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

<<<<<<< HEAD
public class GuiController : MonoBehaviour
{

    //Gui info
=======
public class GuiController : MonoBehaviour {

>>>>>>> Room_Generation
    public Text upvoteDisplay;
    public Text downvoteDisplay;
    public Text fameDisplay;
    float fame = 0f;
<<<<<<< HEAD

    //Gui stuff
    Vector2 fameBarPosition = new Vector2(80, 540);
    Vector2 fameBarSize = new Vector2(100, 20);
=======
    Vector2 pos = new Vector2(80, 540);
    Vector2 size = new Vector2(100,20);
>>>>>>> Room_Generation
    Texture2D emptyBarTexture;
    Texture2D fullBarTexture;
    GUIStyle fullBarStyle = new GUIStyle();
    GUIStyle emptyBarStyle = new GUIStyle();
    Color barrFillColor = Color.green;
    Color emptyBarrFillColor = Color.red;

<<<<<<< HEAD
    //Timer stuff
    [SerializeField]
    Text timer;
    float time;
    public class TimerEvent : UnityEngine.Events.UnityEvent { };
    TimerEvent TimerEnded = new TimerEvent();

=======
>>>>>>> Room_Generation
    private void Awake()
    {
        fullBarTexture = new Texture2D(1, 1);
        emptyBarTexture = new Texture2D(1, 1);
<<<<<<< HEAD
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
=======
>>>>>>> Room_Generation
    }

    private void OnGUI()
    {
        // draw the background:
        emptyBarTexture.SetPixel(0, 0, emptyBarrFillColor);
        emptyBarTexture.Apply();
        emptyBarStyle.normal.background = emptyBarTexture;
<<<<<<< HEAD
        GUI.BeginGroup(new Rect(fameBarPosition.x, fameBarPosition.y, fameBarSize.x, fameBarSize.y));
        GUI.Box(new Rect(0, 0, fameBarSize.x, fameBarSize.y), new GUIContent(""), emptyBarStyle);
=======
        GUI.BeginGroup(new Rect(pos.x, pos.y, size.x, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), new GUIContent(""), emptyBarStyle);
>>>>>>> Room_Generation

        // draw the filled-in part:
        fullBarTexture.SetPixel(0, 0, barrFillColor);
        fullBarTexture.Apply();
        fullBarStyle.normal.background = fullBarTexture;
<<<<<<< HEAD
        GUI.BeginGroup(new Rect(0, 0, fameBarSize.x * (fame / 100f), fameBarSize.y));
        GUI.Box(new Rect(0, 0, fameBarSize.x, fameBarSize.y), new GUIContent(""), fullBarStyle);
=======
        GUI.BeginGroup(new Rect(0, 0, size.x * (fame/100f), size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), new GUIContent(""),fullBarStyle);
>>>>>>> Room_Generation
        GUI.EndGroup();

        GUI.EndGroup();
    }

    public void UpdateDisplays(float upvotes, float downvotes, float fame)
    {
        upvoteDisplay.text = "Upvotes: " + upvotes;
        downvoteDisplay.text = "Downvotes: " + downvotes;
        this.fame = fame;
    }
<<<<<<< HEAD

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
=======
>>>>>>> Room_Generation
}
