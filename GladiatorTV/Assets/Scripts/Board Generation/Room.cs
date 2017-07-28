using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    North, East, South, West, Null,
}

public enum Type// The Rooms Type. This Signifies What The objective of the room will be.
{
    FirstRoom, Survive, Destroy, Kill, Genociede ,ClearRoom, FinalRoom,
}
public enum Member// Member of the a room Either an Enemy, Objective , Trap OR NULL which is used if This field does not need to be tracked
{
    Enemy, Objective, Trap, NULL
}
public class Room
{

    private Tiles[][] tiles; // Tile Types
    private Location[][] obstacles;// Locations of all the obstacles in the room

    private int Trapchoices; /// Traps To chose from 
    private int Objectivechoices;/// Objective Blocks to choose from
    private int Enemychoices;

    private int exitX = 0;
    private int exitY = 0;
    private int enterX = 0;
    private int enterY = 0;
    private int deadendX = 0;
    private int deadendY = 0;

    private Location deadendLocation; // location of the deadend in the room
    private Location enterLocation; // location of the entrance location
    private Location exitLocation;// location of the exit.
    public Location myLocation;

    private Type type; // the type of room being generated

    private GameObject Parent;// Refrence to the parent transform
    public GameObject exit = null; // reffrence to the Exit Door Game object
    public GameObject entrance = null; // reffrence to the Entrance Door Game object
    public GameObject deadend = null; // reffrence to the Dead End Door Game object

    public GameObject roomCenter;

    public List<GameObject> DOORREFFRENCES;

    private int roomWidth; // width of the room
    private int roomHeight;// Height of the room
    private int TileSize;// The Size of the floor and wall tiles

    private List<Location> roomLocations;// Locations of all the obstacles that need to be placed.
    public List<Location> AdjacentRooms;

    public RoomBluePrint bluePrint;// refrence to the RoomBluePrint specific to this room;

    public bool ObjectiveComplete = false;// Keeps Track of the rooms objective;
    public bool TimeIsUp = false; // Keeps Track of room time for Survival type;
    public float SurviveTime = 10; // The Amount of time that will elapse in a Survival room
    public int Difficulty;

    public Direction EXIT = Direction.Null;
    public Direction ENTER = Direction.Null;
    public Direction DEADEND = Direction.Null;

    public Vector3 origin;
    public string flavaTown;

    // Overloaded Method for making a room with only an exit 
    public void SetupRoom(int tileSize, int width, int height, Direction exit, GameObject parent, Location ThisRoom)
    {
        //flavorText = "BOI IM SPICY";

        AdjacentRooms = new List<Location>();
        DOORREFFRENCES = new List<GameObject>();

        ObjectiveComplete = true;

        myLocation = ThisRoom;

        EXIT = exit;

        Parent = parent;
        TileSize = tileSize;
        roomWidth = width;
        roomHeight = height;

        type = Type.FirstRoom;

        tiles = new Tiles[width][];

        // Go through all the tile arrays...
        for (int i = 0; i < tiles.Length; i++)
        {
            // ... and set each tile array is the correct number of rows.
            tiles[i] = new Tiles[height];
        }


        switch (exit)
        {
            case Direction.North:
                exitX = width / 2;
                exitY = height - 1;
                exitLocation = new Location(exitX, exitY);
                AddAdjacentRoom(new Location(myLocation.x, myLocation.y + 1));

                break;
            case Direction.East:
                exitX = width - 1;
                exitY = height / 2;
                exitLocation = new Location(exitX, exitY);

                AddAdjacentRoom(new Location(myLocation.x + 1, myLocation.y));

                break;

            case Direction.South:
                exitX = width / 2;
                exitY = 0;
                exitLocation = new Location(exitX, exitY);
                AddAdjacentRoom(new Location(myLocation.x, myLocation.y - 1));
                break;

            case Direction.West:
                exitX = 0;
                exitY = height / 2;
                exitLocation = new Location(exitX, exitY);
                AddAdjacentRoom(new Location(myLocation.x - 1, myLocation.y));
                break;
        }

        for (int j = 0; j < tiles.Length; j++)
        {
            for (int k = 0; k < tiles[j].Length; k++)
            {

                if (j == exitX && k == exitY)
                {
                    tiles[j][k] = Tiles.Door;
                }

                else if (j == 0 || j == width - 1)
                {
                    tiles[j][k] = Tiles.Wall;
                }

                else if (k == 0 || k == height - 1)
                {
                    tiles[j][k] = Tiles.Wall;
                }

                else
                {
                    tiles[j][k] = Tiles.Floor;
                }

            }
        }


    }

