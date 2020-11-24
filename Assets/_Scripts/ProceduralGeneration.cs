using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ProceduralGeneration : MonoBehaviour
{
    // Generates and Places Tiles for tilemap
    public int xRendDistance;
    public int yRendDistance;
    public int xGenDist;
    public int yGenDist;
    public int caveHeight;
    public int groundDepth;
    public int caveAltitude;
    public int height;
    public GameObject player;
    public Tile ground;
    public Tile floor;
    Tilemap tileMap;
    int heightRefresh;
    int xMax;
    Vector3 pos;
    List<List<int>> map = new List<List<int>>();
    // Start is called before the first frame update
    void Start()
    {
        print(-Mathf.RoundToInt(yGenDist / 2) + " " + Mathf.RoundToInt(yGenDist / 2));
        // Initilizes Player Position
        pos = player.transform.position;
        xMax = xGenDist;
        heightRefresh = xGenDist;
        tileMap = transform.GetComponent<Tilemap>();
        // Addes some starter tiles to build off of.
        for(int x = 0; x < 5; x++)
        {
            List<int> temp = new List<int>();
            //int y = -Mathf.RoundToInt(yGenDist/2);y< Mathf.RoundToInt(yGenDist / 2);y++
            //int y =  anchor - Mathf.RoundToInt(yGenDist / 2); y < anchor + Mathf.RoundToInt(yGenDist / 2); y++
            for (int y = 0; y < height+yGenDist; y++)
            {
                if (y <= height || y >= height + caveHeight)
                {
                    temp.Add(1);
                }
                else
                {
                    temp.Add(0);
                }
            }
            map.Add(temp);
        }
        RefreshMap();
    }

    // Update is called once per frame
    void Update()
    {
        // Updates Player Positon
        pos = player.transform.position;
        print(height);
        // Changes cave height with reguards to world coordinates
        if (heightRefresh < pos.x)
        {
            if (Random.Range(0, 10) == 0)
            {
                height += 1;
                heightRefresh = (int)pos.x + 10;
            }
            else if (Random.Range(0, 10) == 1)
            {
                height -= 1;
                heightRefresh = (int)pos.x + 10;
            }
        }
        if (xMax < pos.x)
        {
            xMax = (int)pos.x;
        }
        //print(map.Count + " " + xMax);
        // Continously generates new map data to render tiles. Will only generate if the player
        // has caught up to the previous xMax corrdinate (Allows for batch generation instead of per frame).
        for(int x = map.Count; x < xMax+xGenDist; x++)
        {
            List<int> temp = new List<int>();
            //int y = height-groundDepth; y<=height+caveAltitude; y++
            for (int y = 0; y < height + yGenDist; y++)
            {
                // Creates the gap in blocks that act as the cave or the surface.
                if (y <= height || y>=height+caveHeight)
                {
                    temp.Add(1);
                }
                else
                {
                    temp.Add(0);
                }
            }
            map.Add(temp);
        }
        RefreshMap();
    }

    private void RefreshMap()
    {
        // This re-draws the Map based on player position. It takes the map data that is generated
        // and draws the tile out RendDistance in the area surrounding player.
        tileMap.ClearAllTiles();
        for (int x = (int)pos.x - xRendDistance; x < (int)pos.x + xRendDistance; x++)
        {
            for(int y = (int)pos.y - yRendDistance; y<(int)pos.y+yRendDistance; y++)
            {
                if (x >= 0 && y >= 0 && map.Count > x)
                {
                    if(map[x].Count > y)
                    {
                        if (map[x][y] == 1)
                        {
                            tileMap.SetTile(new Vector3Int(x, y, 0), ground);
                        }
                    }
                }
            }
        }
    }
}
