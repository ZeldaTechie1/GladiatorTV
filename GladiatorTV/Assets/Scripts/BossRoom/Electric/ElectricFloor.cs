using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricFloor : MonoBehaviour {
    public GameObject electric;
    public int traps;
    public int[] trapPositions;
    public int trapPos;

    public bool activate;
	// Use this for initialization
	void Start () {
        //GenerateFloor();
        trapPositions = new int[traps];
	}
	
	// Update is called once per frame
	void Update () {
		if(activate)
        {
            ActivateTraps();
            activate = false;
        }
	}

    public void GenerateFloor()
    {
        float positionx = 0;
        float positiony = 0;
        for(int i = 0; i < 9; i++)
        {
            for(int j = 0; j < 9; j++)
            {
                GameObject Object = Instantiate(electric, new Vector3(this.transform.localPosition.x + positionx, this.transform.localPosition.y + positiony, 0), Quaternion.identity);
                positionx += 64;
            }
            positionx = 0;
            positiony -= 64;
        }
    }

    void ActivateTraps()
    {
        for(int i = 0; i < traps; i++)
        {
            trapPos = (int)Random.Range(0, 80);

            int currentposition = 0;
            foreach (Transform child in transform)
            {
                if(currentposition == trapPos)
                {
                    child.GetComponent<Electric>().active = true;
                    break;
                }
                else
                {
                    currentposition++;
                }

            }
        }


    }
}