    // Overloaded Method for making a room with an exit and an entrance
    public void SetupRoom(int tileSize, int width, int height, Direction enterance, Direction exit, GameObject parent, Location ThisRoom)
    {
        AdjacentRooms = new List<Location>();
        myLocation = ThisRoom;

        DOORREFFRENCES = new List<GameObject>();
        EXIT = exit;
        ENTER = enterance;


        Parent = parent;
        TileSize = tileSize;
        roomWidth = width;
        roomHeight = height;

        int typechoice = Random.Range(1, 5);

        switch (typechoice)
        {
            case 1:
                type = Type.Survive;
                break;
            case 2:
                type = Type.Destroy;
                break;
            case 3:
                type = Type.Kill;
                break;
            case 4:
                type = Type.ClearRoom;
                break;
        }


        tiles = new Tiles[width][];

        // Go through all the tile arrays...
        for (int i = 0; i < tiles.Length; i++)
        {
            // ... and set each tile array is the correct number of rows.
            tiles[i] = new Tiles[height];
        }

        switch (exit)
        {
            case Direction.North:
                exitX = width / 2;
                exitY = height - 1;
                exitLocation = new Location(exitX, exitY);
                AddAdjacentRoom(new Location(myLocation.x, myLocation.y + 1));

                break;
            case Direction.East:
                exitX = width - 1;
                exitY = height / 2;
                exitLocation = new Location(exitX, exitY);

                AddAdjacentRoom(new Location(myLocation.x + 1, myLocation.y));

                break;

            case Direction.South:
                exitX = width / 2;
                exitY = 0;
                exitLocation = new Location(exitX, exitY);
                AddAdjacentRoom(new Location(myLocation.x, myLocation.y - 1));
                break;

            case Direction.West:
                exitX = 0;
                exitY = height / 2;
                exitLocation = new Location(exitX, exitY);
                AddAdjacentRoom(new Location(myLocation.x - 1, myLocation.y));
                break;
        }

        switch (enterance)
        {
            case Direction.North:
                enterX = width / 2;
                enterY = height - 1;
                enterLocation = new Location(enterX, enterY);
                AddAdjacentRoom(new Location(myLocation.x, myLocation.y + 1));
                break;
            case Direction.East:
                enterX = width - 1;
                enterY = height / 2;
                enterLocation = new Location(enterX, enterY);
                AddAdjacentRoom(new Location(myLocation.x + 1, myLocation.y));
                break;

            case Direction.South:
                enterX = width / 2;
                enterY = 0;
                enterLocation = new Location(enterX, enterY);
                AddAdjacentRoom(new Location(myLocation.x, myLocation.y - 1));
                break;

            case Direction.West:
                enterX = 0;
                enterY = height / 2;
                enterLocation = new Location(enterX, enterY);
                AddAdjacentRoom(new Location(myLocation.x - 1, myLocation.y));
                break;
        }

        for (int j = 0; j < tiles.Length; j++)
        {
            for (int k = 0; k < tiles[j].Length; k++)
            {

                if (j == exitX && k == exitY) // checks to see if the tile is on the exit
                {
                    tiles[j][k] = Tiles.Door;
                }

                else if (j == enterX && k == enterY) // checks to see if the tile is on the entrance
                {
                    tiles[j][k] = Tiles.Door;
                }

                else if (j == 0 || j == width - 1) // Top or bottom of the array
                {
                    tiles[j][k] = Tiles.Wall;
                }

                else if (k == 0 || k == height - 1) // Left or right of the the array
                {
                    tiles[j][k] = Tiles.Wall;
                }

                else
                {
                    tiles[j][k] = Tiles.Floor;
                }

            }
        }


    }

