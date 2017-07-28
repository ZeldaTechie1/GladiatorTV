using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour {

    public BoxCollider2D doorBox; // The box collider attached to the door;
    public DoorEventSystem system;
    bool doorOpen = false;
    Room parentroom;
    [SerializeField]
    GameObject gameBoard;

	// Use this for initialization
	void Start () {

        gameBoard = GameObject.FindWithTag("BOARD");
        system = gameBoard.GetComponent(typeof (DoorEventSystem)) as DoorEventSystem;


    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool GetDoorStatus()
    {
        return doorOpen;
    }


    public void TryOpenDoor()
    {
       
            OpenDoor();
    }

    public void TryCloseDoor()
    {
        CloseDoor();

    }

    public void SetRoom(Room room)
    {
        parentroom = room;
    }

    public void RoomTriggered()
    {   
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
