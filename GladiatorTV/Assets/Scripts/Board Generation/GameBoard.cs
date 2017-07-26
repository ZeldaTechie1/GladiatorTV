
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Tiles
{
    Floor, Wall, Crowd, Door, Trap, Enemy,
}

public struct Location
{
    public int x;
    public int y;
    public int id;
    public Member member;

    public Location(int X, int Y)
    {
        x = X;
        y = Y;
        id = 0;
        member = Member.NULL;

    }

    public Location(int X, int Y, int ID, Member MEM)
    {
        x = X;
        y = Y;
        id = ID;
        member = MEM;

    }
}


public class GameBoard : MonoBehaviour
{


    public GameObject[] Walls;
    public GameObject[] Floor;
    public GameObject[] Doors;
    public GameObject[] Traps;
    public GameObject[] Objectives;
    public GameObject[] Enemies;

    public EnemyManager EnemyManage;


    public int numberOfRooms = 3;
    public int tileSize = 64;

    public int numberOfDeadend;

    public int deadendRoomsPrecentage = 25;// determines the precentage chance a room spawns a dead end room.

    public int roomHeight = 11;// Height of the room
    public int roomWidth = 11;// Width of the room

    public int boardHeight = 100; // Height of the board
    public int boardWidth = 100;// Width of the board

    public Room[][] Board;
    public GameObject boardHolder;




    private Vector3 Origin;
    public List<Location> roomLocation = new List<Location>();// Location For all the rooms in the Board Array. The Members and id of these are NULL because only there X and Y coordinates are needed   
    public List<Location> deadEndLocation = new List<Location>();// Location For all the Deadend rooms in the Board Array. The Members and id of these are NULL because only there X and Y coordinates are needed   
    public List<GameObject> roomDoors = new List<GameObject>();

    void Awake()
    {

        
        EnemyManage = gameObject.GetComponent<EnemyManager>();

        boardHolder = new GameObject("BoardHolder");

        tileSize = 64;


        Board = new Room[boardWidth][];

        // Go through all the Board Arrays...
        for (int j = 0; j < Board.Length; j++)
        {
            // ... and set each Board array is the correct number of rows.
            Board[j] = new Room[boardHeight];

            for (int k = 0; k < Board[j].Length; k++)
            {
                Board[j][k] = null;
            }
        }
        Origin = boardHolder.transform.position;// Orign for the Game Board.

        PopulateBoard();//Fills the game board with rooms



        LinkRooms();

    }

