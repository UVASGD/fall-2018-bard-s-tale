using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    public int columns = 9;
    public int rows = 9;
    public bool north, south, east, west;
    public GameObject NDoor, SDoor, EDoor, WDoor;
    public GameObject[] floorTiles;
    public GameObject wallTile;

    private Transform roomHolder;
    private List<Vector2> gridPositions = new List<Vector2>();

    void InitializeList()
    {
        gridPositions.Clear();
    }

    void BoardSetup()
    {
        roomHolder = new GameObject("Room").transform;

        for (int x = -1; x < columns + 1; x++)
        {
            for (int y = -1; y < rows + 1; y++)
            {
                GameObject toInstantiate = floorTiles[0];

                if (x == -1 || x == columns || y == -1 || y == rows)
                {
                    toInstantiate = wallTile;
                }

                if (north && x == 4 && y == -1)  // TODO: Char moves onto this spot, should go to tile above SDoor
                    toInstantiate = NDoor;
                if (south && x == 4 && y == rows)  // TODO: Char moves onto this spot, should go to tile below NDoor
                    toInstantiate = SDoor;
                if (east && y == 4 && x == columns)  // TODO: Char moves onto this spot, should go to tile right of WDoor
                    toInstantiate = EDoor;
                if (west && y == 4 && x == -1)  // TODO: Char moves onto this spot, should go to tile left of EDoor
                    toInstantiate = WDoor;

                GameObject instance = Instantiate(toInstantiate, new Vector2(x, y), Quaternion.identity) as GameObject;

                instance.transform.SetParent(roomHolder);
            }
        }
    }  

    public void SetupScene()
    {
        BoardSetup();
        InitializeList();
    }
}
