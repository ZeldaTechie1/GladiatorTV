using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwitchChatController : MonoBehaviour
{

    public float numVotes;
    public float upvotes;
    public float downvotes;
    public float fame;
    public GuiController guiController;
    Dictionary<string, int> validCommands = new Dictionary<string, int>();//upvotes take an int of 1, downvotes take an int of -1

    private void Start()
    {
        numVotes = 0;
        upvotes = 0;
        downvotes = 0;
        fame = 0f;
        //RasterizeVotingSystem();
    }

    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Insert))//if left control or left mouse button is pressed
        {
            RasterizeVotingSystem();
        }
    }*/

    public void TranslateMessage(string message)
    {

        if (message.IndexOf(" ") == -1)//there is only one word in this stupid message
        {
            ValidateCommand(message);
        }
        else
        {
            message = message.Substring(0, message.IndexOf(" "));//grab the first word in the message
            ValidateCommand(message);
        }

    }

    public void ValidateCommand(string command)
    {
        Debug.Log("Validating command: " + command);
        int test;
        validCommands.TryGetValue(command, out test);
        Debug.Log(test);
        if (test == 1 || test == -1)//command is valid
        {
            Debug.Log(command + " is a valid command!");
            if (validCommands[command] == 1)
            {
                Debug.Log("That Command was an upvote!");
                upvotes++;
                numVotes++;
            }
            else if (validCommands[command] == -1)
            {
                Debug.Log("That Command was a downvote!");
                downvotes++;
                numVotes++;
            }
        }
        /*if(command == ":)")
        {
            upvotes++;
            numVotes++;
        }
        else if(command == ":(")
        {
            downvotes++;
            numVotes++;
        }*/
        fame = (upvotes / numVotes);
        //Debug.Log("Fame " + fame);
        fame *= 100f;
        fame = Mathf.Round(fame);
        /*Debug.Log("Upvotes: " + upvotes);
        Debug.Log("Downvotes: " + downvotes);
        Debug.Log("Fame: " + fame + "%");*/
        guiController.UpdateDisplays(upvotes, downvotes, fame);
    }

    public void RasterizeVotingSystem()//applies upvotes and downvotes based on player prefs, defaults are :) and :( respectively
    {
        //Debug.Log("Rasterizing Voting System");

        validCommands.Clear();//just make sure the commands are empty
        if (PlayerPrefs.HasKey("Upvotes"))//the upvotes have been set
        {
            int commandCount = 0;
            Debug.Log("Upvotes have been saved, importing now.");
            string upvotes = PlayerPrefs.GetString("Upvotes");//grabs the upvotes stored in the system
            string[] upvoteArray = upvotes.Split(new char[] { ',', ' ' });//separate the string based on commas and spaces
            foreach (string up in upvoteArray)//apply all the upvotes in the valid commands
            {
                if (up != "" && up != " ")
                {
                    validCommands.Add(up, 1);//make it an upvote if it is not empty or a space
                    commandCount++;
                }

            }
            if (commandCount == 0)//there were no valid commands in the string provide, set defaults
            {
                Debug.Log("Default Upvotes being saved.");
                validCommands.Add(":)", 1);
            }
        }
        else
        {
            Debug.Log("Default Upvotes being saved.");
            validCommands.Add(":)", 1);
        }
        if (PlayerPrefs.HasKey("Downvotes"))//the downvotes have been set
        {
            int commandCount = 0;
            Debug.Log("Downvotes have been saved, importing now.");
            string downvotes = PlayerPrefs.GetString("Downvotes");//grabs the downvotes stored in the system
            string[] downvoteArray = downvotes.Split(new char[] { ',', ' ' });//separate the string based on commas and spaces
            //Debug.Log(downvotes);
            foreach (string down in downvoteArray)//apply all the downvotes in the valid commands
            {
                if (down != "" && down != " ")
                {
                    validCommands.Add(down, -1);//make it a downvote if it is not empty or a space
                    commandCount++;
                }

            }
            if (commandCount == 0)//there were no valid commands in the string provide, set defaults
            {
                Debug.Log("Default Downvotes being saved.");
                validCommands.Add(":(", -1);
            }
        }
        else
        {
            Debug.Log("Default Downvotes being saved.");
            validCommands.Add(":(", -1);
        }

    }
}