    private void PopulateBoard()
    {
        roomLocation = new List<Location>();
        deadEndLocation= new List<Location>();


        Direction enterance = Direction.South;
        Direction exit = Direction.North;

        bool final = true;

        Location roomIndex;


        int boardX = boardHeight / 2;// Starts the Room population in the center of the game board.
        int boardY = boardWidth / 2;

        Vector3 populatePosition = new Vector3(0, 0, 0);



        for (int i = 0; i < numberOfRooms; i++)
        {
            Board[boardX][boardY] = new Room();



            GameObject parent = new GameObject("Room" + (i + 1));

            parent.transform.SetParent(boardHolder.transform);// Makes Sure that the Parent Transform of all of the rooms is the same;

            enterance = OppisiteDirection(exit);// Sets the entrance value opposite to the last rooms exit

            populatePosition.x = Origin.x + boardX * (roomWidth * tileSize); // Location of rooms location on the X axis in reffrence to the Game Board;
            populatePosition.y = Origin.y + boardY * (roomHeight * tileSize);// Location of rooms location on the Y axis in reffrence to the Game Board;

            parent.transform.SetPositionAndRotation(populatePosition, Quaternion.identity);

            if (i == 0)
            {


                roomIndex.x = boardX;
                roomIndex.y = boardY;
                roomIndex.id = 0;
                roomIndex.member = Member.NULL;
                roomLocation.Add(roomIndex); // Adds Room locations to list for later use.

                Board[boardX][boardY].SetupRoom(tileSize, roomWidth, roomHeight, exit, parent, roomIndex);
                BuildRoom(Board[boardX][boardY], populatePosition, false); //  buildRoom(Room room, Vector3 origin)



                boardY++; // Since The exit is always north for the first room the Y position is always increased
            }
            else if (i == numberOfRooms - 1)
            {

                roomIndex.x = boardX;
                roomIndex.y = boardY;
                roomIndex.id = 0;
                roomIndex.member = Member.NULL;
                roomLocation.Add(roomIndex); // Adds Room locations to list for later use.

                Board[boardX][boardY].SetupRoom(tileSize, roomWidth, roomHeight, enterance, final, parent, roomIndex);
                BuildRoom(Board[boardX][boardY], populatePosition, false);
            }

            else
            {
                bool Deadend = GetIsDeadEnd();

                bool okDirection = false;

                roomIndex.x = boardX;
                roomIndex.y = boardY;
                roomIndex.id = 0;
                roomIndex.member = Member.NULL;
                roomLocation.Add(roomIndex); // Adds Room locations to list for later use.

                while (!okDirection)
                {
                    Direction test = RandomDirection();
                    Direction deadend;// direction for dead end room;

                    if (test != enterance)// Test To see if the next room would be overlapping or out of range.
                    {
                        try
                        {
                            switch (test)
                            {

                                case Direction.North:

                                    if (boardY < boardHeight && Board[boardX][boardY + 1] == null)
                                    {
                                        // Debug.LogError("North");

                                        exit = test;
                                        okDirection = true;
                                        if (Deadend)// if ther is a dead end ...
                                        {
                                            deadend = DeadEnd(exit, enterance, boardX, boardY);

                                            if (deadend != Direction.Null)// ...and the generateed direction isn't null
                                            {
                                                Board[boardX][boardY].SetupRoom(tileSize, roomWidth, roomHeight, enterance, exit, deadend, parent, roomIndex);// set up room
                                                BuildDeadEnd(deadend, boardX, boardY);
                                            }
                                            else
                                            {
                                                Board[boardX][boardY].SetupRoom(tileSize, roomWidth, roomHeight, enterance, exit, parent, roomIndex);
                                            }

                                        }
                                        else
                                        {
                                            Board[boardX][boardY].SetupRoom(tileSize, roomWidth, roomHeight, enterance, exit, parent, roomIndex);
                                        }

                                        BuildRoom(Board[boardX][boardY], populatePosition, false);
                                        boardY++;
                                    }
                                    break;

                                case Direction.East:

                                    if (boardX < boardWidth && Board[boardX + 1][boardY] == null)
                                    {

                                        //Debug.LogError("East");

                                        exit = test;
                                        okDirection = true;
                                        if (Deadend)
                                        {
                                            deadend = DeadEnd(exit, enterance, boardX, boardY);

                                            if (deadend != Direction.Null)
                                            {
                                                Board[boardX][boardY].SetupRoom(tileSize, roomWidth, roomHeight, enterance, exit, deadend, parent, roomIndex);
                                                BuildDeadEnd(deadend, boardX, boardY);
                                            }
                                            else
                                            {
                                                Board[boardX][boardY].SetupRoom(tileSize, roomWidth, roomHeight, enterance, exit, parent, roomIndex);
                                            }

                                        }
                                        else
                                        {
                                            Board[boardX][boardY].SetupRoom(tileSize, roomWidth, roomHeight, enterance, exit, parent, roomIndex);
                                        }

                                        BuildRoom(Board[boardX][boardY], populatePosition, false);


                                        boardX++;
                                    }
                                    break;

                                case Direction.South:
                                    if (boardY > 0 && Board[boardX][boardY - 1] == null)
                                    {
                                        exit = test;
                                        okDirection = true;
                                        if (Deadend)
                                        {
                                            deadend = DeadEnd(exit, enterance, boardX, boardY);

                                            if (deadend != Direction.Null)
                                            {
                                                Board[boardX][boardY].SetupRoom(tileSize, roomWidth, roomHeight, enterance, exit, deadend, parent, roomIndex);
                                                BuildDeadEnd(deadend, boardX, boardY);
                                            }
                                            else
                                            {
                                                Board[boardX][boardY].SetupRoom(tileSize, roomWidth, roomHeight, enterance, exit, parent, roomIndex);
                                            }

                                        }
                                        else
                                        {
                                            Board[boardX][boardY].SetupRoom(tileSize, roomWidth, roomHeight, enterance, exit, parent, roomIndex);
                                        }

                                        BuildRoom(Board[boardX][boardY], populatePosition, false);

                                        boardY--;
                                    }
                                    break;

                                case Direction.West:

                                    if (boardX > 0 && Board[boardX - 1][boardY] == null)
                                    {
                                        exit = test;
                                        okDirection = true;
                                        if (Deadend)
                                        {
                                            deadend = DeadEnd(exit, enterance, boardX, boardY);

                                            if (deadend != Direction.Null)
                                            {
                                                Board[boardX][boardY].SetupRoom(tileSize, roomWidth, roomHeight, enterance, exit, deadend, parent, roomIndex);
                                                BuildDeadEnd(deadend, boardX, boardY);
                                            }
                                            else
                                            {
                                                Board[boardX][boardY].SetupRoom(tileSize, roomWidth, roomHeight, enterance, exit, parent, roomIndex);
                                            }

                                        }
                                        else
                                        {
                                            Board[boardX][boardY].SetupRoom(tileSize, roomWidth, roomHeight, enterance, exit, parent, roomIndex);
                                        }

                                        BuildRoom(Board[boardX][boardY], populatePosition, false);
                                        boardX--;
                                    }
                                    break;

                                default:
                                    Debug.Log("Error: Not valid Direction");

                                    break;
                            }
                        }
                        catch
                        {
                            Debug.LogError("Error: Unable to Populate Room " + (1 + i));
                            okDirection = false;
                        }
                    }
                }

            }

        }

    }

