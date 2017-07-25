using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeFloor : MonoBehaviour {

    public int trapDamage;

    public float delayTime;
    public float activeTime;

    public bool triggered;
    public bool activated;

    public float counter;
    public int row = 0;

    private void Awake()
    {
        InvokeRepeating("incrementCounter", 0f, .25f);
        SetValues();
    }

    // Update is called once per frame
    void Update () {
		if(triggered)
        {
            CheckRow();
            activaterows();
        }
	}


    public void SetValues()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<SpikeRow>().activeTime = activeTime;
            child.GetComponent<SpikeRow>().delayTime = delayTime;
            child.GetComponent<SpikeRow>().trapDamage = trapDamage;
        }
    }

    private void activaterows()
    {
        int current_row = 0;
        foreach (Transform child in transform)
        {
            Debug.Log(row);
            Debug.Log(current_row);
            if(current_row == row)
            {
                child.GetComponent<SpikeRow>().triggered = true;
                if(current_row == 8)
                {
                    triggered = false;
                    row = 0;
                }
            }
            current_row++;

        }
    }

    private void CheckRow()
    {
        if(counter >= delayTime)
        {
            row++;
            counter = 0;
        }
    }

    private void incrementCounter()
    {
        if(triggered)
        {
            counter += .25f;
        }
    }
}
