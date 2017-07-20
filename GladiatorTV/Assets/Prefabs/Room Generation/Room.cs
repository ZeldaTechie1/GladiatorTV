using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    North=0, East=1, South=2, West=3, Null,
}

public class Room{

    private  Tiles[][] tiles;
    private int exitX = 0;
    private int exitY = 0;
    private int enterX = 0;
    private int enterY = 0;
    private int deadendX = 0;
    private int deadendY = 0;

    private int roomWidth; 
    private int roomHeight;
    private int TileSize;
    private GameObject Parent;


    // Overloaded Method for making a room with only an exit 
    public void SetupRoom(int tileSize,int width, int height, Direction exit, GameObject parent)
    {
        Parent = parent;
        TileSize =tileSize;
        roomWidth=width;
        roomHeight=height;
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
                exitX= width/2;
                exitY = height-1;                 
                break;
            case Direction.East:
                exitX = width -1;
                exitY = height/2;
                break;

            case Direction.South:
                exitX = width /2;
                exitY = 0;
                break;

            case Direction.West:
                exitX = 0;
                exitY = height / 2;
                break;
        }

        for (int j = 0; j < tiles.Length; j++)
        {
            for (int k = 0; k < tiles[j].Length; k++)
            {

                if(j==exitX && k==exitY )
                {
                    tiles[j][k] = Tiles.Door;
                }

               else if(j==0||j==width-1)
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
    public void SetupRoom(int tileSize, int width, int height, Direction enterance, Direction exit, GameObject parent)
    {
        Parent = parent;
        TileSize = tileSize;
        roomWidth = width;
        roomHeight = height;

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
                break;
            case Direction.East:
                exitX = width - 1;
                exitY = height / 2;
                break;

            case Direction.South:
                exitX = width / 2;
                exitY = 0;
                break;

            case Direction.West:
                exitX = 0;
                exitY = height / 2;
                break;
        }

        switch (enterance)
        {
            case Direction.North:
                enterX = width / 2;
                enterY = height - 1;
                break;
            case Direction.East:
                enterX = width - 1;
                enterY = height / 2;
                break;

            case Direction.South:
                enterX = width / 2;
                enterY = 0;
                break;

            case Direction.West:
                enterX = 0;
                enterY = height / 2;
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
    public void SetupRoom(int tileSize, int width, int height, Direction enterance,bool finalRoom, GameObject parent)
    {
        Parent = parent;
        TileSize = tileSize;
        roomWidth = width;
        roomHeight = height;

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
                break;
            case Direction.East:
                enterX = width - 1;
                enterY = height / 2;
                break;

            case Direction.South:
                enterX = width / 2;
                enterY = 0;
                break;

            case Direction.West:
                enterX = 0;
                enterY = height / 2;
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
    public void SetupRoom(int tileSize, int width, int height, Direction enterance, Direction exit, Direction Deadend, GameObject parent)
    {
        Parent = parent;
        TileSize = tileSize;
        roomWidth = width;
        roomHeight = height;

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
                break;
            case Direction.East:
                exitX = width - 1;
                exitY = height / 2;
                break;

            case Direction.South:
                exitX = width / 2;
                exitY = 0;
                break;

            case Direction.West:
                exitX = 0;
                exitY = height / 2;
                break;
        }

        switch (enterance)
        {
            case Direction.North:
                enterX = width / 2;
                enterY = height - 1;
                break;
            case Direction.East:
                enterX = width - 1;
                enterY = height / 2;
                break;

            case Direction.South:
                enterX = width / 2;
                enterY = 0;
                break;

            case Direction.West:
                enterX = 0;
                enterY = height / 2;
                break;
        }

        switch (Deadend)
        {
            case Direction.North:
                deadendX = width / 2;
                deadendY = height - 1;
                break;
            case Direction.East:
                deadendX = width - 1;
                deadendY = height / 2;
                break;

            case Direction.South:
                deadendX = width / 2;
                deadendY = 0;
                break;

            case Direction.West:
                deadendX = 0;
                deadendY = height / 2;
                break;
        }

        Debug.LogError("Exit X " + exitX + " ; Y " + exitY);
        Debug.LogError("Enter X " + enterX + " ; Y " + enterY);
        Debug.LogError("Deadend X " + deadendX + " ; Y " + deadendY);

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

    public Location getExit()
    {
        Location Exit=new Location(exitX, exitY);

        return Exit;
    }

    public Location getEntrance()
    {
        Location Entrance = new Location(enterX, enterY);

        return Entrance;
    }

    public Tiles[][] getRoomTiles()
    {
        return tiles;
    }

    public GameObject getParent()
    {
        return Parent;
    }
		
	
}