    private void BuildRoom(Room room, Vector3 origin, bool isDeadEnd)
    {
        Tiles[][] tiles = room.GetRoomTiles();
        GameObject Parent = room.GetParent();
        GameObject doorholder;
        BoxCollider2D doortrigger;
        Doors doorscript;
        room.SetRoomDifficulty(RoomDifficulty());


        Vector3 newPosition = new Vector3(0, 0, 0);

        GameObject currentTile;

        // j represents the X value of the transform positon
        for (int j = 0; j < tiles.Length; j++)
        {
            newPosition.x = origin.x + (j * (tileSize));
            // k represents the Y value of the transform positon
            for (int k = 0; k < tiles[j].Length; k++)
            {
                newPosition.y = origin.y + (k * (tileSize));
                Location localhold = new Location(j, k);

                switch (tiles[j][k])
                {

                    case Tiles.Wall:

                        newPosition.z = 0.25f;
                        currentTile = Walls[0];
                        Instantiate(currentTile, newPosition, Quaternion.identity).transform.SetParent(Parent.transform);
                        break;
                    case Tiles.Floor:
                        currentTile = Floor[0];
                        if (j == roomWidth / 2 && k == roomHeight / 2)
                        {
                            room.roomCenter = Instantiate(currentTile, newPosition, Quaternion.identity);
                            room.roomCenter.transform.SetParent(Parent.transform);
                        }

                        else
                        {
                            Instantiate(currentTile, newPosition, Quaternion.identity).transform.SetParent(Parent.transform);
                        }
                       

                        break;

                    case Tiles.Door:
                        
                        currentTile = Doors[0];
                        doorholder = Instantiate(currentTile, newPosition, Quaternion.identity);
                        doorscript = doorholder.GetComponent<Doors>();
                        doorholder.transform.SetParent(Parent.transform);
                        if (room.GetExit().x == localhold.x && room.GetExit().y == localhold.y) // moves the room enter trigger to infront of the doors.
                        {
                            room.SetExit(doorholder);

                            switch (room.EXIT)
                            {
                                case Direction.North:

                                    doortrigger = doorholder.transform.Find("DoorTrigger").transform.GetComponent<BoxCollider2D>();
                                    doortrigger.offset = new Vector2(0, -16);

                                   
                                    doorscript.SetRoom(room);


                                    break;
                                case Direction.East:


                                    doortrigger = doorholder.transform.Find("DoorTrigger").transform.GetComponent<BoxCollider2D>();
                                    doortrigger.offset = new Vector2(-16, 0);

                                    doorscript.SetRoom(room);

                                    break;

                                case Direction.South:


                                    doortrigger = doorholder.transform.Find("DoorTrigger").transform.GetComponent<BoxCollider2D>();
                                    doortrigger.offset = new Vector2(0, +16);

                                    doorscript.SetRoom(room);

                                    break;

                                case Direction.West:


                                    doortrigger = doorholder.transform.Find("DoorTrigger").transform.GetComponent<BoxCollider2D>();
                                    doortrigger.offset = new Vector2(+16, 0);

                                    doorscript.SetRoom(room);


                                    break;

                            }

                        }
                        else if (room.GetEntrance().x == localhold.x && room.GetEntrance().y == localhold.y)
                        {
                            room.SetEnter(doorholder);
                            switch (room.ENTER)
                            {
                                case Direction.North:

                                    doortrigger = doorholder.transform.Find("DoorTrigger").transform.GetComponent<BoxCollider2D>();
                                    doortrigger.offset = new Vector2(0, -16);

                                    doorscript.SetRoom(room);


                                    break;
                                case Direction.East:


                                    doortrigger = doorholder.transform.Find("DoorTrigger").transform.GetComponent<BoxCollider2D>();
                                    doortrigger.offset = new Vector2(-16, 0);


                                    doorscript.SetRoom(room);

                                    break;

                                case Direction.South:


                                    doortrigger = doorholder.transform.Find("DoorTrigger").transform.GetComponent<BoxCollider2D>();
                                    doortrigger.offset = new Vector2(0, +16);


                                    doorscript.SetRoom(room);

                                    break;

                                case Direction.West:


                                    doortrigger = doorholder.transform.Find("DoorTrigger").transform.GetComponent<BoxCollider2D>();
                                    doortrigger.offset = new Vector2(+16, 0);

                                    doorscript.SetRoom(room);



                                    break;

                            }

                        }
                        else if (room.GetDeadEnd().x == localhold.x && room.GetDeadEnd().y == localhold.y)
                        {

                            room.SetDeadend(doorholder);
                            switch (room.DEADEND)
                            {
                                case Direction.North:

                                    doortrigger = doorholder.transform.Find("DoorTrigger").transform.GetComponent<BoxCollider2D>();
                                    doortrigger.offset = new Vector2(0, -16);


                                    doorscript.SetRoom(room);


                                    break;
                                case Direction.East:


                                    doortrigger = doorholder.transform.Find("DoorTrigger").transform.GetComponent<BoxCollider2D>();
                                    doortrigger.offset = new Vector2(-16, 0);


                                    doorscript.SetRoom(room);

                                    break;

                                case Direction.South:


                                    doortrigger = doorholder.transform.Find("DoorTrigger").transform.GetComponent<BoxCollider2D>();
                                    doortrigger.offset = new Vector2(0, +16);

                                    doorscript.SetRoom(room);

                                    break;

                                case Direction.West:


                                    doortrigger = doorholder.transform.Find("DoorTrigger").transform.GetComponent<BoxCollider2D>();
                                    doortrigger.offset = new Vector2(+16, 0);

                                    doorscript.SetRoom(room);


                                    break;
                            }

                        }

                        break;

                    default:
                        break;
                }
            }
        }

        PopulateRoom(room, origin, isDeadEnd);

    }

