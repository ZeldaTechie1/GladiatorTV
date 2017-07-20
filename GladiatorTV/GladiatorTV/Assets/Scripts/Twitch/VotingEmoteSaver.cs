using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VotingEmoteSaver : MonoBehaviour {
    
    [SerializeField]
    InputField upvoteInput;
    [SerializeField]
    InputField downvoteInput;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public void SaveInputs () {

            Debug.Log("Storing upvotes: " + upvoteInput.text);
            StoreUpvotes(upvoteInput.text);
            Debug.Log("Storing downvotes: " + downvoteInput.text);
            StoreDownvotes(downvoteInput.text);

        }

    
    
    void StoreUpvotes(string upvotes)
    {
        PlayerPrefs.SetString("Upvotes", upvotes);
    }
    void StoreDownvotes(string downvotes)
    {
        PlayerPrefs.SetString("Downvotes",downvotes);
    }
}
