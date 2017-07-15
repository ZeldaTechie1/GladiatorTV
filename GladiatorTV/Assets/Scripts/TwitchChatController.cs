using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwitchChatController : MonoBehaviour {

    public float numVotes;
    public float upvotes;
    public float downvotes;
    public float fame;
    public GuiController guiController;

    private void Start()
    {
        numVotes = float.MaxValue - 5;
        upvotes = float.MaxValue - 5;
        downvotes = 0;
        fame = 0f;
    }

    public void TranslateMessage(string message)
    {
        
        if(message.IndexOf(" ") == -1)//there is only one word in this stupid message
        {
            ValidateCommand(message);
        }
        else
        {
            message = message.Substring(0,message.IndexOf(" "));//grab the first word in the message
        }

    }

    public void ValidateCommand(string command)
    {
        if(command == ":)")
        {
            upvotes++;
            numVotes++;
        }
        else if(command == ":(")
        {
            downvotes++;
            numVotes++;
        }
        fame = (upvotes / numVotes);
        Debug.Log("Fame " + fame);
        fame *= 100f;
        fame = Mathf.Round(fame);
        Debug.Log("Upvotes: " + upvotes);
        Debug.Log("Downvotes: " + downvotes);
        Debug.Log("Fame: " + fame + "%");
        guiController.UpdateDisplays(upvotes, downvotes, fame);
    }
}
