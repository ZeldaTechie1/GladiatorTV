using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiController : MonoBehaviour {

    public Text upvoteDisplay;
    public Text downvoteDisplay;
    public Text fameDisplay;

    public void UpdateDisplays(float upvotes, float downvotes, float fame)
    {
        upvoteDisplay.text = "Upvotes: " + upvotes;
        downvoteDisplay.text = "Downvotes: " + downvotes;
        fameDisplay.text = "Fame: " + fame;
    }
}
