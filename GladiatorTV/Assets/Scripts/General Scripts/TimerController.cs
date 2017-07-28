using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour {

    //Timer stuff
    [SerializeField]
    Text timer;
    float time;
    public class TimerEvent : UnityEngine.Events.UnityEvent { };
    public TimerEvent TimerEnded = new TimerEvent();

    // Use this for initialization
    void Start () {
        time = 0f;
        timer.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        
        /*if (Input.GetButtonDown("Fire2"))
        {
            //Debug.Log("Creating a Timer!");
            CreateTimer(10f);
        }*/
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
    public void CreateTimer(float timeToSet)//creates a timer(to be used for the objective system)
    {
        if (!timer.enabled)//only make a new timer if one isn't already active!
        {
            //Debug.Log("I should be working!");
            time = timeToSet;
            timer.enabled = true;
            //Debug.Log(time + " " + timer.enabled);
        }
        else
        {
            Debug.LogError("Mah nigs, you got a timer already foo!");
        }
    }
}
