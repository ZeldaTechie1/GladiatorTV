using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {
    public GameObject thiscanvas;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
        thiscanvas.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }
}
