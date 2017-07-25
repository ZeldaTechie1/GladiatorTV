using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEventSystem : MonoBehaviour {


    List<Location> RoomLocations;// List of Room Locations
    List<Location> DeadEndRooms;// List of Deadend Locations
    List<GameObject> AdjoinningEntrances; 
    GameBoard BOARD;// Game Board Reffrence

    Room CurrentRoom;// Current Room Type
    Type CurrentRoomType;// Type of the current room
    Vector3 CurrentRoomsPos;

    RoomBluePrint CurrentRoomBluePrint;// Reffrence to the current Rooms Position.

    int currentObjectives;// The Number Objectives needed to be destroyed.
    int currentTraps;// The Current Number Traps.
    int currentEnemies;// The Current Number of Enemies;

    List<GameObject> CurrentRoomsDoors;// current doors for the room
    string roomFlavorText=null;// Flavor Text For the Room;

    float TimeRemaining=0;


	// Use this for initialization
	void Start () {


        RoomLocations = BOARD.roomLocation;
        DeadEndRooms = BOARD.deadEndLocation;

        CurrentRoom = BOARD.Board[RoomLocations[0].x][RoomLocations[0].y];
        
        CurrentRoomType = CurrentRoom.GetRoomType();
        CurrentRoomBluePrint = CurrentRoom.bluePrint;

        currentObjectives = CurrentRoomBluePrint.numberofobjectives;
        currentTraps = CurrentRoomBluePrint.numberoftraps;

        CurrentRoom.GetDoors(CurrentRoomsDoors);


    }
	// Update is called once per frame
	void Update () {

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

        CurrentRoom.GetDoors(CurrentRoomsDoors);

        TimeRemaining = CurrentRoom.SurviveTime;

        if(!CurrentRoom.ObjectiveComplete)
        {
            CloseDoors();
        }

        // <----------------------------------------------------------------ADD UPDATE CAMERA POSITION
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
        for (int i = 0; i < CurrentRoomsDoors.Count; i++)
        {
           if(!CurrentRoomsDoors[i].GetComponent<Doors>().TryOpenDoor())
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
        for (int i = 0; i < CurrentRoomsDoors.Count; i++)
        {
            if (!CurrentRoomsDoors[i].gameObject.GetComponent<Doors>().TryCloseDoor())
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

            if (Nextdoor.EXIT == adjoiningdoor)
            {
                AdjoinningEntrances.Add(Nextdoor.exit);
            }
            else if (Nextdoor.ENTER == adjoiningdoor) 
            {
                AdjoinningEntrances.Add(Nextdoor.entrance);
            }
            else if (Nextdoor.DEADEND == adjoiningdoor) 
            {
                AdjoinningEntrances.Add(Nextdoor.deadend);
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