    private void PopulateRoom(Room currentRoom, Vector3 origin, bool isDeadEnd)
    {

        int objectiveIDBase = Traps.Length;// The base value of the objective IDs. Since the Locations Generated hold an ID this is required to locate the objectives in ther Objective Array in Game Board.

        currentRoom.ObstacleSetup(Traps.Length, Objectives.Length, Enemies.Length);

        Location[][] Obstacles = currentRoom.GetObstacles();
        GameObject Parent = new GameObject("Obstacles");

        Parent.transform.SetParent(currentRoom.GetParent().transform);

        Vector3 newPosition = new Vector3(0, 0, 0);

        GameObject currentObstacle;

        for (int j = 0; j < roomWidth; j++)
        {
            newPosition.x = origin.x + (j * (tileSize));
            // k represents the Y value of the transform positon
            for (int k = 0; k < roomHeight; k++)
            {
                newPosition.y = origin.y + (k * (tileSize));

                if (Obstacles[j][k].member != Member.NULL)
                {
                    if (Obstacles[j][k].member == Member.Objective)
                    {
                        currentObstacle = Objectives[Obstacles[j][k].id];
                        Instantiate(currentObstacle, newPosition, Quaternion.identity).transform.SetParent(Parent.transform);
                    }
                    else if (Obstacles[j][k].member == Member.Trap)
                    {
                        currentObstacle = Traps[Obstacles[j][k].id];
                        Instantiate(currentObstacle, newPosition, Quaternion.identity).transform.SetParent(Parent.transform);
                    }

                    else if (Obstacles[j][k].member == Member.Enemy)
                    {
                        currentObstacle = Enemies[Obstacles[j][k].id];
                        EnemyManage.SpawnEnemy(currentRoom.Difficulty, newPosition);
                        

                    }
                }

            }
        }

    }




