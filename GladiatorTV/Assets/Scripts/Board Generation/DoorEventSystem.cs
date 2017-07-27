using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEventSystem : MonoBehaviour {


    List<Location> RoomLocations;// List of Room Locations
    List<Location> DeadEndRooms;// List of Deadend Locations
    List<GameObject> Doors; 
    public GameBoard BOARD;// Game Board Reffrence

   public Room CurrentRoom;// Current Room Type
   public Type CurrentRoomType;// Type of the current room
   public Vector3 CurrentRoomsPos;

    RoomBluePrint CurrentRoomBluePrint;// Reffrence to the current Rooms Position.

    int currentObjectives;// The Number Objectives needed to be destroyed.
    int currentTraps;// The Current Number Traps.
    int currentEnemies;// The Current Number of Enemies;
    string roomFlavorText=null;// Flavor Text For the Room;

    float TimeRemaining=0;
    int firstRoomX;
    int firstRoomY;
    public GameObject camera;
    public RoomCamera roomcam;
    bool doorsOpen = false;


	// Use this for initialization
	void Start ()
    {
        Doors = new List<GameObject>();


        camera = GameObject.FindWithTag("MainCamera");
        //roomcam = camera.gameObject.GetComponent<RoomCamera>();
        RoomLocations = BOARD.roomLocation;
        DeadEndRooms = BOARD.deadEndLocation;


        CurrentRoom = BOARD.Board[RoomLocations[0].x][RoomLocations[0].y];
        CurrentRoomType = CurrentRoom.GetRoomType();
        CurrentRoomBluePrint = CurrentRoom.bluePrint;
        CurrentRoom.ObjectiveComplete = true;

        currentObjectives = CurrentRoomBluePrint.numberofobjectives;
        currentTraps = CurrentRoomBluePrint.numberoftraps;

        CurrentRoomsPos = CurrentRoom.roomCenter.transform.position;
       
        GetAllDoors();    
        ObjectiveCheck();      

        ChangeRoom(CurrentRoom);





    }
	// Update is called once per frame
	void Update () {
        if(!doorsOpen&&!CurrentRoom.ObjectiveComplete)
        {
            ObjectiveCheck();
        }
        

        if(CurrentRoomType==Type.Survive && !CurrentRoom.TimeIsUp)
        {
            TimeRemaining = -Time.deltaTime;
        }
		
	}

    public void ChangeRoom(Room room)
      {


        CurrentRoom = room;

        CurrentRoomType = CurrentRoom.GetRoomType();
        CurrentRoomBluePrint = CurrentRoom.bluePrint;

        currentObjectives = CurrentRoomBluePrint.numberofobjectives;
        currentTraps = CurrentRoomBluePrint.numberoftraps;

        TimeRemaining = CurrentRoom.SurviveTime;

        ObjectiveCheck();

        if(!CurrentRoom.ObjectiveComplete)
        {
            CloseDoors();
        }
        CurrentRoomsPos = CurrentRoom.roomCenter.transform.position;

        roomcam.CameraUpdate(CurrentRoomsPos);// <----------------------------------------------------------------ADD UPDATE CAMERA POSITION
        UpdateRoomFlavorText(CurrentRoom.flavorText);

       
    }

    public void ObjectiveCheck()
    { if (CurrentRoom.ObjectiveComplete)
        {
            switch (CurrentRoomType)
            {
                case Type.FirstRoom:
                    CurrentRoom.ObjectiveComplete = true;
                    OpenDoors();
                    break;

                case Type.Survive:
                    if(CheckTime())
                    {
                        CurrentRoom.ObjectiveComplete = true;
                        OpenDoors();
                    }
                    break;

                case Type.Destroy:
                    if(currentObjectives==0)
                    {
                        CurrentRoom.ObjectiveComplete = true;
                        OpenDoors();
                    }

                    break;

                case Type.Kill:

                    if (currentEnemies == 0)
                    {
                        CurrentRoom.ObjectiveComplete = true;
                        OpenDoors();
                    }

                    break;

                case Type.ClearRoom:
                    if (currentObjectives == 0)
                    {
                        CurrentRoom.ObjectiveComplete = true;
                        OpenDoors();
                    }

                    break;

                case Type.FinalRoom:

                    if (currentEnemies == 0)
                    {
                        CurrentRoom.ObjectiveComplete = true;
                        ENDGAME();
                    }

                    break;

                default:

                    break;
            }
        }
        

    }

    public bool CheckTime()
    {
        if(TimeRemaining==0)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public void OpenDoors()
    {
        Debug.Log("Doors: "+Doors.Count); 

        for (int i = 0; i < Doors.Count; i++)
        {
            if (!Doors[i].GetComponent<Doors>().TryOpenDoor())
            {
               
            }
            else
            {

            }

        }
        
    }

    public void CloseDoors()
    {
        for (int i = 0; i < Doors.Count; i++)
        {
            if (!Doors[i].gameObject.GetComponent<Doors>().TryCloseDoor())
            {
                
            }
            else
            {
                
            }
        }

    }

    public void ENDGAME()
    {

    }

    public void UpdateRoomFlavorText(string Flavatown)
    {
        roomFlavorText = Flavatown;
    }
    private void GetAllDoors()
    {
        Doors = new List<GameObject>();
        GameBoard board = gameObject.GetComponent<GameBoard>();
        GameObject door;


        for (int i =0; i< RoomLocations.Count;i++)
        {
            door = board.Board[RoomLocations[i].x][RoomLocations[i].y].exit;
            if(door!=null)
            {
                Doors.Add(door);
            }
            door = board.Board[RoomLocations[i].x][RoomLocations[i].y].entrance;
            if (door != null)
            {
                Doors.Add(door);
            }
            door = board.Board[RoomLocations[i].x][RoomLocations[i].y].deadend;
            if (door != null)
            {
                Doors.Add(door);
            }
        }

        for (int i = 0; i < DeadEndRooms.Count; i++)
        {
            door = board.Board[DeadEndRooms[i].x][DeadEndRooms[i].y].exit;
            if (door != null)
            {
                Doors.Add(door);
            }

            door = board.Board[DeadEndRooms[i].x][DeadEndRooms[i].y].entrance;
            if (door != null)
            {
                Doors.Add(door);
            }
            door = board.Board[DeadEndRooms[i].x][DeadEndRooms[i].y].deadend;
            if (door != null)
            {
                Doors.Add(door);
            }
        }

    }

    private Direction OppositeDirection(Direction dir)
    {
        switch (dir)
        {

            case Direction.North:
                return Direction.South;

            case Direction.East:
                return Direction.West;

            case Direction.South:
                return Direction.North;

            case Direction.West:
                return Direction.East;

            default:
                Debug.Log("Error: Not valid Direction. Default Direction set to North");
                return Direction.North;
        }
    }


}