    // Overloaded Method for making a room with only an entrance
    public void SetupRoom(int tileSize, int width, int height, Direction enterance, bool finalRoom, GameObject parent, Location ThisRoom)
    {
        AdjacentRooms = new List<Location>();
        myLocation = ThisRoom;

        DOORREFFRENCES = new List<GameObject>();
        ENTER = enterance;
        Parent = parent;
        TileSize = tileSize;
        roomWidth = width;
        roomHeight = height;

        if (finalRoom)
        {
            type = Type.FinalRoom;
        }
        else
        {
            int typechoice = Random.Range(1, 5);
            switch (typechoice)
            {
                case 1:
                    type = Type.Destroy;
                    break;
                case 2:
                    type = Type.Destroy;
                    break;
                case 3:
                    type =Type.Genociede;
                    break;
                case 4:
                    type = Type.ClearRoom;
                    break;
            }
        }


        tiles = new Tiles[width][];

        // Go through all the tile arrays...
        for (int i = 0; i < tiles.Length; i++)
        {
            // ... and set each tile array is the correct number of rows.
            tiles[i] = new Tiles[height];
        }

        switch (enterance)
        {
            case Direction.North:
                enterX = width / 2;
                enterY = height - 1;
                enterLocation = new Location(enterX, enterY);
                AddAdjacentRoom(new Location(myLocation.x, myLocation.y + 1));
                break;
            case Direction.East:
                enterX = width - 1;
                enterY = height / 2;
                enterLocation = new Location(enterX, enterY);
                AddAdjacentRoom(new Location(myLocation.x + 1, myLocation.y));
                break;

            case Direction.South:
                enterX = width / 2;
                enterY = 0;
                enterLocation = new Location(enterX, enterY);
                AddAdjacentRoom(new Location(myLocation.x, myLocation.y - 1));
                break;

            case Direction.West:
                enterX = 0;
                enterY = height / 2;
                enterLocation = new Location(enterX, enterY);
                AddAdjacentRoom(new Location(myLocation.x - 1, myLocation.y));
                break;
        }

        for (int j = 0; j < tiles.Length; j++)
        {
            for (int k = 0; k < tiles[j].Length; k++)
            {
                if (j == enterX && k == enterY) // checks to see if the tile is on the entrance
                {
                    tiles[j][k] = Tiles.Door;
                }

                else if (j == 0 || j == width - 1) // Top or bottom of the array
                {
                    tiles[j][k] = Tiles.Wall;
                }

                else if (k == 0 || k == height - 1) // Left or right of the the array
                {
                    tiles[j][k] = Tiles.Wall;
                }

                else
                {
                    tiles[j][k] = Tiles.Floor;
                }

            }
        }


    }

