using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerPreferenceController : MonoBehaviour {
    
    [SerializeField]
    InputField upvoteInput;
    [SerializeField]
    InputField downvoteInput;
    [SerializeField]
    InputField OAuthToken;
    [SerializeField]
    InputField Username;
    [SerializeField]
    bool inPlayMode = false;

	// Use this for initialization
	void Start () {
       // PlayerPrefs.DeleteAll();
		if(inPlayMode)
        {
            this.gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	public void SaveInputs () {

        Debug.Log("Storing Authorization Tokens");
        if(OAuthToken.text == "")
        {
            Debug.Log("This is required, please generate a token!");
            return;
        }
        else
        {
            StoreToken(OAuthToken.text);
        }
        if (Username.text == "")
        {
            Debug.Log("This is required, please input your username!");
            return;
        }
        else
        {
            StoreUsername(Username.text);
        }
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
    void StoreToken(string token)
    {
        PlayerPrefs.SetString("Token",token);
    }
    void StoreUsername(string username)
    {
        PlayerPrefs.SetString("Username",username);
    }
}