    private Direction RandomDirection()
    {
        int dir = Random.Range(0, 4);

        switch (dir)
        {

            case 0:
                return Direction.North;
            case 1:
                return Direction.East;
            case 2:
                return Direction.South;
            case 3:
                return Direction.West;

            default:
                Debug.Log("Error: Not valid Direction. Default Direction set to North");
                return Direction.North;
        }
    }

    private Direction OppisiteDirection(Direction dir)
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

    private Direction DeadEnd(Direction exit, Direction entrance, int boardX, int boardY)
    {
        if ((boardY < boardHeight && Board[boardX][boardY + 1] == null && exit != Direction.North && entrance != Direction.North))
        {
            return Direction.North;
        }
        else if ((boardX < boardHeight && Board[boardX + 1][boardY] == null && exit != Direction.East && entrance != Direction.East))
        {
            return Direction.East;
        }
        else if ((boardY > 0 && Board[boardX][boardY - 1] == null && exit != Direction.South && entrance != Direction.South))
        {
            return Direction.South;
        }

        else if ((boardX > 0 && Board[boardX - 1][boardY] == null && exit != Direction.West && entrance != Direction.West))
        {
            return Direction.West;
        }

        return Direction.Null;
    }

    private bool GetIsDeadEnd()
    {
        int choice = Random.Range(1, 100);

        return choice < (deadendRoomsPrecentage);
    }

