using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Intro : MonoBehaviour {

    
    public AudioSource announcerIntro;
    public AudioSource oldPeopleIntro;

	// Use this for initialization
	void Start () {
		
	}
	
	

    void sceneDone()
    {
        SceneManager.LoadScene(1);
    }
}