    //Overloaded Method for Making a room with dead ends;
    public void SetupRoom(int tileSize, int width, int height, Direction enterance, Direction exit, Direction Deadend, GameObject parent, Location ThisRoom)
    {
        AdjacentRooms = new List<Location>();
        DOORREFFRENCES = new List<GameObject>();

        myLocation = ThisRoom;

        EXIT = exit;
        ENTER = enterance;
        DEADEND = Deadend;

        Parent = parent;
        TileSize = tileSize;
        roomWidth = width;
        roomHeight = height;

        tiles = new Tiles[width][];

        int typechoice = Random.Range(1, 5);

        switch (typechoice)
        {
            case 1:
                type = Type.Destroy;
                break;
            case 2:
                type = Type.Destroy;
                break;
            case 3:
                type = Type.Genociede;
                break;
            case 4:
                type = Type.ClearRoom;
                break;
            default:
                type = Type.Destroy;
               
                break;
        }


        // Go through all the tile arrays...
        for (int i = 0; i < tiles.Length; i++)
        {
            // ... and set each tile array is the correct number of rows.
            tiles[i] = new Tiles[height];
        }


        switch (exit)
        {
            case Direction.North:
                exitX = width / 2;
                exitY = height - 1;
                exitLocation = new Location(exitX, exitY);
                AddAdjacentRoom(new Location(myLocation.x, myLocation.y + 1));

                break;
            case Direction.East:
                exitX = width - 1;
                exitY = height / 2;
                exitLocation = new Location(exitX, exitY);

                AddAdjacentRoom(new Location(myLocation.x + 1, myLocation.y));

                break;

            case Direction.South:
                exitX = width / 2;
                exitY = 0;
                exitLocation = new Location(exitX, exitY);
                AddAdjacentRoom(new Location(myLocation.x, myLocation.y - 1));
                break;

            case Direction.West:
                exitX = 0;
                exitY = height / 2;
                exitLocation = new Location(exitX, exitY);
                AddAdjacentRoom(new Location(myLocation.x - 1, myLocation.y));
                break;
        }

        switch (enterance)
        {
            case Direction.North:
                enterX = width / 2;
                enterY = height - 1;
                enterLocation = new Location(enterX, enterY);
                AddAdjacentRoom(new Location(myLocation.x, myLocation.y + 1));
                break;
            case Direction.East:
                enterX = width - 1;
                enterY = height / 2;
                enterLocation = new Location(enterX, enterY);
                AddAdjacentRoom(new Location(myLocation.x + 1, myLocation.y));
                break;

            case Direction.South:
                enterX = width / 2;
                enterY = 0;
                enterLocation = new Location(enterX, enterY);
                AddAdjacentRoom(new Location(myLocation.x, myLocation.y - 1));
                break;

            case Direction.West:
                enterX = 0;
                enterY = height / 2;
                enterLocation = new Location(enterX, enterY);
                AddAdjacentRoom(new Location(myLocation.x - 1, myLocation.y));
                break;
        }

        switch (Deadend)
        {
            case Direction.North:
                deadendX = width / 2;
                deadendY = height - 1;
                deadendLocation = new Location(deadendX, deadendY);
                AddAdjacentRoom(new Location(myLocation.x, myLocation.y + 1));
                break;

            case Direction.East:
                deadendX = width - 1;
                deadendY = height / 2;
                deadendLocation = new Location(deadendX, deadendY);
                AddAdjacentRoom(new Location(myLocation.x + 1, myLocation.y));
                break;

            case Direction.South:
                deadendX = width / 2;
                deadendY = 0;
                deadendLocation = new Location(deadendX, deadendY);
                AddAdjacentRoom(new Location(myLocation.x, myLocation.y - 1));
                break;

            case Direction.West:
                deadendX = 0;
                deadendY = height / 2;
                deadendLocation = new Location(deadendX, deadendY);
                AddAdjacentRoom(new Location(myLocation.x - 1, myLocation.y));
                break;
        }



        for (int j = 0; j < tiles.Length; j++)
        {
            for (int k = 0; k < tiles[j].Length; k++)
            {
                if (j == exitX && k == exitY) // checks to see if the tile is on the exit
                {
                    tiles[j][k] = Tiles.Door;
                }
                else if (j == enterX && k == enterY) // checks to see if the tile is on the entrance
                {
                    tiles[j][k] = Tiles.Door;
                }
                else if (j == deadendX && k == deadendY) // checks to see if the tile is on the exit
                {
                    tiles[j][k] = Tiles.Door;
                }

                else if (j == 0 || j == width - 1) // Top or bottom of the array
                {
                    tiles[j][k] = Tiles.Wall;
                }

                else if (k == 0 || k == height - 1) // Left or right of the the array
                {
                    tiles[j][k] = Tiles.Wall;
                }

                else
                {
                    tiles[j][k] = Tiles.Floor;
                }

            }
        }
    }

    public Type GetRoomType()// Returns the type of the room
    {
        return type;
    }
    public GameObject GetParent()// Returns the Parent Transform
    {
        return Parent;
    }

    public Location GetExit()//Returns the Exit location in refrence to the room
    {
        Location Exit = new Location(exitX, exitY);

        return Exit;
    }

    public Location GetEntrance()//Returns The Entrance location in refrence to the room
    {
        Location Entrance = new Location(enterX, enterY);

        return Entrance;
    }

    public Location GetDeadEnd()//Returns The DeadEnd location in refrence to the room
    {

        Location deadend = new Location(deadendX, deadendY);

        return deadend;

    }

    public Tiles[][] GetRoomTiles()// returns an array of tile types for the base room
    {
        return tiles;
    }

