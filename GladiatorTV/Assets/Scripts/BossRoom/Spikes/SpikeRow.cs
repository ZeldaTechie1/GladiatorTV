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

    public GameObject SpikePanel;
    public int panels = 9;
    // Use this for initialization
    void Start () {
        GeneratePanels();
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

    public void GeneratePanels()
    {
        float positionx = 0;
        float positiony = 0;
        for (int i = 0; i < 9; i++)
        {
            GameObject thisObject = Instantiate(SpikePanel, new Vector3(this.transform.localPosition.x + positionx, this.transform.localPosition.y + positiony, 0), Quaternion.identity);
            thisObject.transform.parent = this.transform;
            positionx += 64;
        }
    }
}
