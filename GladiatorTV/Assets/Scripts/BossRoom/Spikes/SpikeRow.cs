using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeRow : MonoBehaviour {

    public int trapDamage;

    public float delayTime;
    public float activeTime;

    public bool triggered;
    public bool activated;

    public float counter;

    // Use this for initialization
    void Start () {
        SetValues();
	}
	
	// Update is called once per frame
	void Update () {
		if(triggered)
        {
            ActivateSpikes();
            triggered = false;
        }
	}

    public void ActivateSpikes()
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<BossSpike>().triggered == false && child.GetComponent<BossSpike>().activated == false)
                child.GetComponent<BossSpike>().triggered = true;
        }
    }

    public void SetValues()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<BossSpike>().activeTime = activeTime;
            child.GetComponent<BossSpike>().delayTime = delayTime;
            child.GetComponent<BossSpike>().trapDamage = trapDamage;
        }
    }
}
