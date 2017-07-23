using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour {

    BoxCollider2D doorBox; // The box collider attached to the door;
    bool doorOpen = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool GetDoorStatus()
    {
        return doorOpen;
    }


    public bool TryOpenDoor()
    {
        if(!doorOpen)// if the door is unlocked the door opened 
        {
            OpenDoor();
            return true;
        }

        else
        {
            return false;
        }
    }
    public bool TryCloseDoor()
    {
        CloseDoor();
        return true;
    }

    private void OpenDoor()
    {
        doorBox.enabled = false;
        doorOpen = true;
    }
    private void CloseDoor()
    {
        doorBox.enabled = true;
        doorOpen = false;
    }
}