    private void BuildDeadEnd(Direction deadend, int boardX, int boardY)
    {
        Vector3 position = new Vector3(0, 0, 0);// Origin of the dead end room

        GameObject parent = new GameObject("Dead End Room" + numberOfDeadend);
        numberOfDeadend++;

        Location roomIndex = new Location(boardX, boardY);

        parent.transform.SetParent(boardHolder.transform);

        try
        {
            switch (deadend)
            {
                case Direction.North:

                    boardY++;

                    Board[boardX][boardY] = new Room();



                    Board[boardX][boardY].SetupRoom(tileSize, roomWidth, roomHeight, OppisiteDirection(deadend), false, parent, roomIndex);

                    position.x = Origin.x + boardX * (roomWidth * tileSize); // Location of rooms location on the X axis in reffrence to the Game Board;
                    position.y = Origin.y + boardY * (roomHeight * tileSize);// Location of rooms location on the y axis in reffrence to the Game Board;

                    BuildRoom(Board[boardX][boardY], position, true);

                    break;

                case Direction.East:
                    boardX++;

                    Board[boardX][boardY] = new Room();

                    Board[boardX][boardY].SetupRoom(tileSize, roomWidth, roomHeight, OppisiteDirection(deadend), false, parent, roomIndex);

                    position.x = Origin.x + boardX * (roomWidth * tileSize); // Location of rooms location on the X axis in reffrence to the Game Board;
                    position.y = Origin.y + boardY * (roomHeight * tileSize);// Location of rooms location on the y axis in reffrence to the Game Board;

                    BuildRoom(Board[boardX][boardY], position, true);

                    break;

                case Direction.South:
                    boardY--;

                    Board[boardX][boardY] = new Room();

                    Board[boardX][boardY].SetupRoom(tileSize, roomWidth, roomHeight, OppisiteDirection(deadend), false, parent, roomIndex);

                    position.x = Origin.x + boardX * (roomWidth * tileSize); // Location of rooms location on the X axis in reffrence to the Game Board;
                    position.y = Origin.y + boardY * (roomHeight * tileSize);// Location of rooms location on the y axis in reffrence to the Game Board;

                    BuildRoom(Board[boardX][boardY], position, true);

                    break;

                case Direction.West:

                    boardX--;

                    Board[boardX][boardY] = new Room();

                    Board[boardX][boardY].SetupRoom(tileSize, roomWidth, roomHeight, OppisiteDirection(deadend), false, parent, roomIndex);

                    position.x = Origin.x + boardX * (roomWidth * tileSize); // Location of rooms location on the X axis in reffrence to the Game Board;
                    position.y = Origin.y + boardY * (roomHeight * tileSize);// Location of rooms location on the y axis in reffrence to the Game Board;

                    BuildRoom(Board[boardX][boardY], position, true);
                    break;
            }

        }
        catch
        {

        }
    }

    private int RoomDifficulty()
    {
        TwitchChatController TCC = FindObjectOfType<TwitchChatController>();

        if (TCC.fame < 25)
        {
            return 0;
        }

        else if (TCC.fame < 75)
        {
            return 1;
        }

        else
        {
            return 2;
        }

    }

