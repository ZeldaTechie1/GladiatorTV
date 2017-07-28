using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Intro : MonoBehaviour {


    public AudioSource backgroundMusic;
    public AudioSource announcerIntro;
    public AudioSource oldPeopleIntro;

    public GameObject muteButton;

	// Use this for initialization
	void Start () {

        muteButton.SetActive(false);
        StartCoroutine(StartIntro());

	}
	
	IEnumerator StartIntro()
    {
        announcerIntro.Play();
        yield return new WaitForSeconds(49f);
        backgroundMusic.Stop();
        muteButton.SetActive(true);
        yield return new WaitForSeconds(5f);
        oldPeopleIntro.Play();
        yield return new WaitForSeconds(16f);
        sceneDone();

    }

    void sceneDone()
    {
        SceneManager.LoadScene(1);
    }
}