    public Location[][] GetObstacles()// returns an array of obstacle locations
    {
        return obstacles;
    }

    public void ObstacleSetup(int TrapChoices, int ObjectiveChoices, int EnemyChoices)//Sets up the room locations list
    {
        Trapchoices = TrapChoices;
        Objectivechoices = ObjectiveChoices;
        Enemychoices = EnemyChoices;


        bluePrint = new RoomBluePrint(type, Trapchoices, Objectivechoices, Enemychoices, roomHeight, roomWidth, exitLocation, enterLocation, deadendLocation);
        roomLocations = bluePrint.GetBluePrint();

        SetFlavorText();

        obstacles = new Location[roomWidth][];// Array of Traps

        // Go through all the tile arrays...
        for (int i = 0; i < roomHeight; i++)
        {
            // ... and set each tile array is the correct number of rows.
            obstacles[i] = new Location[roomHeight];
            for (int l = 0; l < roomHeight; l++)
            {
                obstacles[i][l] = new Location(i, l);
            }
        }

        for (int j = 0; j < roomWidth; j++)
        {
            for (int k = 0; k < roomHeight; k++)
            {
                for (int i = 0; i < roomLocations.Count; i++)
                {
                    if (j == roomLocations[i].x && k == roomLocations[i].y)
                    {
                        obstacles[j][k] = roomLocations[i];
                    }
                }

            }
        }

    }

    public void SetExit(GameObject Exit)
    {
        exit =Exit;
        DOORREFFRENCES.Add(Exit);
    }
    public void SetEnter(GameObject enter)
    {
        entrance = enter;
        DOORREFFRENCES.Add(enter);
    }
    public void SetDeadend(GameObject Dead)
    {
        deadend = Dead;
        DOORREFFRENCES.Add(Dead);
    }


   

    public RoomBluePrint GetBluePrint()// Returns the rooms reffrence to its blue print
    {
        return bluePrint;
    }

    public void AddAdjacentRoom(Location Roomlocal)
    {
        if(AdjacentRooms==null)
        {
            
        }
        
        AdjacentRooms.Add(Roomlocal);

    }

    public void SetRoomDifficulty(int dif)
    {
        Difficulty = dif;
    }

    public void SetFlavorText()
    {
        Type thisRoomType = GetRoomType();
        switch (thisRoomType)
        {
            case Type.Kill:

                if (Random.Range(0, 2) == 0)
                { flavaTown = "TRY NOT TO FINISH FIRST ;)"; }
                else
                { flavaTown = "STAY ALIVE WHILE WE GO TO COMMERCIAL !"; }
                break;

            case Type.Survive:
                if (Random.Range(0, 2) == 0)
                { flavaTown = "TRY NOT TO FINISH FIRST ;)"; }
                else
                { flavaTown = "STAY ALIVE WHILE WE GO TO COMMERCIAL !"; }
                break;

            case Type.Genociede:
                if (Random.Range(0, 2) == 0)
                { flavaTown = "EVERY RUN IS A GENOCIDE RUN HERE BABY!"; }
                else
                { flavaTown = "PUTTING THE LAUGHTER IN MANSLAUGHTER"; }
                break;

            case Type.Destroy:

                if (Random.Range(0, 2) == 0)
                { flavaTown = "SMASH TO PASS... IF YOU KNOW WHAT I MEAN ;)"; }
                else
                { flavaTown ="YOU BREAK IT YOU BOUGHT IT !"; }
                break;

            case Type.ClearRoom:

                if (Random.Range(0, 2) == 0)
                { flavaTown = "YOU JUST GOT COMMUNITY SERVICE FOR BEING A VERY BAD BOY!\n OR GIRL, WHATEVER!"; }
                else
                { flavaTown = "ITS TIME TO TAKE OUT THE TRASH, LOOKS LIKE YOU FIT RIGHT IN..."; }


                break;
            case Type.FinalRoom:

                if (Random.Range(0, 2) == 0)
                { flavaTown =  "I'D BEAT MYSELF BUT THEY'D TAKE ME OFF THE AIR!"; }
                else
                { flavaTown = "KILLING YOU WILL BE GREAT FOR RATTINGS!"; }

                break;
        }

    }

}
