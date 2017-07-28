using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBluePrint {

    Type roomType; // type of room
    int trapChoices; // number of traps the blue print can choose from
    int objectiveChoices; // number of objectives the blue print can choose from
    int enemyChoices;

    public int numberoftraps = 0;// Number of traps in the room;
    public int numberofobjectives = 0;// number of objectives in the room;
    public int numberofEnemies = 0;

        // The base value of the trap ID
    // The base value of the objective IDs. Since the Locations Generated hold an ID this is required to locate the objectives in ther Objective Array in Game Board.

    int roomHeight;
    int roomWidth;

    int roomFloorMaxX;// The Width of the floor area;
    int roomFloorMaxY;// The Height of the floor area;

    Location exit; // location of the exit
    Location enter;// location of the enterance;
    Location deadend;// location of the  deadend if one is present;

    private List<Location> roomLocations = new List<Location>();// a list of all the locations of traps and objectives that need to be spawned in.

    public RoomBluePrint(Type RoomType,int TrapChoices,int ObjectiveChoices,int EnemyChoices, int RoomHeight,int RoomWidth,Location Exit, Location Entrance, Location Deadend)
    {
        roomType = RoomType;
        trapChoices = TrapChoices;
        objectiveChoices = ObjectiveChoices;
        enemyChoices = EnemyChoices;

        

        roomHeight = RoomHeight;
        roomWidth = RoomWidth;

        roomFloorMaxX = roomWidth - 1;
        roomFloorMaxY = roomHeight - 1;

        exit = Exit;
        enter = Entrance;
        deadend = Deadend;

    }


    public List<Location> GetBluePrint() // Returns Location list of all things that need to be spawned in the room;
    {


        int i = 0;
        Location locationholder;

        switch (roomType)
        {

            case Type.FirstRoom: // As of Now The First Room Is Empty



                break;


            case Type.Survive:// the player must kill enemies  spawning from indistructable objectives that spawn in the center of the room until time is up;

                numberoftraps = 6;
                numberofobjectives = 6;
                numberofEnemies = 6;

               // roomLocations.Add(new Location(2, 2, objectiveChoices - 1, Member.Objective));// Enemy Spawner Location
               // roomLocations.Add(new Location(2, roomFloorMaxY-1, objectiveChoices - 1, Member.Objective));// Enemy Spawner Location
               // roomLocations.Add(new Location(roomFloorMaxX-1,2, objectiveChoices - 1, Member.Objective));// Enemy Spawner Location
              //  roomLocations.Add(new Location(roomFloorMaxX-2, roomFloorMaxY-2, objectiveChoices - 1, Member.Objective));// Enemy Spawner Location


                i = 0;
                while(i<=numberoftraps)
                {
                    locationholder = RandomTrapLocation();
                    if(LocationAvalible(locationholder))
                    {
                        roomLocations.Add(locationholder);
                        i++;
                    }
                }

                i = 0;

                while (i <= numberofEnemies)
                {
                    locationholder = RandomEnemyLocation();
                    if (LocationAvalible(locationholder))
                    {
                        roomLocations.Add(locationholder);
                        i++;
                    }
                }
                
                break;

            case Type.Destroy:// adds objects that the player must destroy to move forward. The player must do this while avoiding enemies and traps;

                numberoftraps = Random.Range(1, 7); ;
                numberofobjectives = Random.Range(1,5);
                numberofEnemies = Random.Range(1, 7);

                i = 0;

                while (i <= numberofobjectives)
                {
                    locationholder = RandomObjectiveLocation();
                    if (LocationAvalible(locationholder))
                    {
                        roomLocations.Add(locationholder);
                        i++;
                    }
                }


                i = 0;

                while (i <= numberoftraps)
                {
                    locationholder = RandomTrapLocation();
                    if (LocationAvalible(locationholder))
                    {
                        roomLocations.Add(locationholder);
                        i++;
                    }

                }

                i = 0;
                while (i <= numberofEnemies)
                {
                    locationholder = RandomEnemyLocation();
                    if (LocationAvalible(locationholder))
                    {
                        roomLocations.Add(locationholder);
                        i++;
                    }
                }

                break;

            case Type.Genociede://Kill All The Enemies in the Room;

                numberoftraps = 6;
                numberofEnemies = 10;

                i = 0;
                while (i <= numberoftraps)
                {
                    locationholder = RandomTrapLocation();
                    if (LocationAvalible(locationholder))
                    {
                        roomLocations.Add(locationholder);
                        i++;
                    }

                }

                i = 0;

                while (i <= numberofEnemies)
                {
                    locationholder = RandomEnemyLocation();
                    if (LocationAvalible(locationholder))
                    {
                        roomLocations.Add(locationholder);
                        i++;
                    }
                }

                break;




            /*
        case Type.Kill:// NEEDS TO SPAWN A MINI BOSS at Objective Location;

            numberoftraps = 5;
            numberofEnemies=1;

            locationholder = new Location((roomHeight / 2), (roomWidth / 2), enemyChoices - 2, Member.Enemy);

            i = 0;
            while (i <= numberoftraps)
            {
                locationholder = RandomTrapLocation();
                if (LocationAvalible(locationholder))
                {
                    roomLocations.Add(locationholder);
                    i++;
                }

            }

            break;
            */

            case Type.ClearRoom:// fills the room with destructable objectives which the player will have to destroy to progress.

                
                numberofobjectives =15;

                int p = 0;

                for (int j =1; j<roomFloorMaxX;j++)
                {
                    for (int k = 1; k < roomFloorMaxY; k++)
                    {
                        locationholder = new Location(j, k, Random.Range(0,objectiveChoices-1),Member.Objective);
                        if (LocationAvalible(locationholder))
                        {
                            if (p <= numberofobjectives)
                            {
                                roomLocations.Add(locationholder);
                                p++;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }

                break;

            case Type.FinalRoom:// For The Final Boss;

                numberofEnemies = 1;

                locationholder = new Location((roomHeight / 2), (roomWidth / 2), enemyChoices-1,Member.Enemy);
                roomLocations.Add(locationholder);
                numberofobjectives++;

                break;

            default:
                break;

        }

        return roomLocations;
    }

    public bool LocationAvalible(Location locationQuery)
    {
        int j = locationQuery.x;
        int k = locationQuery.y;
        if (j == exit.x && k == exit.y || j == exit.x && k == exit.y + 1 || j == exit.x && k == exit.y - 1 || j == exit.x + 1 && k == exit.y || j == exit.x - 1 && k == exit.y)
        {
            return false;
        }
        else if (j == enter.x && k == enter.y  ||j == enter.x && k == enter.y + 1 || j == enter.x && k == enter.y - 1 || j == enter.x + 1 && k == enter.y || j == enter.x - 1 && k == enter.y)
        {
            return false;
        }
        else if (j == deadend.x && k == deadend.y || j == deadend.x && k == deadend.y + 1 || j == deadend.x && k == deadend.y - 1 || j == deadend.x + 1 && k == deadend.y || j == deadend.x - 1 && k == deadend.y)
        {
            return false;
        }
        else 
        {
            for(int i = 0;i<roomLocations.Count-1;i++)
            {
                if(locationQuery.x==roomLocations[i].x&& locationQuery.y == roomLocations[i].y)
                {
                    return false;
                }
            }

            return true;
        }

    }

    public Location RandomTrapLocation()
    {
        int trapX = Random.Range(1, roomFloorMaxX);
        int trapY = Random.Range(1, roomFloorMaxY);
        int trapID = Random.Range(0, trapChoices);

        return new Location(trapX, trapY, trapID, Member.Trap);
    }

    public Location RandomObjectiveLocation()
    {
        int objX = Random.Range(1, roomFloorMaxX - 1);
        int objY = Random.Range(1, roomFloorMaxY - 1);
        int objID = Random.Range(0,objectiveChoices-1);

        return new Location(objX, objY,objID,Member.Objective);
    }

    public Location RandomEnemyLocation()
    {
        int EnemX = Random.Range(1, roomFloorMaxX - 1);
        int EnemY = Random.Range(1, roomFloorMaxY - 1);
        int EnemID = Random.Range(0, enemyChoices-1);

        return new Location(EnemX, EnemY, EnemID, Member.Enemy);
    }

}
