using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour {

    BoxCollider2D doorBox; // The box collider attached to the door;
    bool doorOpen = false;
    Room parentroom;

	// Use this for initialization
	void Start () {

        doorBox = gameObject.transform.GetComponent<BoxCollider2D>();

		
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

    public void SetRoom(Room room)
    {
        parentroom = room;
    }

    public void RoomTriggered()
    {
        DoorEventSystem system;
        system = GetComponentInParent<DoorEventSystem>();
        system.ChangeRoom(parentroom);
    }

    private void OpenDoor()
    {
        doorBox.enabled = false;
        doorOpen = true;
 
        UpdateAnimation();
    }
    private void CloseDoor()
    {
        doorBox.enabled = true;
        doorOpen = false;
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {

    }


}
