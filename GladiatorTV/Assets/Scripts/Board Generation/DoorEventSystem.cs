using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEventSystem : MonoBehaviour {


    List<Location> RoomLocations;// List of Room Locations
    List<Location> DeadEndRooms;// List of Deadend Locations
    List<GameObject> AdjoinningEntrances; 
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


	// Use this for initialization
	void Start ()
    {
        AdjoinningEntrances = new List<GameObject>();


        camera = GameObject.FindWithTag("MainCamera");
        //roomcam = camera.gameObject.GetComponent<RoomCamera>();
        RoomLocations = BOARD.roomLocation;
        DeadEndRooms = BOARD.deadEndLocation;

        firstRoomX=RoomLocations[0].x;
        firstRoomY = RoomLocations[0].y;
        CurrentRoom = BOARD.Board[firstRoomX][firstRoomY];
        CurrentRoomType = CurrentRoom.GetRoomType();
        CurrentRoomBluePrint = CurrentRoom.bluePrint;
        CurrentRoom.ObjectiveComplete = true;

        currentObjectives = CurrentRoomBluePrint.numberofobjectives;
        currentTraps = CurrentRoomBluePrint.numberoftraps;

        CurrentRoomsPos = CurrentRoom.roomCenter.transform.position;

        GetAdjecentEntrances();       

        ChangeRoom(CurrentRoom);
        OpenDoors();





    }
	// Update is called once per frame
	void Update () {

        ObjectiveCheck();

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

      
        GetAdjecentEntrances();

        if(!CurrentRoom.ObjectiveComplete)
        {
            CloseDoors();
        }
        CurrentRoomsPos = CurrentRoom.roomCenter.transform.position;

        roomcam.CameraUpdate(CurrentRoomsPos);// <----------------------------------------------------------------ADD UPDATE CAMERA POSITION
        UpdateRoomFlavorText(CurrentRoom.flavorText);
    }

    public void ObjectiveCheck()
    { if (!CurrentRoom.ObjectiveComplete)
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
        for (int i = 0; i < CurrentRoom.DOORREFFRENCES.Count; i++)
        {
           if(!CurrentRoom.DOORREFFRENCES[i].GetComponent<Doors>().TryOpenDoor())
            {
                Debug.Log("Door was already open!");
            }
           else
            {
                Debug.Log("Door is open!");
            }
        }
        for (int i = 0; i < AdjoinningEntrances.Count; i++)
        {
            if (AdjoinningEntrances[i].GetComponent<Doors>().TryOpenDoor())
            {
                Debug.Log("Door was already open!");
            }
            else
            {
                Debug.Log("Door is open!");
            }

        }
        
    }

    public void OpenEntrances()
    {
        for (int i = 0; i < AdjoinningEntrances.Count; i++)
        {
            if (!AdjoinningEntrances[i].GetComponent<Doors>().TryOpenDoor())
            {
                Debug.Log("Door was already open!");
            }
            else
            {
                Debug.Log("Door is open!");
            }
        }

    }

    public void CloseDoors()
    {
        for (int i = 0; i < CurrentRoom.DOORREFFRENCES.Count; i++)
        {
            if (!CurrentRoom.DOORREFFRENCES[i].gameObject.GetComponent<Doors>().TryCloseDoor())
            {
                Debug.Log("Door was already closed!");
            }
            else
            {
                Debug.Log("Door is Closed!");
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
    private void GetAdjecentEntrances()
    {
        GameBoard board = gameObject.GetComponent<GameBoard>();
        GameObject door;
        Direction adjoiningdoor;
        Room Nextdoor;
        Location check;


        for(int i =0; i< CurrentRoom.AdjacentRooms.Count;i++)
        {
            

            check = CurrentRoom.AdjacentRooms[i];
            Debug.Log("X="+check.x +", Y="+check.y+" Room Number:"+i );

            if (true)
            { 
                Nextdoor = board.Board[check.x][check.y];
                if(check.y<CurrentRoom.myLocation.y)
                {
                    adjoiningdoor = Direction.North;
                }
                else if(check.y > CurrentRoom.myLocation.y)
                {
                    adjoiningdoor = Direction.South;
                }
                else if (check.x < CurrentRoom.myLocation.x)
                {
                    adjoiningdoor = Direction.West;
                }
                else 
                {
                    adjoiningdoor = Direction.East;
                }

                if(adjoiningdoor==Direction.Null)
                {
                    Debug.Log("Papas!!!");
                }

                if (Nextdoor.EXIT == adjoiningdoor)
                {
                    AdjoinningEntrances.Add(Nextdoor.exit);
                }
                else if (Nextdoor.ENTER == adjoiningdoor) 
                {
                    if(AdjoinningEntrances==null)
                

                    AdjoinningEntrances.Add(Nextdoor.entrance);
                }
                else if (Nextdoor.DEADEND == adjoiningdoor) 
                {
                    AdjoinningEntrances.Add(Nextdoor.deadend);
                }
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