    private void LinkRooms()
    {
        Location check;
        Room room,roomcheck;


        for(int i =0; i<roomLocation.Count;i++)
        {

            check = roomLocation[i];
            room = Board[check.x][check.y];
            if(room.EXIT!=Direction.Null)
            {

                if(i==0)
                {
                    
                }

                switch (room.EXIT)
                {
                            
                    case Direction.North:
                        roomcheck = Board[(check.x)][(check.y + 1)];
                        if (OppisiteDirection(room.EXIT) == roomcheck.ENTER)
                        {
                            room.AddAdjacentRoom(roomcheck.myLocation);

                        }
                        break;

                    case Direction.East:
                        roomcheck = Board[(check.x + 1)][(check.y)];
                        if (OppisiteDirection(room.EXIT) == roomcheck.ENTER)
                        {
                            room.AddAdjacentRoom(roomcheck.myLocation);


                        }
                        break;

                    case Direction.South:

                        roomcheck = Board[(check.x)][(check.y - 1)];
                        if (OppisiteDirection(room.EXIT) == roomcheck.ENTER)
                        {

                            room.AddAdjacentRoom(roomcheck.myLocation);


                        }
                        break;

                    case Direction.West:
                        roomcheck = Board[(check.x - 1)][(check.y)];
                        if (OppisiteDirection(room.EXIT) == roomcheck.ENTER)
                        {
                            room.AddAdjacentRoom(roomcheck.myLocation);


                        }
                        break;

                    default:

                        break;
                }
            }

            if (room.ENTER!= Direction.Null)
            {

                switch (room.ENTER)
                {

                    case Direction.North:
                        roomcheck = Board[(check.x)][(check.y + 1)];
                        if (OppisiteDirection(room.ENTER) == roomcheck.EXIT || OppisiteDirection(room.ENTER) == roomcheck.DEADEND)
                        {
                            room.AddAdjacentRoom(roomcheck.myLocation);


                        }
                        break;

                    case Direction.East:
                        roomcheck = Board[(check.x + 1)][(check.y)];
                        if (OppisiteDirection(room.ENTER) == roomcheck.EXIT || OppisiteDirection(room.ENTER) == roomcheck.DEADEND)
                        {
                            room.AddAdjacentRoom(roomcheck.myLocation);

                        }
                        break;

                    case Direction.South:

                        roomcheck = Board[(check.x)][(check.y - 1)];
                        if (OppisiteDirection(room.ENTER) == roomcheck.EXIT || OppisiteDirection(room.ENTER) == roomcheck.DEADEND)
                        {

                            room.AddAdjacentRoom(roomcheck.myLocation);


                        }
                        break;

                    case Direction.West:
                        roomcheck = Board[(check.x - 1)][(check.y)];
                        if (OppisiteDirection(room.ENTER) == roomcheck.EXIT || OppisiteDirection(room.ENTER) == roomcheck.DEADEND)
                        {
                            room.AddAdjacentRoom(roomcheck.myLocation);


                        }
                        break;

                    default:

                        break;
                }
            }

            if (room.DEADEND != Direction.Null)
            {
    
                switch (room.DEADEND)
                {
                    case Direction.North:
                        roomcheck = Board[(check.x)][(check.y + 1)];
                        if (OppisiteDirection(room.DEADEND) == roomcheck.ENTER)
                        {
                            room.AddAdjacentRoom(roomcheck.myLocation);

                        }
                        break;

                    case Direction.East:
                        roomcheck = Board[(check.x + 1)][(check.y)];
                        if (OppisiteDirection(room.DEADEND) == roomcheck.ENTER)
                        {
                            room.AddAdjacentRoom(roomcheck.myLocation);

                        }
                        break;

                    case Direction.South:

                        roomcheck = Board[(check.x)][(check.y - 1)];
                        if (OppisiteDirection(room.DEADEND) == roomcheck.ENTER)
                        {

                            room.AddAdjacentRoom(roomcheck.myLocation);

                        }
                        break;

                    case Direction.West:
                        roomcheck = Board[(check.x - 1)][(check.y)];
                        if (OppisiteDirection(room.DEADEND) == roomcheck.ENTER)
                        {
                            room.AddAdjacentRoom(roomcheck.myLocation);

                        }
                        break;

                    default:

                        break;
                 }   
               }
        }

       /* for (int i = 0; i < deadEndLocation.Count; i++)
        {

            check = deadEndLocation[i];
            room = Board[check.x][check.y];
            switch (room.ENTER)
            {

                case Direction.North:
                    roomcheck = Board[(check.x)][(check.y + 1)];
                    if (OppisiteDirection(room.ENTER) == roomcheck.DEADEND)
                    {
                        room.AddAdjacentRoom(roomcheck.myLocation);
                    }
                    break;

                case Direction.East:
                    roomcheck = Board[(check.x + 1)][(check.y)];
                    if (OppisiteDirection(room.ENTER) == roomcheck.DEADEND)
                    {
                        room.AddAdjacentRoom(roomcheck.myLocation);
                    }
                    break;

                case Direction.South:

                    roomcheck = Board[(check.x)][(check.y - 1)];
                    if (OppisiteDirection(room.ENTER) == roomcheck.DEADEND)
                    {

                        room.AddAdjacentRoom(roomcheck.myLocation);
                    }
                    break;

                case Direction.West:
                    roomcheck = Board[(check.x - 1)][(check.y)];
                    if (OppisiteDirection(room.ENTER) == roomcheck.DEADEND)
                    {
                        room.AddAdjacentRoom(roomcheck.myLocation);
                    }
                    break;

                default:

                    break;

            }
            
        }*/

    }


    // Update is called once per frame
    void Update()
    {



    }
}
